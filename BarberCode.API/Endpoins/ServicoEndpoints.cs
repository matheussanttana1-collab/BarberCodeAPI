using BarberCode.Application.UseCases.Servicos;
using BarberCode.Domain.Entities.Barbearias;
using BarberCode.Service.Requests;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
namespace BarberCode.API.Endpoins;

public static class ServicoEndpoints
{
    public static void MapServicoEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Barbearia/{barbeariaId}/Servico").WithTags(nameof(Servico));

        group.MapGet("/", () =>
        {

        })
        .WithName("GetAllServicos")
        .WithOpenApi();

        group.MapGet("/{id}", (int id) =>
        {
            //return new Servico { ID = id };
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
