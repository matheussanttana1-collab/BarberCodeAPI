using BarberCode.Application.Interfaces;
using BarberCode.Domain.Shared;

namespace BarberCode.Application.UseCases.AuthAppUser;

/// <summary>
/// UseCase para refresh de token
/// Valida o token anterior e gera um novo
/// </summary>
public class RefreshTokenUseCase
{
	private readonly ITokenService _tokenService;

	public RefreshTokenUseCase(ITokenService tokenService)
	{
		_tokenService = tokenService;
	}

	public ResultData<string> Execute(string token)
	{
		// 1️ Valida input
		if (string.IsNullOrWhiteSpace(token))
			return ResultData<string>.Failure(ResultType.Validation, "Token é obrigatório");

		// 2️ Valida e regenera o token
		var novoToken = _tokenService.RefreshToken(token);

		if (novoToken is null)
			return ResultData<string>.Failure(ResultType.Validation, "Token inválido ou expirado");

		// 3️ Retorna o novo token
		return ResultData<string>.Success(novoToken);
	}
}
