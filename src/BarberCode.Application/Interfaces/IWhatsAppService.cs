using BarberCode.Domain.Entities.Barbearias;
using BarberCode.Domain.Shared;


namespace BarberCode.Application.Interfaces;

public interface IWhatsAppService
{
	public Task<ResultData> EnviarMensagemAsync(string numero,string texto, string instance);

	public Task<ResultData<string>> CadastrarWhatsAppAsync(string instanceName);
	public Task<ResultData<string>> GerarNovoQrCodeWhatsAppAsync(string instanceName);
	public Task<ResultData> LogoutWhatsAppBarbeariaAsync(string instanceName);
	public Task<ResultData<string>> BuscarStatusConexaoWhatsAppAsync(string instanceName);
	public Task<ResultData> DeletarWhatsAppBarbeariaAsync(string instanceName);

	public string GerarTemplateConfirmacaoAgendamento(string nomeBarbearia, string nomeCliente, DateOnly dataAgendamento,
		string nomeProfissional, Endereco endereco);

	public string GerarTemplateConcluirAgendamento(string nomeBarbearia, string nomeCliente, DateOnly dataAgendamento,
		string nomeProfissional);

	public string GerarTemplateCancelarAgendamentoBarbeiro(string nomeBarbearia, string nomeCliente, DateOnly dataAgendamento,
		string nomeProfissional);

	public string GerarTemplateCancelarAgendamentoCliente(string nomeBarbearia, string nomeCliente, DateOnly dataAgendamento,
		string nomeProfissional);
}

