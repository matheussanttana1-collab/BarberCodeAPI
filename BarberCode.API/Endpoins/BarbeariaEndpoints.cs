using BarberCode.Domain.Entities.Barbearias;
using BarberCode.Infra.Banco;
using Microsoft.AspNetCore.Http.HttpResults;
using BarberCode.Service.Responses;
using BarberCode.Service.Requests;
using BarberCode.Application.UseCases;
using BarberCode.Application.UseCases.Servicos;
using Microsoft.AspNetCore.Mvc;

namespace BarberCode.API.Endpoins;


public static class BarbeariaEndpoints
{
    public static void MapBarbeariaEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Barbearia").WithTags(nameof(Barbearia));

        group.MapGet("/", async (BarberCodeContext db) =>
        {
            // TODO: Implementar GetAllBarbearia
            // Retornar List<BarbeariaResponse>
            return await Task.FromResult(Enumerable.Empty<BarbeariaResponse>());
        })
        .WithName("GetAllBarbearia")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<BarbeariaResponse>, NotFound>> (Guid id, BarberCodeContext db) =>
        {
            // TODO: Implementar GetBarbeariaById
            // Retornar BarbeariaResponse
            return TypedResults.NotFound();
        })
        .WithName("GetBarbeariaById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (Guid id, BarbeariaRequest request, BarberCodeContext db) =>
        {
            // TODO: Implementar UpdateBarbearia
            // Receber BarbeariaRequest
            return TypedResults.NotFound();
        })
        .WithName("UpdateBarbearia")
        .WithOpenApi();

        group.MapPost("/", async (BarbeariaRequest request, CriarBarbeariaUseCase criarCase) =>
        {
            criarCase.Execute(request);

            return Results.Created();
        })
        .WithName("CreateBarbearia")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (Guid id, BarberCodeContext db) =>
        {
            // TODO: Implementar DeleteBarbearia
            return TypedResults.NotFound();
        })
        .WithName("DeleteBarbearia")
        .WithOpenApi();
    }
}
