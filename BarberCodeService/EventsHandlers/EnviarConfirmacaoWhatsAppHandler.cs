
using BarberCode.Application.Interfaces;
using BarberCode.Domain.Entities.Barbearias;

namespace BarberCode.Application.EventsHandlers;

public record EnviarMensagemEvent(string ContatoCliente,string BarbeariaSlug,string NomeBarbearia, string NomeCliente,
DateOnly DataAgendamento,string NomeProfissional, Endereco Endereco) : IAppEvent;
public class EnviarConfirmacaoWhatsAppHandler : IEventHandler<EnviarMensagemEvent>
{
	private readonly IWhatsAppService _whatsAppService;

	public EnviarConfirmacaoWhatsAppHandler(IWhatsAppService whatsAppService)
	{
		_whatsAppService = whatsAppService;
	}

	public async Task HandlerAsync(EnviarMensagemEvent ev)
	{
		var body = _whatsAppService.GerarTemplateConfirmacaoAgendamento(ev.NomeBarbearia, ev.NomeCliente,
		ev.DataAgendamento, ev.NomeProfissional, ev.Endereco);
		await _whatsAppService.EnviarMensagemAsync(ev.ContatoCliente, body, ev.BarbeariaSlug);
	}
}
