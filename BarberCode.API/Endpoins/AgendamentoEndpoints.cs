using AutoMapper;
using BarberCode.API.Models;
using BarberCode.Application.Interfaces;
using BarberCode.Application.UseCases.Agendamentos;
using BarberCode.Domain.Entities.Agendamentos;
using BarberCode.Service.Requests;
using BarberCode.Service.Responses;
using BarberCode.Domain.Shared;
namespace BarberCode.API.Endpoins;

public static class AgendamentoEndpoints
{
	public static void MapAgendamentoEndpoints(this IEndpointRouteBuilder routes)
	{
		var group = routes.MapGroup("/api/Agendamento").WithTags(nameof(Agendamento));

		group.MapGet("/", async (Guid barbeiroId, IAgendamentoRepository repo,
		 IMapper mapper, StatusAgendamento? status) =>
		{
			var agendamentos = await repo.BuscarAgendamentosAsync(barbeiroId, status);
			return ResultData<List<AgendamentoResponse>>.Success(mapper.Map<List<AgendamentoResponse>>
			(agendamentos)).ToOkSingleResult();
		})
		.WithName("GetAllAgendamentos")
		.WithOpenApi()
		.RequireAuthorization("manager","employee");

		group.MapGet("/{id}", async (Guid id, IAgendamentoRepository repo, IMapper mapper) =>
		{
			var agendamento = await repo.BuscarAgendadamentoPorIdAsync(id);

			return (agendamento is null
				? ResultData<AgendamentoResponse>.Failure(ResultType.NotFound, "Agendamento não encontrado")
				: ResultData<AgendamentoResponse>.Success(mapper.Map<AgendamentoResponse>(agendamento)))
			.ToOkSingleResult();
		})
		.WithName("GetAgendamentoById")
		.WithOpenApi();

		group.MapPatch("/{id}/concluir", async (Guid id, ConcluirAgendamentoUseCase useCase) =>
		{
			var result = await useCase.ExecuteAsync(id);
			return result.ToNoContentResult();
		})
		.WithName("ConcluirAgendamento")
		.WithOpenApi()
		.RequireAuthorization("manager", "employee");

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
		.WithOpenApi()
		.RequireAuthorization("manager", "employee"); ;
	}
}
