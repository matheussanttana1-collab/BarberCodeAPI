using AutoMapper;
using BarberCode.API.Models;
using BarberCode.Application.Interfaces;
using BarberCode.Application.UseCases.AuthAppUser;
using BarberCode.Service.Requests;
using BarberCode.Service.Responses;
using System.Security.Claims;

namespace BarberCode.API.Endpoins;

public static class AuthEndpoints
{
	public static void MapAuthEndpoints(this IEndpointRouteBuilder routes)
	{
		var group = routes.MapGroup("/api/Auth").WithTags("Auth");

		group.MapPost("/login", async (LoginRequest request, LoginUseCase useCase) =>
		{
			var result = await useCase.ExecuteAsync(request.Email, request.Senha);

			return result.ToOkSingleResult();
		})
		.WithName("LoginBarbearia")
		.WithOpenApi()
		.Produces<string>(StatusCodes.Status200OK)
		.Produces(StatusCodes.Status400BadRequest)
		.AddEndpointFilter<ValidationFilter<LoginRequest>>();

		group.MapPost("/login-cliente", async (LoginClienteRequest request, LoginClienteUseCase useCase) =>
		{
			var result = await useCase.ExecuteAsync(request);

			return result.ToOkSingleResult();
		})
		.WithName("LoginCliente")
		.WithOpenApi()
		.Produces<string>(StatusCodes.Status200OK)
		.Produces(StatusCodes.Status400BadRequest)
		.Produces(StatusCodes.Status404NotFound)
		.AddEndpointFilter<ValidationFilter<LoginClienteRequest>>();

		group.MapPost("/alterar-senha", async (string email, string token, AlterarSenhaRequest request, AlterarSenhaUseCase useCase) =>
		{
			var result = await useCase.ExecuteAsync(email, token, request.SenhaNova);

			return result.ToOkSingleResult();
		})
		.WithName("AlterarSenha")
		.WithOpenApi()
		.AddEndpointFilter<ValidationFilter<AlterarSenhaRequest>>();

		group.MapPost("/esqueci-senha", async (EsqueciSenhaRequest request, EsqueciSenhaUseCase useCase) =>
		{
			var result = await useCase.ExecuteAsync(request.Email);

			return result.ToOkSingleResult();
		})
		.WithName("EsqueciSenha")
		.WithOpenApi();

		group.MapPost("/refresh-token", (RefreshTokenRequest request, RefreshTokenUseCase useCase) =>
		{
			var result = useCase.Execute(request.Token);

			return result.ToOkSingleResult();
		})
		.WithName("RefreshToken")
		.WithOpenApi()
		.Produces<string>(StatusCodes.Status200OK)
		.Produces(StatusCodes.Status400BadRequest)
		.AddEndpointFilter<ValidationFilter<RefreshTokenRequest>>();

	}
}

