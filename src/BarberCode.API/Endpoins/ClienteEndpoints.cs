using AutoMapper;
using BarberCode.API.Models;
using BarberCode.Application.Interfaces;
using BarberCode.Application.UseCases.Agendamentos;
using BarberCode.Service.Responses;
using BarberCode.Domain.Shared;
using System.Security.Claims;
namespace BarberCode.API.Endpoins;

public static class ClienteEndpoints
{
    public static void MapClienteInfoEndpoints (this IEndpointRouteBuilder routes)
    {
		var group = routes.MapGroup("/api/cliente").WithTags("Cliente");

		group.MapGet("/barbearia", async (IClienteRepository repo, IMapper mapper, ClaimsPrincipal user) =>
		{
			var barbeariaId = user.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
			var clientes = await repo.BuscarClientesAsync(Guid.Parse(barbeariaId));
			return ResultData<List<ClienteInfoResponse>>.Success(mapper.Map<List<ClienteInfoResponse>>(clientes))
			.ToOkSingleResult();
		})
		.WithName("GetAllClienteInfos")
		.WithOpenApi()
		.RequireAuthorization("manager");

		group.MapGet("/{id}", async (Guid id, IClienteRepository repo, IMapper mapper) =>
		{
			var cliente = await repo.BuscarClientePorIdAsync(id);

			return (cliente is null
				? ResultData<ClienteInfoResponse>.Failure(ResultType.NotFound, "Cliente não encontrado")
				: ResultData<ClienteInfoResponse>.Success(mapper.Map<ClienteInfoResponse>(cliente)))
			.ToOkSingleResult();
		})
		.WithName("GetClienteInfoById")
		.WithOpenApi()
		.RequireAuthorization("manager");

		group.MapGet("/me", async (IClienteRepository repo, IMapper mapper, ClaimsPrincipal user) =>
		{
			var clienteId = user.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
			var cliente = await repo.BuscarClientePorIdAsync(Guid.Parse(clienteId));

			return (cliente is null
				? ResultData<ClienteInfoResponse>.Failure(ResultType.NotFound, "Cliente não encontrado")
				: ResultData<ClienteInfoResponse>.Success(mapper.Map<ClienteInfoResponse>(cliente)))
			.ToOkSingleResult();
		})
		.WithName("GetMeInfoById")
		.WithOpenApi()
		.RequireAuthorization("user");



		group.MapGet("/agendamentos", async (IAgendamentoRepository repo, IMapper mapper
		, ClaimsPrincipal user) =>
		{
			var clienteId = user.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
			var agendamentos = await repo.BuscarAgendamentosDoClienteAsync(Guid.Parse(clienteId));

			return ResultData<List<AgendamentoResponse>>.Success(mapper.Map<List<AgendamentoResponse>>
			(agendamentos)).ToOkSingleResult();
		})
		.WithName("AgendamentosDoCliente")
		.WithOpenApi()
		.RequireAuthorization("user");

		group.MapDelete("/agendamentos", async ( Guid agendamentoId,
		CancelarAgendamentoClienteUseCase useCase, ClaimsPrincipal user) =>
		{
			var clienteId = user.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
			var result = await useCase.ExecuteAsync(agendamentoId, Guid.Parse(clienteId));
			return result.ToNoContentResult();
		})
		.WithName("CancelarAgendamentoCliente")
		.WithOpenApi()
		.RequireAuthorization("user");

		//group.MapDelete("/{id}", (Guid id) =>
		//      {
		//          //return TypedResults.Ok(new ClienteInfo { ID = id });
		//      })
		//      .WithName("DeleteClienteInfo")
		//      .WithOpenApi();
	}
}
