using AutoMapper;
using BarberCode.API.Models;
using BarberCode.Application.Interfaces;
using BarberCode.Application.UseCases.Agendamentos;
using BarberCode.Domain.Entities.Agendamentos;
using BarberCode.Service.Responses;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using BarberCode.Domain.Shared;
namespace BarberCode.API.Endpoins;

public static class ClienteEndpoints
{
    public static void MapClienteInfoEndpoints (this IEndpointRouteBuilder routes)
    {
		var group = routes.MapGroup("/api/Cliente").WithTags("Cliente");

		group.MapGet("/Barbearia/{barbeariaId}", async (Guid barbeariaId, IClienteRepository repo, IMapper mapper) =>
		{
			var clientes = await repo.BuscarClientesAsync(barbeariaId);
			return ResultData<List<ClienteInfoResponse>>.Success(mapper.Map<List<ClienteInfoResponse>>(clientes))
			.ToOkSingleResult();
		})
		.WithName("GetAllClienteInfos")
		.WithOpenApi();

		group.MapGet("/", async (string celular, IClienteRepository repo, IMapper mapper) =>
		{
			var cliente = await repo.BuscarClientePeloTelefoneAsync(celular);

			return (cliente is null
				? ResultData<ClienteInfoResponse>.Failure(ResultType.NotFound, "Cliente não encontrado")
				: ResultData<ClienteInfoResponse>.Success(mapper.Map<ClienteInfoResponse>(cliente)))
			.ToOkSingleResult();
		})
		.WithName("GetClienteInfoByCel")
		.WithOpenApi();

		group.MapGet("/{id}/agendamentos", async (Guid id, IAgendamentoRepository repo, IMapper mapper) =>
		{
			var agendamentos = await repo.BuscarAgendamentosDoClienteAsync(id);

			return ResultData<List<AgendamentoResponse>>.Success(mapper.Map<List<AgendamentoResponse>>
			(agendamentos)).ToOkSingleResult();
		})
		.WithName("AgendamentosDoCliente")
		.WithOpenApi();

		group.MapDelete("/{id}/agendamentos/{agendamentoId}", async (Guid id, Guid agendamentoId,
		CancelarAgendamentoClienteUseCase useCase) =>
		{
			var result = await useCase.ExecuteAsync(agendamentoId, id);
			return result.ToNoContentResult();
		})
		.WithName("CancelarAgendamentoCliente")
		.WithOpenApi();

		//group.MapDelete("/{id}", (Guid id) =>
		//      {
		//          //return TypedResults.Ok(new ClienteInfo { ID = id });
		//      })
		//      .WithName("DeleteClienteInfo")
		//      .WithOpenApi();
	}
}
