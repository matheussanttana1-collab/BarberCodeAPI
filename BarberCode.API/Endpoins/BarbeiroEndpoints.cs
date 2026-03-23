using AutoMapper;
using BarberCode.Application.Interfaces;
using BarberCode.Application.Requests;
using BarberCode.Application.Responses;
using BarberCode.Application.UseCases.Barbearias;
using BarberCode.Application.UseCases.Barbeiros;
using BarberCode.Domain.Entities.Barbeiros;
using BarberCode.Service.Requests;
using BarberCode.Service.Responses;
using Microsoft.AspNetCore.Http.HttpResults;
namespace BarberCode.API.Endpoins;

public static class BarbeiroEndpoints
{
	public static void MapBarbeiroEndpoints(this IEndpointRouteBuilder routes)
	{
		var group = routes.MapGroup("/api/Barbeiro").WithTags(nameof(Barbeiro));

		group.MapGet("/Barbearia/{barbeariaId}", async (Guid barbeariaId, IBarbeiroRepository repo, IMapper mapper) =>
		{
			var barbeiros = await repo.BuscarBarbeirosAsync(barbeariaId);
			return Results.Ok(mapper.Map<List<BarbeiroResponse>>(barbeiros));
		})
 .WithName("GetAllBarbeiros")
 .WithOpenApi();

		group.MapGet("/{id}", async (Guid id, IBarbeiroRepository repo, IMapper mapper) =>
		{
			var barbeiro = await repo.BuscarBarbeiroPorAsync(id);
			if (barbeiro is null)
				return Results.NotFound("Barbeiro não encontrado.");

			return Results.Ok(mapper.Map<BarbeiroResponse>(barbeiro));
		})
		.WithName("GetBarbeiroById")
		.WithOpenApi();

		group.MapGet("/{id}/slots", async (Guid id, Guid barbeariaId, DateOnly diaEscolhido,
			Guid servicoId, GerarSlotsUseCase useCase) =>
		{
			var slots = await useCase.ExecuteAsync(id, barbeariaId, servicoId, diaEscolhido);
			return Results.Ok(slots);
		})
		.WithName("GerarSlotsBarbeiro")
		.WithOpenApi();

		group.MapPatch("/{id}", async (Guid id, AtualizarBarbeiroRequest request, AlterarBarbeiroUseCase useCase) =>
		{
			await useCase.ExecuteAsync(id, request);
			return Results.NoContent();
		})
		.WithName("UpdateBarbeiro")
		.WithOpenApi();

		group.MapPost("/barbearia/{barbeariaId}", async (Guid barbeariaId, BarbeiroRequest request,
		CriarBarbeiroUseCase useCase) =>
		{
			var id = await useCase.ExecuteAsync(request, barbeariaId);
			return Results.Created($"/api/Barbeiros/{id}", new { id });
		})
		.WithName("CreateBarbeiro")
		.WithOpenApi();

		group.MapDelete("/{id}", async (Guid id, DeletarBarbeiroUseCase useCase) =>
		{
			await useCase.ExecuteAsync(id);
			return Results.NoContent();
		})
		.WithName("DeleteBarbeiro")
		.WithOpenApi();
	}
}
