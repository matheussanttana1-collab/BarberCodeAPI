using AutoMapper;
using BarberCode.API.Models;
using BarberCode.Application.Interfaces;
using BarberCode.Application.UseCases.Agendamentos;
using BarberCode.Domain.Entities.Agendamentos;
using BarberCode.Service.Requests;
using BarberCode.Service.Responses;
namespace BarberCode.API.Endpoins;

public static class AgendamentoEndpoints
{
	public static void MapAgendamentoEndpoints(this IEndpointRouteBuilder routes)
	{
		var group = routes.MapGroup("/api/Agendamento").WithTags(nameof(Agendamento));

		group.MapGet("/Barbeiro/{barbeiroId}", async (Guid barbeiroId, IAgendamentoRepository repo,
		 IMapper mapper, StatusAgendamento? status) =>
		{
			var agendamentos = await repo.BuscarAgendamentosAsync(barbeiroId, status);
			return Results.Ok(mapper.Map<List<AgendamentoResponse>>(agendamentos));
		})
		.WithName("GetAllAgendamentos")
		.WithOpenApi();

		group.MapGet("/{id}", async (Guid id, IAgendamentoRepository repo, IMapper mapper) =>
		{
			var agendamento = await repo.BuscarAgendadamentoPorIdAsync(id);
			if (agendamento is null)
				return Results.NotFound("Agendamento não encontrado.");

			return Results.Ok(mapper.Map<AgendamentoResponse>(agendamento));
		})
		.WithName("GetAgendamentoById")
		.WithOpenApi();

		group.MapPatch("/{id}/concluir", async (Guid id, ConcluirAgendamentoUseCase useCase) =>
		{
			var result = await useCase.ExecuteAsync(id);
			return result.ToNoContentResult();
		})
		.WithName("ConcluirAgendamento")
		.WithOpenApi();

		group.MapPost("/", async (CriarAgendamentoRequest request, CriarAgendamentoUseCase useCase) =>
		{
			var resultId = await useCase.ExecuteAsync(request);
			return resultId.ToCreateResult($"/api/Agendamento/{resultId}");
		})
		.WithName("CreateAgendamento")
		.WithOpenApi()
		.AddEndpointFilter<ValidationFilter<CriarAgendamentoRequest>>(); 

		group.MapDelete("/{id}", async (Guid id, CancelarAgendamentoUseCase useCase) =>
		{
			var result = await useCase.ExecuteAsync(id);
			return result.ToNoContentResult();
		})
		.WithName("DeleteAgendamento")
		.WithOpenApi();
	}
}
