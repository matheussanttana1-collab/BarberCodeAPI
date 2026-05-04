using BarberCode.API.Models;
using BarberCode.Application.UseCases.WhatsApp;
using System.Security.Claims;

namespace BarberCode.API.Endpoins;

public static class WhatsAppEndpoints
{
	public static void MapWhatsAppEndpoints(this IEndpointRouteBuilder routes)
	{
		var group = routes.MapGroup("/api/WhatsApp").WithTags("WhatsApp").RequireAuthorization("manager");

		group.MapPost("/Cadastrar", async (CadastrarWhatsAppUseCase useCase, ClaimsPrincipal user) =>
		{
			var id = user.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
			var result = await useCase.ExecuteAsync(Guid.Parse(id));
			return result.ToCreateResult($"/api/WhatsApp/Cadastrar/{result}");
		})
		.WithName("CadastrarWhatsApp")
		.WithOpenApi()
		.WithSummary("Registra uma nova conexão WhatsApp")
		.WithDescription("Cadastra e vincula uma nova conexão WhatsApp à barbearia do gerenciador autenticado.")
		.Produces(StatusCodes.Status201Created);

		group.MapGet("/QrCode", async (GerarNovoQrCodeUseCase useCase, ClaimsPrincipal user) =>
		{
			var id = user.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
			var result = await useCase.ExecuteAsync(Guid.Parse(id));
			return result.ToOkSingleResult();
		})
		.WithName("QrCodeWhatsApp")
		.WithOpenApi()
		.WithSummary("Gera novo QR Code para autenticação WhatsApp")
		.WithDescription("Gera um novo QR Code para escanear e autenticar a conexão WhatsApp da barbearia.")
		.Produces(StatusCodes.Status200OK);

		group.MapGet("/ConnectionState", async (BuscarStatusConexaoWhatsAppUseCase useCase, ClaimsPrincipal user) =>
		{
			var id = user.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
			var result = await useCase.ExecuteAsync(Guid.Parse(id));
			return result.ToOkSingleResult();
		})
		.WithName("ConnectionStateWhatsApp")
		.WithOpenApi()
		.WithSummary("Verifica o estado da conexão WhatsApp")
		.WithDescription("Retorna o status atual da conexão WhatsApp (conectado, desconectado, etc.) da barbearia.")
		.Produces(StatusCodes.Status200OK);

		group.MapDelete("/Logout", async (LogoutWhatsAppUseCase useCase, ClaimsPrincipal user) =>
		{
			var id = user.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
			var result = await useCase.ExecuteAsync(Guid.Parse(id));
			return result.ToNoContentResult();
		})
		.WithName("LogoutWhatsApp")
		.WithOpenApi()
		.WithSummary("Faz logout da conexão WhatsApp")
		.WithDescription("Desconecta a sessão WhatsApp da barbearia, mas mantém a configuração registrada.")
		.Produces(StatusCodes.Status204NoContent);


		group.MapDelete("/", async (DeletarWhatsAppUseCase useCase, ClaimsPrincipal user) =>
		{
			var id = user.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
			var result = await useCase.ExecuteAsync(Guid.Parse(id));
			return result.ToNoContentResult();
		})
		.WithName("DeletarWhatsApp")
		.WithOpenApi()
		.WithSummary("Remove a conexão WhatsApp")
		.WithDescription("Deleta completamente a configuração de conexão WhatsApp da barbearia.")
		.Produces(StatusCodes.Status204NoContent);
	}
}
