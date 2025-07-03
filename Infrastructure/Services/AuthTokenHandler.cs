using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace Trichechus.Frontend.Infrastructure.Services;

public class AuthTokenHandler : DelegatingHandler
{
	private readonly IJSRuntime _jsRuntime;
	private readonly LocalStorageService _localStorage;

	public AuthTokenHandler(IJSRuntime jsRuntime, LocalStorageService localStorage)
	{
		_localStorage = localStorage;
		_jsRuntime = jsRuntime;
	}

	protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
	{
		var token = await _localStorage.GetItemAsync<string>("authToken");
		// var token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "authToken");
		// token = token?.Trim('"'); // Remove aspas extras se existirem

		Console.WriteLine($"TOKEN INJETADO: {token}");
		
		if (!string.IsNullOrWhiteSpace(token))
		{
			Console.WriteLine($"🔐 Adicionando token no header: {token.Substring(0, 20)}..."); // log parcial
			request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
		}
		else
		{
			Console.WriteLine("⚠️ Nenhum token encontrado no localStorage.");
		}
		
		return await base.SendAsync(request, cancellationToken);
	}
}