using AutoMapper;
using BarberCode.API.Models;
using BarberCode.Application.Interfaces;
using BarberCode.Application.Requests;
using BarberCode.Application.UseCases.Servicos;
using BarberCode.Domain.Entities.Barbearias;
using BarberCode.Domain.Shared;
using BarberCode.Service.Requests;
using BarberCode.Service.Responses;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
namespace BarberCode.API.Endpoins;

public static class ServicoEndpoints
{
	public static void MapServicoEndpoints(this IEndpointRouteBuilder routes)
	{
		var group = routes.MapGroup("/api/Barbearia/{barbeariaId}/Servico").WithTags(nameof(Servico))
		.RequireAuthorization("manager");

		group.MapGet("/", async (Guid barbeariaId,IServicoRepository repo, IMapper mapper) =>
		{
			
			var servicos = await repo.BuscarServicosAsync(barbeariaId);
			return ResultData<List<ServicoResponse>>.Success(mapper.Map<List<ServicoResponse>>(servicos))
			.ToOkSingleResult();
		})
		.WithName("GetAllServicos")
		.WithOpenApi()
		.AllowAnonymous();

		group.MapGet("/{id}", async (Guid id, IServicoRepository repo, IMapper mapper) =>
		{
			var servico = await repo.BuscarServicoPorAsync(id);

			return (servico is null
				? ResultData<ServicoResponse>.Failure(ResultType.NotFound, "Serviço não encontrado")
				: ResultData<ServicoResponse>.Success(mapper.Map<ServicoResponse>(servico)))
			.ToOkSingleResult();
		})
		.WithName("GetServicoById")
		.WithOpenApi()
		.AllowAnonymous();

		group.MapPatch("/{id}", async (Guid id, AtualizarServicoRequest request, AlterarServicoUseCase useCase) =>
		{
			var result = await useCase.ExecuteAsync(id, request);
			return result.ToNoContentResult();
		})
		.WithName("UpdateServico")
		.WithOpenApi()
		.AddEndpointFilter<ValidationFilter<AtualizarServicoRequest>>();

		group.MapPost("/", async (CriarServicoRequest request, CriarServicoUseCase useCase, ClaimsPrincipal user) =>
		{
			var barbeariaId = user.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
			var result = await useCase.ExecuteAsync(Guid.Parse(barbeariaId), request);
			return result.ToCreateResult($"/api/Servicos/{result}");
		})
		.WithName("CreateServico")
		.WithOpenApi()
		.AddEndpointFilter<ValidationFilter<CriarServicoRequest>>(); ;

		group.MapDelete("/{id}", async (Guid id, DeletarServicoUseCase useCase) =>
		{
			var result = await useCase.ExecuteAsync(id);
			return result.ToNoContentResult();
		})
		.WithName("DeleteServico")
		.WithOpenApi();
	}
}
