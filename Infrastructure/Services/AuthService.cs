using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using Trichechus.Frontend.Shared.DTOs;

namespace Trichechus.Frontend.Infrastructure.Services;

public class AuthService : IAuthService
{
	private readonly HttpClient _httpClient;
	private readonly LocalStorageService _localStorage;
	private readonly AuthenticationStateProvider _authStateProvider;

	public AuthService(HttpClient httpClient, LocalStorageService localStorage, AuthenticationStateProvider authStateProvider)
	{
		_httpClient = httpClient;
		_localStorage = localStorage;
		_authStateProvider = authStateProvider;
	}

	public async Task<AuthResponseDto> LoginSGAAsync(LoginRequestDto loginRequest)
	{
		var response = await SendAuthRequestAsync("api/SGAAutenticacao/login", loginRequest);
		if (response.IsSuccessStatusCode)
		{
			var authResponse = await response.Content.ReadFromJsonAsync<AuthResponseDto>();
			await StoreAuthDataAsync(authResponse!);
			return authResponse!;
		}

		throw new Exception($"Falha na autenticação SGA: {response.ReasonPhrase}");
	}

	public async Task<AuthResponseDto> LoginLocalAsync(LoginRequestDto loginRequest)
	{
		var response = await SendAuthRequestAsync("api/LocalAutenticacao/login", loginRequest);
		if (response.IsSuccessStatusCode)
		{
			var authResponse = await response.Content.ReadFromJsonAsync<AuthResponseDto>();

			if (authResponse is null)
				throw new Exception("Resposta da API está vazia (authResponse nulo).");

			await StoreAuthDataAsync(authResponse);

			if (_authStateProvider is AuthStateProvider customProvider)
				customProvider.NotifyUserAuthentication(authResponse.Token);

			return authResponse;
		}

		var errorContent = await response.Content.ReadAsStringAsync();
		throw new Exception($"Falha na autenticação local: {errorContent}");
	}

	public async Task<AuthResponseDto> RegisterLocalAsync(RegisterRequestDto registerRequest)
	{
		var response = await SendAuthRequestAsync("api/LocalAutenticacao/registrar", registerRequest);
		if (response.IsSuccessStatusCode)
		{
			var authResponse = await response.Content.ReadFromJsonAsync<AuthResponseDto>();
			await StoreAuthDataAsync(authResponse!);
			return authResponse!;
		}

		throw new Exception($"Falha no registro: {response.ReasonPhrase}");
	}

	public async Task Logout()
	{
		await _localStorage.RemoveItemAsync("authToken");
		await _localStorage.RemoveItemAsync("authUser");
		await _localStorage.RemoveItemAsync("authExpiration");

		if (_authStateProvider is AuthStateProvider customProvider)
			customProvider.NotifyUserLogout();
	}

	private async Task<HttpResponseMessage> SendAuthRequestAsync<T>(string url, T data)
	{
		try
		{
			var request = new HttpRequestMessage(HttpMethod.Post, url);
			var json = JsonSerializer.Serialize(data);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			request.Content = content;

			// Configuração para CORS
			request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

			// Console.WriteLine($"Enviando requisição para: {request.RequestUri}");
			// Console.WriteLine($"Conteúdo: {json}");
			// Console.WriteLine($"Request: {request}");

			// var response = await _httpClient.SendAsync(request);
			var response = await _httpClient.PostAsync(url, content);

			Console.WriteLine($"Status da resposta: {response.StatusCode}");
			if (!response.IsSuccessStatusCode)
			{
				var errorContent = await response.Content.ReadAsStringAsync();
				Console.WriteLine($"Erro: {errorContent}");
			}

			return response;
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Exceção: {ex.Message}");
			throw;
		}
	}

	private async Task StoreAuthDataAsync(AuthResponseDto authResponse)
	{
		await _localStorage.SetItemAsync("authToken", authResponse.Token);
		// await _localStorage.SetItemAsStringAsync("authToken", authResponse.Token);
		// await _localStorage.SetItemAsync("authToken", JsonSerializer.Deserialize<string>(JsonSerializer.Serialize(authResponse.Token)));
		await _localStorage.SetItemAsync("authUser", JsonSerializer.Serialize(new
		{
			authResponse.Nome,
			authResponse.Email,
			authResponse.Roles
		}));
		await _localStorage.SetItemAsync("authExpiration", authResponse.Expiracao.ToString("o"));
		
	}
}