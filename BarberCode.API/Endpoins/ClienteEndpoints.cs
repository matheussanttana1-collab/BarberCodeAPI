using AutoMapper;
using BarberCode.API.Models;
using BarberCode.Application.Interfaces;
using BarberCode.Application.UseCases.Agendamentos;
using BarberCode.Domain.Entities.Agendamentos;
using BarberCode.Service.Responses;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
namespace BarberCode.API.Endpoins;

public static class ClienteEndpoints
{
    public static void MapClienteInfoEndpoints (this IEndpointRouteBuilder routes)
    {
		var group = routes.MapGroup("/api/Cliente").WithTags("Cliente");

		group.MapGet("/Barbearia/{barbeariaId}", async (Guid barbeariaId, IClienteRepository repo, IMapper mapper) =>
		{
			var clientes = await repo.BuscarClientesAsync(barbeariaId);
			return Results.Ok(mapper.Map<List<ClienteInfoResponse>>(clientes));
		})
		.WithName("GetAllClienteInfos")
		.WithOpenApi();

		group.MapGet("/", async (string celular, IClienteRepository repo, IMapper mapper) =>
		{
			var cliente = await repo.BuscarClientePeloTelefoneAsync(celular);
			if (cliente is null)
				return Results.NotFound("Cliente não encontrado.");

			return Results.Ok(mapper.Map<ClienteInfoResponse>(cliente));
		})
		.WithName("GetClienteInfoByCel")
		.WithOpenApi();

		group.MapGet("/{id}/agendamentos", async (Guid id, IAgendamentoRepository repo, IMapper mapper) =>
		{
			var agendamentos = await repo.BuscarAgendamentosDoClienteAsync(id);
			return Results.Ok(mapper.Map<List<AgendamentoResponse>>(agendamentos));
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
