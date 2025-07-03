using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Http;

namespace Trichechus.Frontend.Infrastructure.Services;

public class HttpService
{
	private readonly HttpClient _httpClient;
	// private readonly LocalStorageService _localStorage;

	// public HttpService(HttpClient httpClient, LocalStorageService localStorage)
	// {
	// 	_httpClient = httpClient;
	// 	_localStorage = localStorage;
	// }
	public HttpService(HttpClient httpClient)
	{
		_httpClient = httpClient;
	}

	public async Task<T> GetAsync<T>(string uri)
	{
		var request = new HttpRequestMessage(HttpMethod.Get, uri);
		return await SendRequestAsync<T>(request);
	}

	public async Task<T> PostAsync<T>(string uri, object value)
	{
		var request = new HttpRequestMessage(HttpMethod.Post, uri)
		{
			Content = new StringContent(JsonSerializer.Serialize(value), Encoding.UTF8, "application/json")
		};
		return await SendRequestAsync<T>(request);
	}

	public async Task<T> PutAsync<T>(string uri, object value)
	{
		var request = new HttpRequestMessage(HttpMethod.Put, uri)
		{
			Content = new StringContent(JsonSerializer.Serialize(value), Encoding.UTF8, "application/json")
		};
		return await SendRequestAsync<T>(request);
	}

	public async Task DeleteAsync(string uri, object value)
	{
		var request = new HttpRequestMessage(HttpMethod.Delete, uri)
		{
			Content = new StringContent(JsonSerializer.Serialize(value), Encoding.UTF8, "application/json")
		};
		
		await SendRequestAsync<object>(request);
	}

	private async Task<T> SendRequestAsync<T>(HttpRequestMessage request)
	{
		var response = await _httpClient.SendAsync(request);

		// Auto logout se 401 Unauthorized
		if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
		{
			// await _localStorage.RemoveItemAsync("authToken");
			// await _localStorage.RemoveItemAsync("authUser");
			// await _localStorage.RemoveItemAsync("authExpiration");
			throw new UnauthorizedAccessException("Não autorizado");
		}

		// Lança exceção para outros erros
		if (!response.IsSuccessStatusCode)
		{
			var error = await response.Content.ReadAsStringAsync();
			throw new Exception($"Erro HTTP: {response.StatusCode}. Detalhes: {error}");
		}

		var content = await response.Content.ReadFromJsonAsync<T>();
		return content!;
	}
}
