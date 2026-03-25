using BarberCode.Domain.Entities.Barbearias;
using BarberCode.Service.Responses;
using BarberCode.Service.Requests;
using BarberCode.Application.Interfaces;
using AutoMapper;
using BarberCode.Application.UseCases.Barbearias;
using FluentValidation;
using BarberCode.API.Models;

namespace BarberCode.API.Endpoins;


public static class BarbeariaEndpoints
{
	public static void MapBarbeariaEndpoints(this IEndpointRouteBuilder routes)
	{
		var group = routes.MapGroup("/api/Barbearia").WithTags(nameof(Barbearia));
		group.MapGet("/", async (IBarbeariaRepository repo, IMapper mapper) =>
		{
			var barbearias = await repo.BuscarBarbeariasAsync();
			var barbeariasDto = mapper.Map<List<BarbeariaResponse>>(barbearias);
			return barbeariasDto.ToOkResult();
		})
		.WithName("GetAllBarbearia")
		.WithOpenApi();

		group.MapGet("/{id}", async (Guid id, IBarbeariaRepository repo, IMapper mapper) =>
		{
			var barbearia = await repo.BuscarBarbeariaPorAsync(id);
			var dto = mapper.Map<BarbeariaResponse>(barbearia);

			return barbearia.ToOkSingleResult();
		})
		.WithName("GetBarbeariaById")
		.WithOpenApi();

		group.MapPost("/", async (CriarBarbeariaRequest request, CriarBarbeariaUseCase useCase) =>
		{

			var result = await useCase.ExecuteAsync(request);
			return ResultsExtends.ToCreateResult(result, $"/api/Barbearias/{result}");
		})
		.WithName("CreateBarbearia")
		.WithOpenApi()
		.AddEndpointFilter<ValidationFilter<CriarBarbeariaRequest>>();

		group.MapPatch("/{id}/endereco", async (Guid id, EnderecoRequest request, AlterarEnderecoUseCase useCase) =>
		{
			var result = await useCase.ExecuteAsync(id, request);
			return result.ToNoContentResult();
		})
		.WithName("AlterarEndereco")
		.WithOpenApi()
		.AddEndpointFilter<ValidationFilter<EnderecoRequest>>();

		group.MapPatch("/{id}/funcionamento", async (Guid id, List<HorarioFuncionamentoRequest> request,
			AlterarHorarioFuncionamentoUseCase useCase) =>
		{
			var result = await useCase.ExecuteAsync(id, request);
			return result.ToNoContentResult();
		})
		.WithName("AlterarFuncionamento")
		.WithOpenApi()
		.AddEndpointFilter<ValidationFilter<List<HorarioFuncionamentoRequest>>>();

		group.MapDelete("/{id}", async (Guid id, DeletarBarbeariaUseCase useCase) =>
		{
			var result = await useCase.ExecuteAsync(id);
			return result.ToNoContentResult();
		})
		.WithName("DeleteBarbearia")
		.WithOpenApi();
	}
}
