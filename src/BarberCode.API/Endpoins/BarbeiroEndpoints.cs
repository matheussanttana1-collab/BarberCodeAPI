using AutoMapper;
using BarberCode.API.Models;
using BarberCode.Application.Interfaces;
using BarberCode.Application.Requests;
using BarberCode.Application.UseCases.Barbeiros;
using BarberCode.Domain.Entities.Barbeiros;
using BarberCode.Domain.Shared;
using BarberCode.Service.Requests;
using BarberCode.Service.Responses;
using System.Security.Claims;

namespace BarberCode.API.Endpoins;

public static class BarbeiroEndpoints
{
	public static void MapBarbeiroEndpoints(this IEndpointRouteBuilder routes)
	{
		var group = routes.MapGroup("/api/Barbeiros").WithTags(nameof(Barbeiro));

		group.MapGet("/", async (Guid barbeariaId, IBarbeiroRepository repo, IMapper mapper) =>
		{
			var barbeiros = await repo.BuscarBarbeirosAsync(barbeariaId);
			return ResultData<List<BarbeiroResponse>>.Success(mapper.Map<List<BarbeiroResponse>>(barbeiros))
			.ToOkSingleResult();
		})
		.WithName("GetAllBarbeiros")
		.WithOpenApi()
		.WithSummary("Lista todos os barbeiros de uma barbearia")
		.WithDescription("Retorna a lista completa de barbeiros registrados em uma barbearia específica.")
		.Produces<ResultData<List<BarbeiroResponse>>>(StatusCodes.Status200OK);

		group.MapGet("/{id}", async (Guid id, IBarbeiroRepository repo, IMapper mapper) =>
		{
			var barbeiro = await repo.BuscarBarbeiroPorAsync(id);

			return (barbeiro is null
			? ResultData<BarbeiroResponse>.Failure(ResultType.NotFound, "Barbeiro Não Encontrado")
			: ResultData<BarbeiroResponse>.Success(mapper.Map<BarbeiroResponse>(barbeiro)))
			.ToOkSingleResult();
		})
		.WithName("GetBarbeiroById")
		.WithOpenApi()
		.WithSummary("Obtém informações de um barbeiro específico")
		.WithDescription("Retorna os dados detalhados de um barbeiro (experiência, especialidades, etc.) pelo seu ID.")
		.Produces<ResultData<BarbeiroResponse>>(StatusCodes.Status200OK);

		group.MapGet("/{id}/slots", async (Guid id,DateOnly diaEscolhido,
			Guid servicoId, GerarSlotsUseCase useCase) =>
		{
			var slots = await useCase.ExecuteAsync(id,servicoId, diaEscolhido);
			return slots.ToOkSingleResult();
		})
		.WithName("GerarSlotsBarbeiro")
		.WithOpenApi()
		.WithSummary("Gera horários disponíveis para agendamento")
		.WithDescription("Retorna os horários (slots) disponíveis de um barbeiro para um serviço e data específicos.")
		.Produces<ResultData<object>>(StatusCodes.Status200OK);
		

		group.MapPatch("/{id}", async (Guid id, AtualizarBarbeiroRequest request, AlterarBarbeiroUseCase useCase
		, ClaimsPrincipal user) =>
		{
			var userId = user.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
			var result = await useCase.ExecuteAsync(id, request, Guid.Parse(userId));
			return result.ToNoContentResult();
		})
		.WithName("UpdateBarbeiro")
		.WithOpenApi()
		.AddEndpointFilter<ValidationFilter<AtualizarBarbeiroRequest>>()
		.RequireAuthorization("managerOrEmployee")
		.WithSummary("Atualiza informações de um barbeiro")
		.WithDescription("Permite que o gerenciador ou o próprio barbeiro modifique seus dados profissionais.")
		.Produces(StatusCodes.Status204NoContent);

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
		.RequireAuthorization("manager")
		.WithSummary("Registra um novo barbeiro na barbearia")
		.WithDescription("Permite que o gerenciador cadastre um novo barbeiro com suas especialidades e agenda.")
		.Produces(StatusCodes.Status201Created);

		group.MapDelete("/{id}", async (Guid id, DeletarBarbeiroUseCase useCase, ClaimsPrincipal user) =>
		{
			var barbeariaId = user.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
			var result = await useCase.ExecuteAsync(id, Guid.Parse(barbeariaId));
			return result.ToNoContentResult();
		})
		.WithName("DeleteBarbeiro")
		.WithOpenApi()
		.RequireAuthorization("manager")
		.WithSummary("Remove um barbeiro da barbearia")
		.WithDescription("Permite que o gerenciador delete um barbeiro do sistema.")
		.Produces(StatusCodes.Status204NoContent);
	}
}
