using AutoMapper;
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

        group.MapGet("/Barbearia/{barbeariaId}", (Guid barbeariaId, IClienteRepository repo,
        IMapper mapper) =>
        {
            var clientes = repo.BuscarClientes(barbeariaId);

            return Results.Ok(mapper.Map<List<ClienteInfoResponse>>(clientes));
        })
        .WithName("GetAllClienteInfos")
        .WithOpenApi();

        group.MapGet("/", (string celular, IClienteRepository repo, IMapper mapper) =>
        {
            var cliente = repo.BuscarClientePeloTelefone(celular);
            if (cliente is null)
                return Results.NotFound("Cliente Nao Encotrado");

            return Results.Ok(mapper.Map<ClienteInfoResponse>(cliente));
        })
        .WithName("GetClienteInfoByCel")
        .WithOpenApi();

        group.MapGet("/{id}/Agendamentos", (Guid id, IAgendamentoRepository repo, IMapper mapper) =>
        {
            var agendamentos = repo.BuscarAgendamentosDoCliente(id);

            return Results.Ok(mapper.Map<List<AgendamentoResponse>>(agendamentos));
        })
        .WithName("AgendamentosDoCliente")
        .WithOpenApi();

		group.MapDelete("Agendamento/{agendamentoId}/{id}", (Guid id, Guid agendamentoId,
        CancelarAgendamentoClienteUseCase useCase) =>
		{
			useCase.Execute(id, agendamentoId);
			return Results.NoContent();
		})
		.WithName("DeleteAgendamentoCliente")
		.WithOpenApi();

		group.MapDelete("/{id}", (Guid id) =>
        {
            //return TypedResults.Ok(new ClienteInfo { ID = id });
        })
        .WithName("DeleteClienteInfo")
        .WithOpenApi();
    }
}
