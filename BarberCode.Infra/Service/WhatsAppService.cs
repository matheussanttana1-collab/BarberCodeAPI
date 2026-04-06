using BarberCode.Application.Interfaces;
using BarberCode.Domain.Entities.Barbearias;
using BarberCode.Domain.Shared;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Text.Json;


namespace BarberCode.Infra.Service;

public class WhatsAppService : IWhatsAppService
{
	private readonly IConfiguration _configuration;
	private readonly HttpClient _httpClient;

	public WhatsAppService(IConfiguration configuration, HttpClient httpClient)
	{
		_configuration = configuration;
		_httpClient = httpClient;
	}

	/// <summary>
	/// Envia uma mensagem de texto via WhatsApp utilizando a Evolution API.
	/// </summary>
	/// <param name="numero">Número do destinatário com DDI e DDD (Ex: 5511999999999).</param>
	/// <param name="texto">Conteúdo da mensagem a ser enviada.</param>
	/// <param name="instance">Nome da instância da barbearia conectada no WhatsApp.</param>
	/// <returns>ResultData indicando sucesso ou falha na operação.</returns>
	public async Task<ResultData> EnviarMensagem(string numero, string texto, string instance)
	{
		// Recupera as configurações globais de BaseUrl (Docker/AWS) e a API Key mestra
		var baseUrl = _configuration["WhatsApp:BaseUrl"];
		var apiKey = _configuration["WhatsApp:ApiKey"];

		if (string.IsNullOrWhiteSpace(baseUrl) || string.IsNullOrWhiteSpace(apiKey))
			return ResultData.Failure(ResultType.Failure, "Configurações do WhatsApp não encontradas (BaseUrl/ApiKey).");

		if (string.IsNullOrWhiteSpace(instance))
			return ResultData.Failure(ResultType.Failure, "Instância do WhatsApp não informada.");

		// Monta a URL dinâmica injetando o nome da instância da barbearia específica
		// Correção: Adicionada a '/' antes de {instance} para o endpoint correto
		var url = $"{baseUrl}/message/sendText/{instance}";

		// Cria o objeto anônimo que a Evolution API espera receber
       var payload = new
		{
			number = $"55{numero}",
			textMessage = new
			{
				text = texto
			},
			options = new
			{
				delay = 1200,
				linkPreview = true
			}
		};

		// Prepara a requisição HTTP do tipo POST
		var request = new HttpRequestMessage(HttpMethod.Post, url);

		// Adiciona a chave de segurança no cabeçalho (Header) da requisição
		request.Headers.Add("apikey", apiKey);

		// Serializa o payload para JSON e define o encoding UTF8
		request.Content = new StringContent(
			JsonSerializer.Serialize(payload),
			Encoding.UTF8,
			"application/json"
		);

		// Dispara a chamada assíncrona para o servidor da Evolution API
		var response = await _httpClient.SendAsync(request);

		// Verifica se o status code da resposta indica sucesso (200-299)
		if (!response.IsSuccessStatusCode)
		{
			// Caso falhe, captura o erro retornado pela API externa para diagnóstico
			var error = await response.Content.ReadAsStringAsync();

			// Retorna o objeto de falha seguindo o Result Pattern do projeto
			return ResultData.Failure(ResultType.Failure, $"Erro ao enviar WhatsApp: {error}");
		}

		// Retorna sucesso caso a mensagem tenha sido aceita pela fila da Evolution API
		return ResultData.Success();
	}

	public string GerarTemplateConfirmacaoAgendamento(string nomeBarbearia,string nomeCliente,DateOnly dataAgendamento,
	string nomeProfissional,Endereco endereco)
	{
		return WhatsAppTemplates.GerarTemplateConfirmacaoAgendamento(nomeBarbearia,nomeCliente,dataAgendamento,
			nomeProfissional,endereco);
	}
}
