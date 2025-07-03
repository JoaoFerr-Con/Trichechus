using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Radzen;
using Trichechus.Frontend;
using Trichechus.Frontend.Infrastructure.Services;
using System.IdentityModel.Tokens.Jwt;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Limpar mapeamentos de claims padrão
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

// Configurar HttpClient
// builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// Adicionar serviços Radzen
builder.Services.AddRadzenComponents();

// Adicionar serviços de autenticação
builder.Services.AddScoped<LocalStorageService>();
builder.Services.AddScoped<AuthTokenHandler>();



builder.Services.AddScoped(sp =>
{
	var config = sp.GetRequiredService<IConfiguration>();
	var handler = sp.GetRequiredService<AuthTokenHandler>();
	handler.InnerHandler = new HttpClientHandler();

	return new HttpClient(handler)
	{
		BaseAddress = new Uri(config["Endpoint-API"]!) // Ex: "http://localhost:8081/trichechus/"
	};
});


builder.Services.AddScoped<HttpService>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddScoped<AuthStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();

// Adicionar serviços da aplicação
builder.Services.AddScoped<AtividadeService>();
builder.Services.AddScoped<TarefaService>();
builder.Services.AddScoped<PerfilService>();
builder.Services.AddScoped<AppNotificationService>();

builder.Services.AddScoped<DialogService>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<TooltipService>();
builder.Services.AddScoped<ContextMenuService>();

// Configurar autenticação
builder.Services.AddAuthorizationCore();




await builder.Build().RunAsync();
