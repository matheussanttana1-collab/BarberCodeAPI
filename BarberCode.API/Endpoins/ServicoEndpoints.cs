using AutoMapper;
using BarberCode.API.Models;
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

		group.MapGet("/", async (Guid barbeariaId, IServicoRepository repo, IMapper mapper) =>
		{
			var servicos = await repo.BuscarServicosAsync(barbeariaId);
			return Results.Ok(mapper.Map<List<ServicoResponse>>(servicos));
		})
		.WithName("GetAllServicos")
		.WithOpenApi();

		group.MapGet("/{id}", async (Guid id, IServicoRepository repo, IMapper mapper) =>
		{
			var servico = await repo.BuscarServicoPorAsync(id);
			if (servico is null)
				return Results.NotFound("Serviço não encontrado.");

			return Results.Ok(mapper.Map<ServicoResponse>(servico));
		})
		.WithName("GetServicoById")
		.WithOpenApi();

		group.MapPatch("/{id}", async (Guid id, AtualizarServicoRequest request, AlterarServicoUseCase useCase) =>
		{
			await useCase.ExecuteAsync(id, request);
			return Results.NoContent();
		})
		.WithName("UpdateServico")
		.WithOpenApi();

		group.MapPost("/", async (Guid barbeariaId, CriarServicoRequest request, CriarServicoUseCase useCase) =>
		{
			var id = await useCase.ExecuteAsync(barbeariaId, request);
			return Results.Created($"/api/Servicos/{id}", new { id });
		})
		.WithName("CreateServico")
		.WithOpenApi()
		.AddEndpointFilter<ValidationFilter<CriarServicoRequest>>(); ;

		group.MapDelete("/{id}", async (Guid id, DeletarServicoUseCase useCase) =>
		{
			await useCase.ExecuteAsync(id);
			return Results.NoContent();
		})
		.WithName("DeleteServico")
		.WithOpenApi();
	}
}
