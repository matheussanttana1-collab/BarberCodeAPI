using BarberCode.Domain.Entities.Barbearias;

namespace BarberCode.Infra.Service.WhatsAppServices;

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

	public static string GerarTemplateConcluirAgendamento(
		string nomeBarbearia,
		string nomeCliente,
		DateOnly dataAgendamento,
		string nomeProfissional)
	{
		return
			$"Agendamento Concluído\n\n" +
			$"{nomeBarbearia}.\n\n" +
			$"Olá {nomeCliente}.\n\n" +
			$"Agradecemos a visita! Seu agendamento com {nomeProfissional} em {dataAgendamento:dd/MM/yyyy} foi concluído com sucesso.\n" +
			$"Esperamos te ver em breve! 😊\n\n" +
			$"Obrigado pela preferência!";
	}

	public static string GerarTemplateCancelarAgendamentoBarbeiro(
		string nomeBarbearia,
		string nomeCliente,
		DateOnly dataAgendamento,
		string nomeProfissional)
	{
		return
			$"Agendamento Cancelado\n\n" +
			$"{nomeBarbearia}.\n\n" +
			$"Olá {nomeCliente}.\n\n" +
			$"Informamos que seu agendamento com {nomeProfissional} em {dataAgendamento:dd/MM/yyyy} foi cancelado pelo profissional.\n" +
			$"Entre em contato conosco para remarcar sua data. Aguardamos você!\n\n" +
			$"Obrigado!";
	}

	public static string GerarTemplateCancelarAgendamentoCliente(
		string nomeBarbearia,
		string nomeCliente,
		DateOnly dataAgendamento,
		string nomeProfissional)
	{
		return
			$"Cancelamento de Agendamento\n\n" +
			$"{nomeBarbearia}.\n\n" +
			$"Olá {nomeCliente}.\n\n" +
			$"Confirmamos o cancelamento do seu agendamento com {nomeProfissional} em {dataAgendamento:dd/MM/yyyy}.\n" +
			$"Caso queira remarcar, será um prazer te atender novamente!\n\n" +
			$"Obrigado!";
	}
}
