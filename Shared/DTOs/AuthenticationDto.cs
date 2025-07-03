using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Trichechus.Frontend.Shared.DTOs
{
	public class LoginRequestDto
	{
		[Required(ErrorMessage = "O email é obrigatório")]
		[EmailAddress(ErrorMessage = "Formato de email inválido")]
		public string? Email { get; set; }

		[Required(ErrorMessage = "A senha é obrigatória")]
		public string? Senha { get; set; }
	}

	public class RegisterRequestDto
	{
		[Required(ErrorMessage = "O nome é obrigatório")]
		public string? Nome { get; set; }

		[Required(ErrorMessage = "O email é obrigatório")]
		[EmailAddress(ErrorMessage = "Formato de email inválido")]
		public string? Email { get; set; }

		[Required(ErrorMessage = "A senha é obrigatória")]
		[StringLength(100, MinimumLength = 6, ErrorMessage = "A senha deve ter entre 6 e 100 caracteres")]
		public string? Senha { get; set; }

		[Compare("Senha", ErrorMessage = "As senhas não conferem")]
		public string? ConfirmacaoSenha { get; set; }

		public List<string>? NomesPerfis { get; set; }
	}

	public class AuthResponseDto
	{
		public string? Token { get; set; }
		public string? Nome { get; set; }
		public string? Email { get; set; }
		public List<string>? Perfis { get; set; }
		public List<string>? Roles { get; set; }
		public System.DateTime Expiracao { get; set; }
		public bool IsAuthenticated => !string.IsNullOrEmpty(Token);
	}
}
