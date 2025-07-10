using System.ComponentModel.DataAnnotations;

namespace Trichechus.Frontend.Shared.DTOs;

public class PerfilDto
{
	public Guid Id { get; set; }
	public string Nome { get; set; } = default!;
	public string? Descricao { get; set; }
	public List<FuncionalidadeDto>? Funcionalidades { get; set; }
}

public class CreatePerfilDto
{
	[Required(ErrorMessage = "O nome do perfil é obrigatório")]
	[StringLength(100)]
	public string Nome { get; set; } = default!;
	public string? Descricao { get; set; }
}

public class UpdatePerfilDto
{
	[Required(ErrorMessage = "O nome do perfil é obrigatório")]
	[StringLength(100)]
	public string Nome { get; set; } = default!;
	public string? Descricao { get; set; }
}

// public class DeleteSoftPerfilDto
// {
// 	public Guid Id { get; set; }
// 	public string Nome { get; set; } = default!;

// }

