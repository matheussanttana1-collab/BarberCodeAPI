using BarberCode.Application.Interfaces;

namespace BarberCode.Application.EventsHandlers;

public record CancelarAgendamentoClienteEvent(
	string ContatoCliente, 
	string NomeCliente, 
	string NomeBarbearia, 
	string NomeProfissional, 
	DateOnly DataAgendamento, 
	string BarbeariaSlug) : IAppEvent;

public class CancelarAgendamentoClienteWhatsAppHandler : IEventHandler<CancelarAgendamentoClienteEvent>
{
	private readonly IWhatsAppService _whatsAppService;

	public CancelarAgendamentoClienteWhatsAppHandler(IWhatsAppService whatsAppService)
	{
		_whatsAppService = whatsAppService;
	}

	public async Task HandlerAsync(CancelarAgendamentoClienteEvent ev)
	{
		var mensagem = _whatsAppService
			.GerarTemplateCancelarAgendamentoCliente(ev.NomeBarbearia, ev.NomeCliente, ev.DataAgendamento, ev.NomeProfissional);
		
		await _whatsAppService.EnviarMensagemAsync(ev.ContatoCliente, mensagem, ev.BarbeariaSlug);
	}
}
