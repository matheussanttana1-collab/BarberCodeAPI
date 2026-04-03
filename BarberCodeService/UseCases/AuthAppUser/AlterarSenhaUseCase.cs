using BarberCode.Application.Interfaces;
using BarberCode.Domain.Shared;

namespace BarberCode.Application.UseCases.AuthAppUser;

/// <summary>
/// UseCase para alterar senha do usuário
/// </summary>
public class AlterarSenhaUseCase
{
	private readonly IAppUserService _userService;

	public AlterarSenhaUseCase(IAppUserService userService)
	{
		_userService = userService;
	}

	public async Task<ResultData> ExecuteAsync(string email, string token, string novaSenha)
	{
		var user = await _userService.BuscarPeloEmailAsync(email);
		if (user is null)
			return ResultData.Failure(ResultType.NotFound, "Usuario Não Encontrado");

		var result = await _userService.AlterarSenhaAsync(user, token, novaSenha);

		return result.IsSuccess
		? ResultData.Success()
		: ResultData.Failure(result.Type, result.Message);
	}
}
