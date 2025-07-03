using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using Trichechus.Frontend.Shared.DTOs;

namespace Trichechus.Frontend.Infrastructure.Services;

	public class AuthStateProvider : AuthenticationStateProvider
	{
		private readonly HttpClient _httpClient;
		private readonly LocalStorageService _localStorage;
		private readonly AuthenticationState _anonymous;

		public AuthStateProvider(HttpClient httpClient, LocalStorageService localStorage)
		{
			_httpClient = httpClient;
			_localStorage = localStorage;
			_anonymous = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
		}

		public override async Task<AuthenticationState> GetAuthenticationStateAsync()
		{
			var token = await _localStorage.GetItemAsync<string>("authToken");

			if (string.IsNullOrWhiteSpace(token))
				return _anonymous;

			var expirationString = await _localStorage.GetItemAsync<string>("authExpiration");
			if (!DateTime.TryParse(expirationString, out var expiration) || expiration <= DateTime.Now)
			{
				await _localStorage.RemoveItemAsync("authToken");
				await _localStorage.RemoveItemAsync("authUser");
				await _localStorage.RemoveItemAsync("authExpiration");
				return _anonymous;
			}

			_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

			var userJson = await _localStorage.GetItemAsync<string>("authUser");
			if (string.IsNullOrWhiteSpace(userJson))
				return _anonymous;

			try
			{
				var userData = JsonSerializer.Deserialize<UserData>(userJson,
					new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

				var claims = new List<Claim>
				{
					new Claim(ClaimTypes.Name, userData!.Nome!),
					new Claim(ClaimTypes.Email, userData.Email!)
				};

				if (userData.Roles != null)
				{
					foreach (var role in userData.Roles)
					{
						claims.Add(new Claim(ClaimTypes.Role, role));
					}
				}

				var identity = new ClaimsIdentity(claims, "jwt");
				var user = new ClaimsPrincipal(identity);
				
				return new AuthenticationState(user);
			}
			catch
			{
				return _anonymous;
			}
		}

		public void NotifyUserAuthentication(string? token)
		{
			// var authenticatedUser = new ClaimsPrincipal(
				// new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, "User") }, "jwtAuthType"));

			// var authState = Task.FromResult(new AuthenticationState(authenticatedUser));
			var authState = GetAuthenticationStateAsync();
			NotifyAuthenticationStateChanged(authState);
		}

		public void NotifyUserLogout()
		{
			var authState = Task.FromResult(_anonymous);
			NotifyAuthenticationStateChanged(authState);
		}

		private class UserData
		{
			public string? Nome { get; set; }
			public string? Email { get; set; }
			public List<string>? Roles { get; set; }
		}
	}