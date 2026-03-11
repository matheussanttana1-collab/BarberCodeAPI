using Microsoft.EntityFrameworkCore;
using BarberCode.Domain.Entities.Barbeiros;
using BarberCode.Infra.Banco;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using BarberCode.Service.Responses;
using BarberCode.Service.Requests;
namespace BarberCode.API.Endpoins;

public static class BarbeiroEndpoints
{
    public static void MapBarbeiroEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/barbearias/{barbeariaId}/Barbeiro").WithTags(nameof(Barbeiro));

        group.MapGet("/", async (BarberCodeContext db) =>
        {
            // TODO: Implementar GetAllBarbeiros
            // Retornar List<BarbeiroResponse>
            return await Task.FromResult(Enumerable.Empty<BarbeiroResponse>());
        })
        .WithName("GetAllBarbeiros")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<BarbeiroResponse>, NotFound>> (Guid id, BarberCodeContext db) =>
        {
            // TODO: Implementar GetBarbeiroById
            // Retornar BarbeiroResponse
            return TypedResults.NotFound();
        })
        .WithName("GetBarbeiroById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (Guid id, BarbeiroRequest request, BarberCodeContext db) =>
        {
            // TODO: Implementar UpdateBarbeiro
            // Receber BarbeiroRequest
            return TypedResults.NotFound();
        })
        .WithName("UpdateBarbeiro")
        .WithOpenApi();

        group.MapPost("/", async (BarbeiroRequest request, BarberCodeContext db) =>
        {
            // TODO: Implementar CreateBarbeiro
            // Receber BarbeiroRequest
            // Retornar BarbeiroResponse
            return TypedResults.Created($"/api/Barbeiro/{Guid.Empty}", new BarbeiroResponse(Guid.Empty, "", null));
        })
        .WithName("CreateBarbeiro")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (Guid id, BarberCodeContext db) =>
        {
            // TODO: Implementar DeleteBarbeiro
            return TypedResults.NotFound();
        })
        .WithName("DeleteBarbeiro")
        .WithOpenApi();
    }
}
