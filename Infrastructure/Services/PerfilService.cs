using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Trichechus.Frontend.Shared.DTOs;

namespace Trichechus.Frontend.Infrastructure.Services
{
	public class PerfilService
	{
		private readonly HttpService _httpService;
		private readonly string _baseUrl = "api/Perfis";

		public PerfilService(HttpService httpService)
		{
			_httpService = httpService;
		}

		public async Task<IEnumerable<PerfilDto>> GetAllPerfilsAsync()
		{
			return await _httpService.GetAsync<IEnumerable<PerfilDto>>(_baseUrl);
		}

		public async Task<PerfilDto> GetPerfilByIdAsync(Guid id)
		{
			return await _httpService.GetAsync<PerfilDto>($"{_baseUrl}/{id}");
		}

		public async Task<Guid> CreatePerfilAsync(CreatePerfilDto perfil)
		{
			return await _httpService.PostAsync<Guid>(_baseUrl, perfil);
		}

		public async Task UpdatePerfilAsync(Guid id, UpdatePerfilDto perfil)
		{
			await _httpService.PutAsync<object>($"{_baseUrl}/{id}", perfil);
		}

		public async Task DeletePerfilAsync(Guid id)
		{
			await _httpService.DeleteAsync($"{_baseUrl}/{id}");
		}
	}
}
