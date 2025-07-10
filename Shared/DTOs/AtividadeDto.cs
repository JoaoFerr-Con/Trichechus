using System.ComponentModel.DataAnnotations;

namespace Trichechus.Frontend.Shared.DTOs;

public class AtividadeDto
{
	public Guid Id { get; set; }

	[Required(ErrorMessage = "O título é obrigatório.")]
	[StringLength(100, ErrorMessage = "Máximo de 100 caracteres.")]
	public string Titulo { get; set; } = string.Empty;
	public string? Descricao { get; set; }
	public string? Situacao { get; set; }
	public string? NomeResponsavel { get; set; }
	public DateTime Prazo { get; set; }
	public string? NomeEquipeResponsavel { get; set; }
	public DateTime CriadoEm { get; set; }
	public DateTime? AtualizadoEm { get; set; }
	public DateTime? DeletadoEm { get; set; }
	public string? TipoEntrada { get; set; }

	public List<TarefaDto> Tarefas { get; set; } = new List<TarefaDto>();
}

public class CreateAtividadeDto
{
	[Required(ErrorMessage = "O título é obrigatório")]
	[StringLength(200, ErrorMessage = "O título não pode ter mais de 200 caracteres")]
	public string? Titulo { get; set; }

	[StringLength(1000, ErrorMessage = "A descrição não pode ter mais de 1000 caracteres")]
	public string? Descricao { get; set; }

	public string? Situacao { get; set; }

	public string? NomeResponsavel { get; set; }

	[Required(ErrorMessage = "O prazo é obrigatório")]
	public DateTime Prazo { get; set; } = DateTime.Now.AddDays(7);

	public string? NomeEquipeResponsavel { get; set; }

	public string? TipoEntrada { get; set; }

	public DateTime? DeletadoEm { get; set; } 
}

public class UpdateAtividadeDto
{
	public Guid? Id { get; set; }

	[Required(ErrorMessage = "O título é obrigatório")]
	[StringLength(200, ErrorMessage = "O título não pode ter mais de 200 caracteres")]
	public string? Titulo { get; set; }

	[StringLength(1000, ErrorMessage = "A descrição não pode ter mais de 1000 caracteres")]
	public string? Descricao { get; set; }

	public string? Situacao { get; set; }

	public string? NomeResponsavel { get; set; }

	[Required(ErrorMessage = "O prazo é obrigatório")]
	public DateTime Prazo { get; set; }

	public string? NomeEquipeResponsavel { get; set; }

	public string? TipoEntrada { get; set; }
}

public class DeleteSoftAtividadeDto
{
	public Guid Id { get; set; }
	public string Titulo { get; set; } = string.Empty;
	public DateTime? DeletadoEm { get; set; } = DateTime.UtcNow;
}
