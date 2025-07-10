using System.ComponentModel.DataAnnotations;

namespace Trichechus.Frontend.Shared.DTOs;

public class FuncionalidadeDto
{
	public Guid Id { get; set; }
	public string Nome { get; set; } = default!;
	public string? Descricao { get; set; }
}

public class CreateFuncionalidadeDto
{
	[Required(ErrorMessage = "O nome  é obrigatório")]
	[StringLength(200, ErrorMessage = "O título não pode ter mais de 200 caracteres")]
	public string Nome { get; set; } = default!;

	[StringLength(1000, ErrorMessage = "A descrição não pode ter mais de 1000 caracteres")]
	public string? Descricao { get; set; }
}

public class UpdateFuncionalidadeDto
{
	[Required(ErrorMessage = "O nome  é obrigatório")]
	[StringLength(200, ErrorMessage = "O título não pode ter mais de 200 caracteres")]
	public string Nome { get; set; } = default!;

	[StringLength(1000, ErrorMessage = "A descrição não pode ter mais de 1000 caracteres")]
	public string? Descricao { get; set; }
}