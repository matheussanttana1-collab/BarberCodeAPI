using BarberCode.Application.Interfaces;

namespace BarberCode.Application.EventsHandlers;

public record EmailBoasVindasEvent(string Email, string Assunto, string NomeBarbearia, string? NomeBarbeiro = null) : IAppEvent;
public class EmailBoasVindasHandler : IEventHandler<EmailBoasVindasEvent>
{
	private readonly IEmailService _emailService;
	private readonly IEmailTemplateService _emailTemplateService;

	public EmailBoasVindasHandler(IEmailService emailService, IEmailTemplateService emailTemplateService)
	{
		_emailService = emailService;
		_emailTemplateService = emailTemplateService;
	}

	public async Task HandlerAsync(EmailBoasVindasEvent ev)
	{
		var body = string.Empty;
		if (ev.NomeBarbeiro is null)
			body = _emailTemplateService.gerarTemplateBoasVindasBarbearia(ev.NomeBarbearia);
		else
			body = _emailTemplateService.gerarTemplateBoasVindasBarbeiro(ev.NomeBarbeiro, ev.NomeBarbearia);

		await _emailService.SendEmailAsync(ev.Email, ev.Email, body);
	}
}
