using BarberCode.Application.Interfaces;

namespace BarberCode.Application.EventsHandlers;

public record ConcluirAgendamentoEvent(string ContatoCliente, string NomeCliente, string NomeBarbearia, 
	string NomeProfissional, DateOnly DataAgendamento, string BarbeariaSlug) : IAppEvent;

public class ConcluirAgendamentoWhatsAppHandler : IEventHandler<ConcluirAgendamentoEvent>
{
	private readonly IWhatsAppService _whatsAppService;

	public ConcluirAgendamentoWhatsAppHandler(IWhatsAppService whatsAppService)
	{
		_whatsAppService = whatsAppService;
	}

	public async Task HandlerAsync(ConcluirAgendamentoEvent ev)
	{
		var mensagem = _whatsAppService
			.GerarTemplateConcluirAgendamento(ev.NomeBarbearia, ev.NomeCliente, ev.DataAgendamento, ev.NomeProfissional);

		await _whatsAppService.EnviarMensagemAsync(ev.ContatoCliente, mensagem, ev.BarbeariaSlug);
	}
}
