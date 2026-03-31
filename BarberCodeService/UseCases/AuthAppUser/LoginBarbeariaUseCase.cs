using BarberCode.Application.Interfaces;
using BarberCode.Domain.Shared;

namespace BarberCode.Application.UseCases.AuthAppUser;

/// <summary>
/// UseCase de Login para Barbearia
/// Orquestra a validação e geração de token usando métodos específicos do AppUserService
/// </summary>
public class LoginBarbeariaUseCase
{
	private readonly IAppUserService _userService;
	private readonly ITokenService _tokenService;

	public LoginBarbeariaUseCase(IAppUserService userService, ITokenService tokenService)
	{
		_userService = userService;
		_tokenService = tokenService;
	}

	/// <summary>
	/// Executa o login com fluxo de validações
	/// Fluxo:
	/// 1. Valida entrada (email/senha não vazios)
	/// 2. ValidarEmail → busca usuário pelo email (retorna AuthUser)
	/// 3. ValidarSenha → valida a senha do usuário (retorna AuthUser)
	/// 4. ObterRoles → obtém as roles do usuário
	/// 5. GerarToken → gera token JWT via ITokenService
	/// </summary>
	public async Task<ResultData<string>> ExecuteAsync(string email, string senha)
	{
		// 1️⃣ Validação básica de entrada
		if (string.IsNullOrWhiteSpace(email))
			return ResultData<string>.Failure(ResultType.Validation, "Email é obrigatório");

		if (string.IsNullOrWhiteSpace(senha))
			return ResultData<string>.Failure(ResultType.Validation, "Senha é obrigatória");

		// 2️⃣ Valida Email - Busca o usuário pelo email
		var user = await _userService.ValidarEmailAsync(email);
		if (user is null)
			return ResultData<string>.Failure(ResultType.NotFound, "Email não encontrado ou não cadastrado");

		// 3️⃣ Valida Senha - Verifica se a senha está correta
		var userValidado = await _userService.ValidarSenhaAsync(user, senha);
		if (userValidado is null)
			return ResultData<string>.Failure(ResultType.Validation, "Senha inválida");

		// 4️⃣ Obtém Roles - Busca as roles do usuário autenticado
		var roles = await _userService.ObterRolesAsync(userValidado);

		// 5️⃣ Gera Token - Cria token JWT através de IAppUserService (que usa ITokenService)
		var token = _tokenService.GerarToken(userValidado, roles);

		// ✅ Retorna sucesso com token
		return ResultData<string>.Success(token);
	}
}
