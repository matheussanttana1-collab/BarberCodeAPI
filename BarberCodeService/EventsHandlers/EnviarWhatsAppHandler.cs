
using BarberCode.Application.Interfaces;

namespace BarberCode.Application.EventsHandlers;

public record EnviarMensagemEvent(string ContatoCliente, string Mensagem, string BarbeariaSlug) : IAppEvent;
public class EnviarWhatsAppHandler : IEventHandler<EnviarMensagemEvent>
{
	private readonly IWhatsAppService _whatsAppService;

	public EnviarWhatsAppHandler(IWhatsAppService whatsAppService)
	{
		_whatsAppService = whatsAppService;
	}

	public async Task HandlerAsync(EnviarMensagemEvent ev)
	{
		await _whatsAppService.EnviarMensagemAsync(ev.ContatoCliente, ev.Mensagem, ev.BarbeariaSlug);
	}
}
