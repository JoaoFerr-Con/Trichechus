using System.ComponentModel.DataAnnotations;

namespace Trichechus.Frontend.Shared.DTOs;

public class TarefaDto
{
	public Guid Id { get; set; }

	[Required(ErrorMessage = "O título é obrigatório")]
	[StringLength(200, ErrorMessage = "O título não pode ter mais de 200 caracteres")]
	public string Titulo { get; set; } = string.Empty;

	public string? Descricao { get; set; }

	public DateTime? Prazo { get; set; }

	public string? NomeResponsavel { get; set; }

	public string? Situacao { get; set; }

	public string? Observacao { get; set; }

	public DateTime CriadoEm { get; set; }

	public DateTime? AtualizadoEm { get; set; }

	public DateTime? DeletadoEm { get; set; }

	public Guid AtividadeId { get; set; } // Obrigatório
}

public class CreateTarefaDto
{
	[Required(ErrorMessage = "O título é obrigatório")]
	[StringLength(200, ErrorMessage = "O título não pode ter mais de 200 caracteres")]
	public string? Titulo { get; set; }

	[StringLength(1000, ErrorMessage = "A descrição não pode ter mais de 1000 caracteres")]
	public string? Descricao { get; set; }

	public DateTime? Prazo { get; set; }

	public string? NomeResponsavel { get; set; }

	public string? Situacao { get; set; }

	public string? Observacao { get; set; }

	[Required(ErrorMessage = "A atividade é obrigatória")]
	public Guid AtividadeId { get; set; }
}

public class UpdateTarefaDto
{
	[Required(ErrorMessage = "O título é obrigatório")]
	[StringLength(200, ErrorMessage = "O título não pode ter mais de 200 caracteres")]
	public string? Titulo { get; set; }

	[StringLength(1000, ErrorMessage = "A descrição não pode ter mais de 1000 caracteres")]
	public string? Descricao { get; set; }

	public DateTime? Prazo { get; set; }

	public string? NomeResponsavel { get; set; }

	public string? Situacao { get; set; }

	public string? Observacao { get; set; }
}

public class DeleteSoftTarefaDto
{
	public Guid Id { get; set; }
	public string Titulo { get; set; } = string.Empty;
	public DateTime? DeletadoEm { get; set; } = DateTime.UtcNow;
}