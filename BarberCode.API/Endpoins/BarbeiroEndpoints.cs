using AutoMapper;
using BarberCode.API.Models;
using BarberCode.Application.Interfaces;
using BarberCode.Application.Requests;
using BarberCode.Application.UseCases.Barbeiros;
using BarberCode.Application.Validators;
using BarberCode.Domain.Entities.Barbeiros;
using BarberCode.Domain.Shared;
using BarberCode.Service.Requests;
using BarberCode.Service.Responses;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BarberCode.API.Endpoins;

public static class BarbeiroEndpoints
{
	public static void MapBarbeiroEndpoints(this IEndpointRouteBuilder routes)
	{
		var group = routes.MapGroup("/api/Barbeiros").WithTags(nameof(Barbeiro));

		group.MapGet("/Barbearia/{barbeariaId}", async (Guid barbeariaId, IBarbeiroRepository repo, IMapper mapper) =>
		{
			var barbeiros = await repo.BuscarBarbeirosAsync(barbeariaId);
			return ResultData<List<BarbeiroResponse>>.Success(mapper.Map<List<BarbeiroResponse>>(barbeiros))
			.ToOkSingleResult();
		})
		.WithName("GetAllBarbeiros")
		.WithOpenApi();

		group.MapGet("/{id}", async (Guid id, IBarbeiroRepository repo, IMapper mapper) =>
		{
			var barbeiro = await repo.BuscarBarbeiroPorAsync(id);

			return (barbeiro is null
			? ResultData<BarbeiroResponse>.Failure(ResultType.NotFound, "Barbeiro Não Encontrado")
			: ResultData<BarbeiroResponse>.Success(mapper.Map<BarbeiroResponse>(barbeiro)))
			.ToOkSingleResult();
		})
		.WithName("GetBarbeiroById")
		.WithOpenApi();

		group.MapGet("/{id}/slots", async (Guid id,DateOnly diaEscolhido,
			Guid servicoId, GerarSlotsUseCase useCase) =>
		{
			var slots = await useCase.ExecuteAsync(id,servicoId, diaEscolhido);
			return slots.ToOkSingleResult();
		})
		.WithName("GerarSlotsBarbeiro")
		.WithOpenApi();
		

		group.MapPatch("/{id}", async (Guid id, AtualizarBarbeiroRequest request, AlterarBarbeiroUseCase useCase) =>
		{
			var result = await useCase.ExecuteAsync(id, request);
			return result.ToNoContentResult();
		})
		.WithName("UpdateBarbeiro")
		.WithOpenApi()
		.AddEndpointFilter<ValidationFilter<AtualizarBarbeiroRequest>>()
		.RequireAuthorization("manager","employee");

		group.MapPost("/", async (CriarBarbeiroRequest request,
		CriarBarbeiroUseCase useCase, ClaimsPrincipal user) =>
		{
			var barbeariaId = user.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
			var result = await useCase.ExecuteAsync(request, Guid.Parse(barbeariaId));
			return result.ToCreateResult($"/api/Barbeiros/{result}");
		})
		.WithName("CreateBarbeiro")
		.WithOpenApi()
		.AddEndpointFilter<ValidationFilter<CriarBarbeiroRequest>>()
		.RequireAuthorization("manager");

		group.MapDelete("/{id}", async (Guid id, DeletarBarbeiroUseCase useCase, ClaimsPrincipal user) =>
		{
			var barbeariaId = user.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
			var result = await useCase.ExecuteAsync(id, Guid.Parse(barbeariaId));
			return result.ToNoContentResult();
		})
		.WithName("DeleteBarbeiro")
		.WithOpenApi()
		.RequireAuthorization("manager");
	}
}
