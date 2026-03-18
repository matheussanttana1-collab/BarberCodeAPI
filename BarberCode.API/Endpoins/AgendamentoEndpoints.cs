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

        group.MapGet("/Barbeiro/{BarbeiroId}", (Guid barbeiroId, IAgendamentoRepository repo, IMapper mapper) =>
        {
            var agendamentos = repo.BuscarAgendamentos(barbeiroId);

            return Results.Ok(mapper.Map<List<AgendamentoResponse>>(agendamentos));
        })
        .WithName("GetAllAgendamentos")
        .WithOpenApi();



        group.MapGet("/{id}", (Guid Id, IAgendamentoRepository repo, IMapper mapper) =>
        {
            var agendamento = repo.BuscarAgendadamentoPorId(Id);

            if (agendamento == null)
            {
                return Results.NotFound("Agendamento Nao escontrado");
            }

            return Results.Ok(mapper.Map<AgendamentoResponse>(agendamento));
        })
        .WithName("GetAgendamentoById")
        .WithOpenApi();

        group.MapPut("/{id}", () =>
        {
            return TypedResults.NoContent();
        })
        .WithName("UpdateAgendamento")
        .WithOpenApi();

        group.MapPost("/", (AgendamentoRequest request, CriarAgendamentoUseCase useCase) =>
        {
            useCase.Execute(request);
            return Results.Created();
        })
        .WithName("CreateAgendamento")
        .WithOpenApi();

        group.MapDelete("/{id}", (Guid id,CancelarAgendamentoUseCase useCase) =>
        {
            useCase.Execute(id);
            return Results.NoContent();
        })
        .WithName("DeleteAgendamento")
        .WithOpenApi();
    }
}
