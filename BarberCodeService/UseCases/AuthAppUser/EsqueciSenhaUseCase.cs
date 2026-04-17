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
	private readonly IEmailTemplateService _emailTemplateService;

 public EsqueciSenhaUseCase(IAppUserService userService, IEmailService emailService,
	IEmailTemplateService emailTemplateService)
	{
		_userService = userService;
		_emailService = emailService;
       _emailTemplateService = emailTemplateService;
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
		var body = _emailTemplateService.gerarTemplateResetSenha(email, token);

		// 5️ Monta e envia o e-mail
		await _emailService.SendEmailAsync(email, "Recuperação de Senha", body);
		return ResultData<string>.Success("Email de recuperação enviado com sucesso");
	}
}
