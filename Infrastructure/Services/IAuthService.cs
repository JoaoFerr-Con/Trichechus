using System.Threading.Tasks;
using Trichechus.Frontend.Shared.DTOs;

namespace Trichechus.Frontend.Infrastructure.Services;

	public interface IAuthService
	{
		Task<AuthResponseDto> LoginSGAAsync(LoginRequestDto loginRequest);
		Task<AuthResponseDto> LoginLocalAsync(LoginRequestDto loginRequest);
		Task<AuthResponseDto> RegisterLocalAsync(RegisterRequestDto registerRequest);
		Task Logout();
	}