using BarberCode.Domain.Entities.Barbeiros;
using Microsoft.AspNetCore.Http.HttpResults;
using BarberCode.Service.Responses;
using BarberCode.Service.Requests;
using BarberCode.Application.UseCases.Barbeiros;
using BarberCode.Application.Interfaces;
using AutoMapper;
using BarberCode.Application.Responses;
using BarberCode.Application.Requests;
namespace BarberCode.API.Endpoins;

public static class BarbeiroEndpoints
{
    public static void MapBarbeiroEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Barbeiro").WithTags(nameof(Barbeiro));

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

        group.MapGet("/{id}/Slots", async Task<Results<Ok<GerarSlotsResponse>, NotFound>> (Guid id, 
        Guid barbeariaId,DateOnly diaEscolhido, Guid servicoId, GerarSlotsUseCase useCase) =>
        {
           var slots = useCase.Execute(id, barbeariaId, servicoId, diaEscolhido);

            return TypedResults.Ok(slots);
        })
        .WithName("GerarSlotsBarbeiro")
        .WithOpenApi();

        group.MapPatch("/{id}", async Task<Results<NoContent, NotFound>> (Guid id, AtualizarBarbeiroRequest request,
        AlterarBarbeiroUseCase useCase) =>
        {
            useCase.Execute(id, request);
            return TypedResults.NoContent();
        })
        .WithName("UpdateBarbeiro")
        .WithOpenApi();

        group.MapPost("barbearia/{barbeariaId}/", async (Guid barbeariaId, BarbeiroRequest request, CriarBarbeiroUseCase useCase) =>
        {
            useCase.Execute(request, barbeariaId);

            return Results.Created();
        })
        .WithName("CreateBarbeiro")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<NoContent, NotFound>> (Guid id, DeletarBarbeiroUseCase useCase) =>
        {
            useCase.Execute(id);
            return TypedResults.NoContent();
        })
        .WithName("DeleteBarbeiro")
        .WithOpenApi();
    }
}
