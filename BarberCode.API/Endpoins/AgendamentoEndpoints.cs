using AutoMapper;
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
			await useCase.ExecuteAsync(id);
			return Results.NoContent();
		})
		.WithName("ConcluirAgendamento")
		.WithOpenApi();

		group.MapPost("/", async (CriarAgendamentoRequest request, CriarAgendamentoUseCase useCase) =>
		{
			var id = await useCase.ExecuteAsync(request);
			return Results.Created($"/api/Agendamento/{id}", new { id });
		})
		.WithName("CreateAgendamento")
		.WithOpenApi();

		group.MapDelete("/{id}", async (Guid id, CancelarAgendamentoUseCase useCase) =>
		{
			await useCase.ExecuteAsync(id);
			return Results.NoContent();
		})
		.WithName("DeleteAgendamento")
		.WithOpenApi();
	}
}
