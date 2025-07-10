using System.Text.Json;

public static class ErrorFormatter
{
	public static string FormatApiProblemDetails(string json)
	{
		try
		{
			using var doc = JsonDocument.Parse(json);
			var root = doc.RootElement;

			var sb = new System.Text.StringBuilder();
			// sb.AppendLine("⚠️ Erro ao processar a requisição:");

			// if (root.TryGetProperty("title", out var title))
			// {
			// 	var titleText = title.GetString();
			// 	if (titleText == "One or more validation errors occurred.")
			// 		sb.AppendLine("• Ocorreram erros de validação.");
			// 	else
			// 		sb.AppendLine($"• {titleText}");
			// }

			if (root.TryGetProperty("errors", out var errors))
			{
				foreach (var error in errors.EnumerateObject())
				{
					var key = error.Name;
					var messages = error.Value.EnumerateArray().Select(e => e.GetString());
					foreach (var msg in messages)
						sb.AppendLine($"→ {key}: {msg}");
				}
			}

			return sb.ToString();
		}
		catch
		{
			return $"❌ Erro ao interpretar a resposta da API:\n{json}";
		}
	}
}