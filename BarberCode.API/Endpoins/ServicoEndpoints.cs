using AutoMapper;
using BarberCode.Application.Interfaces;
using BarberCode.Application.UseCases.Servicos;
using BarberCode.Domain.Entities.Barbearias;
using BarberCode.Service.Requests;
using BarberCode.Service.Responses;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
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

        group.MapPut("/{id}", (int id) =>
        {
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

        group.MapDelete("/{id}", (int id) =>
        {
            //return TypedResults.Ok(new Servico { ID = id });
        })
        .WithName("DeleteServico")
        .WithOpenApi();
    }
}
