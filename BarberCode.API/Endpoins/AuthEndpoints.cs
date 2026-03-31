using AutoMapper;
using BarberCode.API.Models;
using BarberCode.Application.Interfaces;
using BarberCode.Application.UseCases.AuthAppUser;
using BarberCode.Service.Requests;
using BarberCode.Service.Responses;

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
		.Produces<LoginResponse>(StatusCodes.Status200OK)
		.Produces(StatusCodes.Status400BadRequest)
		.AddEndpointFilter<ValidationFilter<LoginRequest>>();
	}
}
