using AutoMapper;
using BarberCode.API.Models;
using BarberCode.Application.Interfaces;
using BarberCode.Application.UseCases.Barbearias;
using BarberCode.Domain.Entities.Barbearias;
using BarberCode.Domain.Shared;
using BarberCode.Service.Requests;
using BarberCode.Service.Responses;
using FluentValidation;
using System.Security.Claims;


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
		.WithOpenApi()
		.WithSummary("Lista todas as barbearias registradas")
		.WithDescription("Retorna a lista completa de todas as barbearias cadastradas no sistema.")
		.Produces<ResultData<List<BarbeariaResponse>>>(StatusCodes.Status200OK);

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
		.AllowAnonymous()
		.WithSummary("Obtém informações de uma barbearia específica")
		.WithDescription("Retorna os dados detalhados de uma barbearia (nome, endereço, telefone, serviços, etc.).")
		.Produces<ResultData<BarbeariaResponse>>(StatusCodes.Status200OK);

		group.MapPost("/", async (CriarBarbeariaRequest request, CriarBarbeariaUseCase useCase) =>
		{

			var result = await useCase.ExecuteAsync(request);
			return result.ToCreateResult($"/api/Barbearia/{result}");
		})
		.WithName("CreateBarbearia")
		.WithOpenApi()
		.AddEndpointFilter<ValidationFilter<CriarBarbeariaRequest>>()
		.AllowAnonymous()
		.WithSummary("Registra uma nova barbearia no sistema")
		.WithDescription("Permite o cadastro de uma nova barbearia com nome, endereço, telefone e horários de funcionamento.")
		.Produces(StatusCodes.Status201Created);

		group.MapPatch("/endereco", async (EnderecoRequest request, AlterarEnderecoUseCase useCase, 
		ClaimsPrincipal user) =>
		{
			var barbeariaId = user.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
			var result = await useCase.ExecuteAsync(Guid.Parse(barbeariaId), request);
			return result.ToNoContentResult();
		})
		.WithName("AlterarEndereco")
		.WithOpenApi()
		.AddEndpointFilter<ValidationFilter<EnderecoRequest>>()
		.WithSummary("Atualiza o endereço de uma barbearia")
		.WithDescription("Permite que o gerenciador modifique o endereço (rua, número, bairro, etc.) da barbearia.")
		.Produces(StatusCodes.Status204NoContent);

		group.MapPatch("/funcionamento", async ( List<HorarioFuncionamentoRequest> request,
			AlterarHorarioFuncionamentoUseCase useCase, ClaimsPrincipal user) =>
		{
			var barbeariaId = user.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
			var result = await useCase.ExecuteAsync(Guid.Parse(barbeariaId), request);
			return result.ToNoContentResult();
		})
		.WithName("AlterarFuncionamento")
		.WithOpenApi()
		.AddEndpointFilter<ValidationFilter<List<HorarioFuncionamentoRequest>>>()
		.WithSummary("Atualiza os horários de funcionamento")
		.WithDescription("Permite que o gerenciador modifique o horário de abertura e fechamento por dia da semana.")
		.Produces(StatusCodes.Status204NoContent);

		group.MapDelete("/", async (DeletarBarbeariaUseCase useCase, ClaimsPrincipal user) =>
		{
			var barbeariaId = user.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
			var result = await useCase.ExecuteAsync(Guid.Parse(barbeariaId));
			return result.ToNoContentResult();
		})
		.WithName("DeleteBarbearia")
		.WithOpenApi()
		.WithSummary("Remove uma barbearia do sistema")
		.WithDescription("Permite que o gerenciador delete sua barbearia e todos os seus dados associados.")
		.Produces(StatusCodes.Status204NoContent);
	}


}
