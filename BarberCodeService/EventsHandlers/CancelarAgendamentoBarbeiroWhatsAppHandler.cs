using BarberCode.Application.Interfaces;

namespace BarberCode.Application.EventsHandlers;

public record CancelarAgendamentoBarbeiroEvent(string ContatoCliente, string NomeCliente, string NomeBarbearia, 
	string NomeProfissional, DateOnly DataAgendamento, string BarbeariaSlug) : IAppEvent;

public class CancelarAgendamentoBarbeiroWhatsAppHandler : IEventHandler<CancelarAgendamentoBarbeiroEvent>
{
	private readonly IWhatsAppService _whatsAppService;

	public CancelarAgendamentoBarbeiroWhatsAppHandler(IWhatsAppService whatsAppService)
	{
		_whatsAppService = whatsAppService;
	}

	public async Task HandlerAsync(CancelarAgendamentoBarbeiroEvent ev)
	{
		var mensagem = _whatsAppService
			.GerarTemplateCancelarAgendamentoBarbeiro(ev.NomeBarbearia, ev.NomeCliente, ev.DataAgendamento, ev.NomeProfissional);

		await _whatsAppService.EnviarMensagemAsync(ev.ContatoCliente, mensagem, ev.BarbeariaSlug);
	}
}
