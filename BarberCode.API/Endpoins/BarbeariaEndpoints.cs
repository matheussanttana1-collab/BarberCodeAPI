using AutoMapper;
using BarberCode.API.Models;
using BarberCode.Application.Interfaces;
using BarberCode.Application.UseCases.Barbearias;
using BarberCode.Domain.Entities.Barbearias;
using BarberCode.Domain.Shared;
using BarberCode.Service.Requests;
using BarberCode.Service.Responses;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace BarberCode.API.Endpoins;


public static class BarbeariaEndpoints
{
	public static void MapBarbeariaEndpoints(this IEndpointRouteBuilder routes)
	{
		var group = routes.MapGroup("/api/Barbearia").WithTags(nameof(Barbearia)).RequireAuthorization("manager");
		group.MapGet("/", async (IBarbeariaRepository repo, IMapper mapper) =>
		{
			var barbearias = await repo.BuscarBarbeariasAsync();
			return ResultData<List<BarbeariaResponse>>.Success(mapper.Map<List<BarbeariaResponse>>(barbearias))
			.ToOkSingleResult();
		})
		.WithName("GetAllBarbearia")
		.WithOpenApi();

		group.MapGet("/{id}", async (Guid id, IBarbeariaRepository repo, IMapper mapper) =>
		{
			var barbearia = await repo.BuscarBarbeariaPorAsync(id);
			var dto = mapper.Map<BarbeariaResponse>(barbearia);

			return (barbearia is null
				? ResultData<BarbeariaResponse>.Failure(ResultType.NotFound, "Barbearia não encontrada")
				: ResultData<BarbeariaResponse>.Success(dto))
			.ToOkSingleResult();
		})
		.WithName("GetBarbeariaById")
		.WithOpenApi()
		.AllowAnonymous();

		group.MapPost("/", async (CriarBarbeariaRequest request, CriarBarbeariaUseCase useCase) =>
		{

			var result = await useCase.ExecuteAsync(request);
			return result.ToCreateResult($"/api/Barbearia/{result}");
		})
		.WithName("CreateBarbearia")
		.WithOpenApi()
		.AddEndpointFilter<ValidationFilter<CriarBarbeariaRequest>>()
		.AllowAnonymous();

		group.MapPost("/CadastrarWhatsApp", async (CadastrarWhatsApp useCase,
		ClaimsPrincipal user) =>
		{
			var id = user.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
			var result = await useCase.ExecuteAsync(Guid.Parse(id));

			return result.ToCreateResult($"/api/Barbearia/CadastrarWhatsApp/{result}");
		})
		.WithName("CadastrarWhatsApp")
		.WithOpenApi();


		group.MapPatch("/endereco", async (EnderecoRequest request, AlterarEnderecoUseCase useCase, 
		ClaimsPrincipal user) =>
		{
			var barbeariaId = user.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
			var result = await useCase.ExecuteAsync(Guid.Parse(barbeariaId), request);
			return result.ToNoContentResult();
		})
		.WithName("AlterarEndereco")
		.WithOpenApi()
		.AddEndpointFilter<ValidationFilter<EnderecoRequest>>();

		group.MapPatch("/funcionamento", async ( List<HorarioFuncionamentoRequest> request,
			AlterarHorarioFuncionamentoUseCase useCase, ClaimsPrincipal user) =>
		{
			var barbeariaId = user.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
			var result = await useCase.ExecuteAsync(Guid.Parse(barbeariaId), request);
			return result.ToNoContentResult();
		})
		.WithName("AlterarFuncionamento")
		.WithOpenApi()
		.AddEndpointFilter<ValidationFilter<List<HorarioFuncionamentoRequest>>>();

		group.MapDelete("/", async (DeletarBarbeariaUseCase useCase, ClaimsPrincipal user) =>
		{
			var barbeariaId = user.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
			var result = await useCase.ExecuteAsync(Guid.Parse(barbeariaId));
			return result.ToNoContentResult();
		})
		.WithName("DeleteBarbearia")
		.WithOpenApi();
	}


}
