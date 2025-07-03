using Radzen;
using System;
using System.Threading.Tasks;
using Trichechus.Frontend.Layout.shared;

namespace Trichechus.Frontend.Infrastructure.Services;

public class AppNotificationService
{
	private readonly NotificationService _notificationService;
	private readonly DialogService _dialogService;
	public AppNotificationService(NotificationService notificationService, DialogService dialogService)
	{
		_notificationService = notificationService;
		_dialogService = dialogService;
	}

	public void Success(string message, string title = "Sucesso", double? duration = 5000)
	{
		var time = TimeSpan.FromMilliseconds(duration ?? 5000);
		_notificationService.Notify(NotificationSeverity.Success, title, message, time);
	}

	public void Info(string message, string title = "Informação", double? duration = 5000)
	{
		var time = TimeSpan.FromMilliseconds(duration ?? 5000);
		_notificationService.Notify(NotificationSeverity.Info, title, message, time);
	}

	public void Warning(string message, string title = "Aviso", double? duration = 5000)
	{
		var time = TimeSpan.FromMilliseconds(duration ?? 5000);
		_notificationService.Notify(NotificationSeverity.Warning, title, message, time);
	}

	public void Error(string message, string title = "Erro", double? duration = 8000)
	{
		var time = TimeSpan.FromMilliseconds(duration ?? 8000);
		_notificationService.Notify(NotificationSeverity.Error, title, message, time);
	}

	public async Task<bool> Confirm(string message, string title = "Confirmação")
	{
		var parameters = new Dictionary<string, object>
		{
			{ "Title", title },
			{ "Message", message }
		};

		var result = await _dialogService.OpenAsync<ConfirmDialog>(
			title,
			parameters,
			new DialogOptions
			{
				Width = "400px",
				Height = "250px",
				ShowClose = false,
				CloseDialogOnOverlayClick = false
			});

		return result is bool confirmed && confirmed;
	}
}
