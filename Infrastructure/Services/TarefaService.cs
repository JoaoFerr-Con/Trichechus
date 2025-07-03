// Infrastructure/Services/TarefaService.cs
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Trichechus.Frontend.Shared.DTOs;

namespace Trichechus.Frontend.Infrastructure.Services
{
	public class TarefaService
	{
		private readonly HttpService _httpService;
		private readonly string _baseUrl = "api/Tarefas";

		public TarefaService(HttpService httpService)
		{
			_httpService = httpService;
		}

		public async Task<IEnumerable<TarefaDto>> GetAllTarefasAsync()
		{
			return await _httpService.GetAsync<IEnumerable<TarefaDto>>(_baseUrl);
		}

		public async Task<TarefaDto> GetTarefaByIdAsync(Guid id)
		{
			return await _httpService.GetAsync<TarefaDto>($"{_baseUrl}/{id}");
		}

		public async Task<IEnumerable<TarefaDto>> GetTarefasByAtividadeIdAsync(Guid atividadeId)
		{
			return await _httpService.GetAsync<IEnumerable<TarefaDto>>($"{_baseUrl}/atividade/{atividadeId}");
		}

		public async Task<Guid> CreateTarefaAsync(CreateTarefaDto tarefa)
		{
			return await _httpService.PostAsync<Guid>(_baseUrl, tarefa);
		}

		public async Task UpdateTarefaAsync(Guid id, UpdateTarefaDto tarefa)
		{
			await _httpService.PutAsync<object>($"{_baseUrl}/{id}", tarefa);
		}

		public async Task DeleteTarefaAsync(Guid id, DeleteSoftTarefaDto tarefa)
		{
			await _httpService.DeleteAsync($"{_baseUrl}/{id}", tarefa);
		}
	}
}
