using BarberCode.Application.Interfaces;

namespace BarberCode.Application.EventsHandlers;

public record EmailResetSenhaEvent(string Email, string Token) : IAppEvent;

public class EnviarEmailResetSenhaHandler : IEventHandler<EmailResetSenhaEvent>
{
	private readonly IEmailService _emailService;
	private readonly IEmailTemplateService _emailTemplateService;

	public EnviarEmailResetSenhaHandler(IEmailService emailService, IEmailTemplateService emailTemplateService)
	{
		_emailService = emailService;
		_emailTemplateService = emailTemplateService;
	}

	public async Task HandlerAsync(EmailResetSenhaEvent ev)
	{
		var body = _emailTemplateService.gerarTemplateResetSenha(ev.Email, ev.Token);
		await _emailService.SendEmailAsync(ev.Email, "Recuperação de Senha", body);
	}
}
