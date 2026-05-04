using AutoMapper;
using BarberCode.API.Models;
using BarberCode.Application.Interfaces;
using BarberCode.Application.UseCases.AuthAppUser;
using BarberCode.Domain.Shared;
using BarberCode.Service.Requests;
using BarberCode.Service.Responses;
using System.Security.Claims;
using System.Text;

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
		.Produces<ResultData<string>>(StatusCodes.Status200OK)
		.AddEndpointFilter<ValidationFilter<LoginRequest>>()
		.WithSummary("Login para gerenciadores de barbearia")
		.WithDescription("Autentica um gerenciador de barbearia e retorna um token JWT válido.");

		group.MapPost("/login-cliente", async (LoginClienteRequest request, LoginClienteUseCase useCase) =>
		{
			var result = await useCase.ExecuteAsync(request);

			return result.ToOkSingleResult();
		})
		.WithName("LoginCliente")
		.WithOpenApi()
		.Produces<ResultData<string>>(StatusCodes.Status200OK)
		.AddEndpointFilter<ValidationFilter<LoginClienteRequest>>()
		.WithSummary("Login para clientes")
		.WithDescription("Autentica um cliente do sistema e retorna um token JWT válido.");

		group.MapPost("/alterar-senha{token}", async (string email,string token ,AlterarSenhaRequest request, 
		AlterarSenhaUseCase useCase) =>
		{
			byte[] decodedBytes = Microsoft.AspNetCore.WebUtilities.WebEncoders.Base64UrlDecode(token);
			string tokenOriginal = Encoding.UTF8.GetString(decodedBytes);
			var result = await useCase.ExecuteAsync(email,tokenOriginal, request.SenhaNova);

			return result.ToOkSingleResult();
		})
		.WithName("AlterarSenha")
		.WithOpenApi()
		.Produces<ResultData<bool>>(StatusCodes.Status200OK)
		.AddEndpointFilter<ValidationFilter<AlterarSenhaRequest>>()
		.WithSummary("Altera a senha do usuário")
		.WithDescription("Permite que um usuário modifique sua senha utilizando um token de reset válido.");

		group.MapPost("/esqueci-senha", async (EsqueciSenhaRequest request, EsqueciSenhaUseCase useCase) =>
		{
			var result = await useCase.ExecuteAsync(request.Email);

			return result.ToOkSingleResult();
		})
		.WithName("EsqueciSenha")
		.WithOpenApi()
		.Produces<ResultData<bool>>(StatusCodes.Status200OK)
		.WithSummary("Solicita recuperação de senha")
		.WithDescription("Envia um email com um token de reset de senha para o usuário.");

		group.MapPost("/refresh-token", (RefreshTokenRequest request, RefreshTokenUseCase useCase) =>
		{
			var result = useCase.Execute(request.Token);

			return result.ToOkSingleResult();
		})
		.WithName("RefreshToken")
		.WithOpenApi()
		.Produces<ResultData<string>>(StatusCodes.Status200OK)
		.AddEndpointFilter<ValidationFilter<RefreshTokenRequest>>()
		.WithSummary("Renova o token de autenticação")
		.WithDescription("Gera um novo token JWT válido a partir de um refresh token, mantendo a sessão ativa.");

	}
}

