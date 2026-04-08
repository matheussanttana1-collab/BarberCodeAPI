using BarberCode.Domain.Entities.Barbearias;
using BarberCode.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberCode.Application.Interfaces;

public interface IWhatsAppService
{
	public Task<ResultData> EnviarMensagem(string numero,string texto, string instance);

	public Task<ResultData<string>> GerarQrCodeDeCadastroWhatsApp(string instanceName);
	public Task<ResultData<string>> GerarNovoQrCodeWhatsApp(string instanceName);
  public Task<ResultData> LogoutWhatsAppBarbearia(string instanceName);
	public Task<ResultData<string>> BuscarStatusConexaoWhatsApp(string instanceName);
	public Task<ResultData> DeletarWhatsAppBarbearia(string instanceName);


	public string GerarTemplateConfirmacaoAgendamento(string nomeBarbearia, string nomeCliente, DateOnly dataAgendamento,
		string nomeProfissional, Endereco endereco);
}
