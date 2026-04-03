using BarberCode.Application.Interfaces;
using BarberCode.Domain.Shared;

namespace BarberCode.Application.UseCases.AuthAppUser;

/// <summary>
/// UseCase para esqueci de senha
/// Reseta a senha para um padrão temporário
/// </summary>
public class EsqueciSenhaUseCase
{
	private readonly IAppUserService _userService;

	public EsqueciSenhaUseCase(IAppUserService userService)
	{
		_userService = userService;
	}

	public async Task<ResultData<string>> ExecuteAsync(string email)
	{
		// 1️ Valida input
		if (string.IsNullOrWhiteSpace(email))
			return ResultData<string>.Failure(ResultType.Validation, "Email é obrigatório");

		// 2️ Busca o usuário pelo email
		var user = await _userService.BuscarPeloEmailAsync(email);
		if (user is null)
			return ResultData<string>.Failure(ResultType.NotFound, "Email não encontrado");

		// 3️ Reseta a senha para um padrão temporário
		var token = await _userService.GerarTokenDeResetSenhaAsync(user);
		return ResultData<string>.Success(token);
	}
}
