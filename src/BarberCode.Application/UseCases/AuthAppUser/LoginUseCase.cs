using BarberCode.Application.Interfaces;
using BarberCode.Domain.Shared;

namespace BarberCode.Application.UseCases.AuthAppUser;

/// <summary>
/// UseCase de Login para Barbearia
/// Orquestra a validação e geração de token usando métodos específicos do AppUserService
/// </summary>
public class LoginUseCase
{
	private readonly IAppUserService _userService;
	private readonly ITokenService _tokenService;

	public LoginUseCase(IAppUserService userService, ITokenService tokenService)
	{
		_userService = userService;
		_tokenService = tokenService;
	}

	/// <summary>
	/// Executa o login com fluxo de validações
	/// Fluxo:
	/// 1. ValidarEmail → busca usuário pelo email (retorna AuthUser)
	/// 2. ValidarSenha → valida a senha do usuário (retorna AuthUser)
	/// 3. ObterRoles → obtém as roles do usuário validado
	/// 4. GerarToken → gera token JWT via ITokenService
	/// </summary>
	public async Task<ResultData<string>> ExecuteAsync(string email, string senha)
	{
		var userValidado = await _userService.ValidarUsuarioAsync(email, senha);
		if (userValidado is null)
			return ResultData<string>.Failure(ResultType.Validation, "Email ou senha invalidos");
		// Obtém Roles - Busca as roles do usuário autenticado
		var roles = await _userService.ObterRolesAsync(userValidado);

		// Gera Token - Cria token JWT com claims de autenticação
		var token = _tokenService.GerarToken(userValidado, roles);

		// ✅ Retorna sucesso com token
		return ResultData<string>.Success(token);
	}
}
