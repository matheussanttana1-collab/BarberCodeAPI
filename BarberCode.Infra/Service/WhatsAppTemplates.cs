using BarberCode.Domain.Entities.Barbearias;

namespace BarberCode.Infra.Service;

public static class WhatsAppTemplates
{
	public static string GerarTemplateConfirmacaoAgendamento(
		string nomeBarbearia,
		string nomeCliente,
		DateOnly dataAgendamento,
		string nomeProfissional,
		Endereco endereco)
	{
		return
			$"Lembrete de agendamento\n\n" +
			$"{nomeBarbearia}.\n\n" +
			$"Olá {nomeCliente}.\n\n" +
			$"Estamos passando para lembrar de seu agendamento - {dataAgendamento:dd/MM/yyyy} com profissional {nomeProfissional}.\n" +
			$"Se houver qualquer imprevisto, peço que nos avise com antecedência, é muito importante.\n\n" +
			$"{endereco.Logradouro}, N°{endereco.Numero}, {endereco.Bairro}, {endereco.Cidade}-{endereco.Estado}. Obrigado pela preferencia!";
	}
}
