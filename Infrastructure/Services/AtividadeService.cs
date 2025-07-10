using System.Net.Http.Json;
using Trichechus.Frontend.Shared.DTOs;

namespace Trichechus.Frontend.Infrastructure.Services;

public class AtividadeService
{
	private readonly HttpService _httpService;
	private readonly string _baseUrl = "api/Atividades";

	public AtividadeService(HttpService httpService)
	{
		_httpService = httpService;
	}

	public async Task<IEnumerable<AtividadeDto>> GetAllAtividadesAsync()
	{
		return await _httpService.GetAsync<IEnumerable<AtividadeDto>>(_baseUrl);
	}

	public async Task<AtividadeDto> GetAtividadeByIdAsync(Guid id)
	{
		return await _httpService.GetAsync<AtividadeDto>($"{_baseUrl}/{id}");
	}

	public async Task<Guid> CreateAtividadeAsync(CreateAtividadeDto atividade)
	{
		return await _httpService.PostAsync<Guid>(_baseUrl, atividade);
	}

	public async Task UpdateAtividadeAsync(Guid id, UpdateAtividadeDto atividade)
	{
		await _httpService.PutAsync<object>($"{_baseUrl}/{id}", atividade);
	}

	public async Task DeleteAtividadeAsync(Guid id)
	{
		await _httpService.DeleteAsync($"{_baseUrl}/{id}");
	}

}
