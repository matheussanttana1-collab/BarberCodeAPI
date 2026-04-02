using BarberCode.Application.Interfaces;
using BarberCode.Domain.Shared;
using BarberCode.Service.Requests;
using BarberCode.Service.Responses;

namespace BarberCode.Application.UseCases.AuthAppUser;

public class LoginClienteUseCase
{
	private readonly IAppUserService _appUserService;
	private readonly ITokenService _tokenService;
	private readonly IClienteRepository _clienteRepository;

	public LoginClienteUseCase(IAppUserService appUserService, ITokenService tokenService, IClienteRepository clienteRepository)
	{
		_appUserService = appUserService;
		_tokenService = tokenService;
		_clienteRepository = clienteRepository;
	}

	/// <summary>
	/// Realiza o login de um cliente usando celular e senha
	/// </summary>
	public async Task<ResultData<string>> ExecuteAsync(LoginClienteRequest request)
	{
		// 1️ Valida se celular foi informado
		if (string.IsNullOrWhiteSpace(request.Celular))
			return ResultData<string>.Failure(ResultType.Validation, "Celular é obrigatório");

		// 2️ Busca o cliente pelo celular
		var cliente = await _clienteRepository.BuscarClientePeloTelefoneAsync(request.Celular);
		if (cliente is null)
			return ResultData<string>.Failure(ResultType.NotFound, "Cliente não Cadastrado");

		// 3️ Reconstrói o email fake do cliente
		var emailFake = $"{request.Celular}@cliente.barbercode.local";

		// 4️ Valida o email (busca o AppUser)
		var authUser = await _appUserService.ValidarEmailAsync(emailFake);
		if (authUser is null)
			return ResultData<string>.Failure(ResultType.NotFound, "Usuário não encontrado");

		// 5 Valida a senha
		var senhaFinal = request.Senha ?? "Cliente@123";
		var usuarioValidado = await _appUserService.ValidarSenhaAsync(authUser, senhaFinal);
		if (usuarioValidado is null)
			return ResultData<string>.Failure(ResultType.Validation, "Credenciais inválidas");

		// 6️ Obtém as roles do usuário
		var roles = await _appUserService.ObterRolesAsync(usuarioValidado);

		// 7️ Gera o token JWT
		var token = _tokenService.GerarToken(usuarioValidado, roles);

		// 8️ Retorna a resposta com sucesso
		return ResultData<string>.Success(token);
	}
}
