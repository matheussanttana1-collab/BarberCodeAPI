using Microsoft.EntityFrameworkCore;
using BarberCode.Domain.Entities.Barbeiros;
using BarberCode.Infra.Banco;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using BarberCode.Service.Responses;
using BarberCode.Service.Requests;
using BarberCode.Application.UseCases.Barbeiros;
using BarberCode.Application.Interfaces;
using AutoMapper;
namespace BarberCode.API.Endpoins;

public static class BarbeiroEndpoints
{
    public static void MapBarbeiroEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/barbearia/{barbeariaId}/Barbeiro").WithTags(nameof(Barbeiro));

        group.MapGet("/", async (Guid barbeariaId, IBarbeiroRepository repo, IMapper mapper) =>
        {
            var barbeiros = repo.BuscarBarbeiros(barbeariaId);
            return await Task.FromResult(mapper.Map<List<BarbeiroResponse>>(barbeiros));
        })
        .WithName("GetAllBarbeiros")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<BarbeiroResponse>, NotFound>> (Guid id, IBarbeiroRepository repo
        , IMapper mapper) =>
        {
            var barbeiro = repo.BuscarBarbeiroPor(id);
            if (barbeiro is null)
            {
                return TypedResults.NotFound();
            }

            return TypedResults.Ok(mapper.Map<BarbeiroResponse>(barbeiro));
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

        group.MapPost("/", async (Guid barbeariaId, BarbeiroRequest request, CriarBarbeiroUseCase useCase) =>
        {
            useCase.Execute(request, barbeariaId);

            return Results.Created();
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
