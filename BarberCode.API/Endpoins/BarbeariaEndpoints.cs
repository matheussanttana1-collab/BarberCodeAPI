using BarberCode.Domain.Entities.Barbearias;
using BarberCode.Infra.Banco;
using Microsoft.AspNetCore.Http.HttpResults;
using BarberCode.Service.Responses;
using BarberCode.Service.Requests;
using BarberCode.Application.UseCases;
using BarberCode.Application.Interfaces;
using AutoMapper;

namespace BarberCode.API.Endpoins;


public static class BarbeariaEndpoints
{
    public static void MapBarbeariaEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Barbearia").WithTags(nameof(Barbearia));

        group.MapGet("/", async (IBarbeariaRepository repo, IMapper mapper) =>
        {
            var barbearias = repo.BuscarBarbearias();

            return mapper.Map<List<BarbeariaResponse>>(barbearias);
        })
        .WithName("GetAllBarbearia")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<BarbeariaResponse>, NotFound>> (Guid id,
        IBarbeariaRepository repo, IMapper mapper) =>
        {
            var barbearia = repo.BuscarBarbeariaPor(id);
            if (barbearia is null)
            {
                return TypedResults.NotFound();
            }

            return TypedResults.Ok(mapper.Map<BarbeariaResponse>(barbearia));
        })
        .WithName("GetBarbeariaById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (Guid id, BarbeariaRequest request) =>
        {
            // TODO: Implementar UpdateBarbearia
            // Receber BarbeariaRequest
            return TypedResults.NotFound();
        })
        .WithName("UpdateBarbearia")
        .WithOpenApi();

        group.MapPost("/", async (BarbeariaRequest request, CriarBarbeariaUseCase useCase) =>
        {
            useCase.Execute(request);

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
