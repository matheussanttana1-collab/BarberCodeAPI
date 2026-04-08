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
		.WithOpenApi();

		group.MapGet("/QrCode", async (GerarNovoQrCodeUseCase useCase, ClaimsPrincipal user) =>
		{
			var id = user.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
			var result = await useCase.ExecuteAsync(Guid.Parse(id));
			return result.ToOkSingleResult();
		})
		.WithName("QrCodeWhatsApp")
		.WithOpenApi();

		group.MapDelete("/", async (DeletarWhatsAppUseCase useCase, ClaimsPrincipal user) =>
		{
			var id = user.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
			var result = await useCase.ExecuteAsync(Guid.Parse(id));
			return result.ToNoContentResult();
		})
		.WithName("DeletarWhatsApp")
		.WithOpenApi();
	}
}
