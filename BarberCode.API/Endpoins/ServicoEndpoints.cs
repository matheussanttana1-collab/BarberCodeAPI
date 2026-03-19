using AutoMapper;
using BarberCode.Application.Interfaces;
using BarberCode.Application.Requests;
using BarberCode.Application.UseCases.Servicos;
using BarberCode.Domain.Entities.Barbearias;
using BarberCode.Service.Requests;
using BarberCode.Service.Responses;
namespace BarberCode.API.Endpoins;

public static class ServicoEndpoints
{
    public static void MapServicoEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Barbearia/{barbeariaId}/Servico").WithTags(nameof(Servico));

        group.MapGet("/", (Guid BarbeariaId, IServicoRepository repo, IMapper mapper) =>
        {
            var servico = repo.BuscarServicos(BarbeariaId);

            return Results.Ok(mapper.Map<List<ServicoResponse>>(servico));
        })
        .WithName("GetAllServicos")
        .WithOpenApi();

        group.MapGet("/{id}", (Guid Id, IServicoRepository repo, IMapper mapper) =>
        {
            var servico = repo.BuscarServicoPor(Id);

            if (servico is null)
                return Results.NotFound("Servico Não Encontrado");

            return Results.Ok(mapper.Map<ServicoResponse>(servico));

        })
        .WithName("GetServicoById")
        .WithOpenApi();

        group.MapPatch("/{id}", (Guid id, AtualizarServicoRequest request, AlterarServicoUseCase useCase) =>
        {
            useCase.Execute(id, request);
            return TypedResults.NoContent();
        })
        .WithName("UpdateServico")
        .WithOpenApi();

        group.MapPost("/", (Guid barbeariaId, ServicoRequest request, CriarServicoUseCase useCase) =>
        {
            useCase.Execute(barbeariaId, request);

            return Results.Created();
        })
        .WithName("CreateServico")
        .WithOpenApi();

        group.MapDelete("/{id}", (Guid id, DeletarServicoUseCase useCase) =>
        {
            useCase.Execute(id);
            return Results.NoContent();
        })
        .WithName("DeleteServico")
        .WithOpenApi();
    }
}
