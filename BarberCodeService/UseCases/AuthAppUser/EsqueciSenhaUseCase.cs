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
	private readonly IEmailService _emailService;

	public EsqueciSenhaUseCase(IAppUserService userService, IEmailService emailService)
	{
		_userService = userService;
		_emailService = emailService;
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

		var token = await _userService.GerarTokenDeResetSenhaAsync(user);


		var tokenCodificado = Uri.EscapeDataString(token);
		var emailCodificado = Uri.EscapeDataString(email);

		var linkReset = $"https://localhost:7050/api/auth/alterar-senha/{tokenCodificado}?email={emailCodificado}";

		// 5️ Monta e envia o e-mail
		await _emailService.sendEmailAsync(
			email,
			"Recuperação de Senha",
			$"<h1>Olá!</h1>" +
			$"<p>Você solicitou a alteração da sua senha. Para criar uma nova, clique no link abaixo:</p>" +
			$"<a href='{linkReset}'>Redefinir minha senha</a>"
		);
		return ResultData<string>.Success("Email de recuperação enviado com sucesso");
	}
}
