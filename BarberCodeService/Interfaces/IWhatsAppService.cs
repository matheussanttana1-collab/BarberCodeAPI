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

	public string GerarTemplateConfirmacaoAgendamento(string nomeBarbearia, string nomeCliente, DateOnly dataAgendamento,
		string nomeProfissional, Endereco endereco);
}
