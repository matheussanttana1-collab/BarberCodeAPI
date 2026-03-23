using BarberCode.Domain.Entities.Barbearias;
using BarberCode.Service.Responses;
using BarberCode.Service.Requests;
using BarberCode.Application.Interfaces;
using AutoMapper;
using BarberCode.Application.UseCases.Barbearias;

namespace BarberCode.API.Endpoins;


public static class BarbeariaEndpoints
{
	public static void MapBarbeariaEndpoints(this IEndpointRouteBuilder routes)
	{
		var group = routes.MapGroup("/api/Barbearia").WithTags(nameof(Barbearia));
		group.MapGet("/", async (IBarbeariaRepository repo, IMapper mapper) =>
		{
			var barbearias = await repo.BuscarBarbeariasAsync();
			return Results.Ok(mapper.Map<List<BarbeariaResponse>>(barbearias));
		})
		.WithName("GetAllBarbearia")
		.WithOpenApi();

		group.MapGet("/{id}", async (Guid id, IBarbeariaRepository repo, IMapper mapper) =>
		{
			var barbearia = await repo.BuscarBarbeariaPorAsync(id);
			if (barbearia is null)
				return Results.NotFound("Barbearia não encontrada.");

			return Results.Ok(mapper.Map<BarbeariaResponse>(barbearia));
		})
		.WithName("GetBarbeariaById")
		.WithOpenApi();

		group.MapPost("/", async (BarbeariaRequest request, CriarBarbeariaUseCase useCase) =>
		{
			var id = await useCase.ExecuteAsync(request);
			return Results.Created($"/api/Barbearias/{id}", new { id });
		})
		.WithName("CreateBarbearia")
		.WithOpenApi();

		group.MapPatch("/{id}/endereco", async (Guid id, EnderecoRequest request, AlterarEnderecoUseCase useCase) =>
		{
			await useCase.ExecuteAsync(id, request);
			return Results.NoContent();
		})
		.WithName("AlterarEndereco")
		.WithOpenApi();

		group.MapPatch("/{id}/funcionamento", async (Guid id, List<HorarioFuncionamentoRequest> request,
			AlterarHorarioFuncionamentoUseCase useCase) =>
		{
			await useCase.ExecuteAsync(id, request);
			return Results.NoContent();
		})
		.WithName("AlterarFuncionamento")
		.WithOpenApi();

		group.MapDelete("/{id}", async (Guid id, DeletarBarbeariaUseCase useCase) =>
		{
			await useCase.ExecuteAsync(id);
			return Results.NoContent();
		})
		.WithName("DeleteBarbearia")
		.WithOpenApi();
	}
}
