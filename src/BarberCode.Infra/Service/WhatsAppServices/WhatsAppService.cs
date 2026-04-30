using BarberCode.Application.Interfaces;
using BarberCode.Domain.Entities.Barbearias;
using BarberCode.Domain.Shared;
using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;


namespace BarberCode.Infra.Service.WhatsAppServices;

public class WhatsAppService : IWhatsAppService
{
	private readonly HttpClient _httpClient;

	public WhatsAppService(HttpClient httpClient)
	{
		_httpClient = httpClient;
	}

	public async Task<ResultData> EnviarMensagemAsync(string numero, string texto, string instance)
	{
		var url = $"/message/sendText/{instance}";

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

		var request = new HttpRequestMessage(HttpMethod.Post, url);

		request.Content = new StringContent(
			JsonSerializer.Serialize(payload),
			Encoding.UTF8,
			"application/json"
		);

		var response = await _httpClient.SendAsync(request);

		if (!response.IsSuccessStatusCode)
		{
			var error = await response.Content.ReadAsStringAsync();
			var errorData = JsonSerializer.Deserialize<EvolutionErrorReponse>(error);
			return ResultData.Failure(ResultType.Failure, $"Erro ao enviar WhatsApp: " +
			$"{errorData.Error.First()}");
		}

		return ResultData.Success();
	}

	public async Task<ResultData<string>> CadastrarWhatsAppAsync(string instanceName)
	{
		var url = "/instance/create";
		var payload = new
		{
			instanceName = instanceName,
			token = Guid.NewGuid().ToString(),
			qrcode = true
		};

		var request = new HttpRequestMessage(HttpMethod.Post, url);
		request.Content = new StringContent(JsonSerializer.Serialize(payload),
		Encoding.UTF8, "application/json");

		var response = await _httpClient.SendAsync(request);
		if (!response.IsSuccessStatusCode)
		{
			var error = await response.Content.ReadAsStringAsync();
			var errorData = JsonSerializer.Deserialize<EvolutionErrorReponse>(error);
			return ResultData<string>.Failure(ResultType.Failure, $"Erro ao Cadastrar WhatsApp: " +
			$"{errorData!.Response.Message.First()}.");	
		}

		var jsonString = await response.Content.ReadAsStringAsync();
		var data = JsonSerializer.Deserialize<EvolutionCreateResponse>(jsonString);
		return ResultData<string>.Success(data!.QrCodeData.Base64);
	}
	public async Task<ResultData<string>> GerarNovoQrCodeWhatsAppAsync(string instanceName)
	{
		var url = $"/instance/connect/{instanceName}";

		var request = new HttpRequestMessage(HttpMethod.Get, url);

		var response = await _httpClient.SendAsync(request);
		if (!response.IsSuccessStatusCode)
		{
			var error = await response.Content.ReadAsStringAsync();
			var errorData = JsonSerializer.Deserialize<EvolutionErrorReponse>(error);
			return ResultData<string>.Failure(ResultType.Failure, $"Erro ao Gerar qrCode: " +
			$"{errorData.Response.Message.First()}");	
		}

		var jsonString = await response.Content.ReadAsStringAsync();
		var data = JsonSerializer.Deserialize<QrCodeData>(jsonString);
		return ResultData<string>.Success(data.Base64);
	}

	public async Task<ResultData> LogoutWhatsAppBarbeariaAsync(string instanceName)
	{
		var url = $"/instance/logout/{instanceName}";

		var request = new HttpRequestMessage(HttpMethod.Delete, url);

		var response = await _httpClient.SendAsync(request);
		if (!response.IsSuccessStatusCode)
		{
			var error = await response.Content.ReadAsStringAsync();
			var errorData = JsonSerializer.Deserialize<EvolutionErrorReponse>(error);
			return ResultData.Failure(ResultType.Failure, $"Erro ao realizar logout do WhatsApp: " +
			$"{errorData!.Response.Message.First()}");
		}

		return ResultData.Success();
	}

	public async Task<ResultData<string>> BuscarStatusConexaoWhatsAppAsync(string instanceName)
	{
		var url = $"/instance/connectionState/{instanceName}";

		var request = new HttpRequestMessage(HttpMethod.Get, url);

		var response = await _httpClient.SendAsync(request);
		if (!response.IsSuccessStatusCode)
		{
			var error = await response.Content.ReadAsStringAsync();
			var errorData = JsonSerializer.Deserialize<EvolutionErrorReponse>(error);
			return ResultData<string>.Failure(ResultType.Failure, $"Erro ao buscar status da conexão: " +
			$"{errorData!.Response.Message.First()}");
		}

		var jsonString = await response.Content.ReadAsStringAsync();
		var data = JsonSerializer.Deserialize<EvolutionConnectionStateResponse>(jsonString);

		return ResultData<string>.Success(data!.Instance.State);
	}

	public async Task<ResultData> DeletarWhatsAppBarbeariaAsync(string instanceName)
	{
		var url = $"/instance/delete/{instanceName}";

		var request = new HttpRequestMessage(HttpMethod.Delete, url);

		var response = await _httpClient.SendAsync(request);
		if (!response.IsSuccessStatusCode)
		{
			var error = await response.Content.ReadAsStringAsync();
			var errorData = JsonSerializer.Deserialize<EvolutionErrorReponse>(error);
			return ResultData.Failure(ResultType.NotFound, $"Erro ao Gerar qrCode: " +
			$"{errorData!.Response.Message.First()}");
		}

		return ResultData.Success();
	}


	public string GerarTemplateConfirmacaoAgendamento(string nomeBarbearia,string nomeCliente,DateOnly dataAgendamento,
	string nomeProfissional,Endereco endereco)
	{
		return WhatsAppTemplates.GerarTemplateConfirmacaoAgendamento(nomeBarbearia,nomeCliente,dataAgendamento,
			nomeProfissional,endereco);
	}

	public string GerarTemplateConcluirAgendamento(string nomeBarbearia, string nomeCliente, DateOnly dataAgendamento,
		string nomeProfissional)
	{
		return WhatsAppTemplates.GerarTemplateConcluirAgendamento(nomeBarbearia, nomeCliente, dataAgendamento, nomeProfissional);
	}

	public string GerarTemplateCancelarAgendamentoBarbeiro(string nomeBarbearia, string nomeCliente, DateOnly dataAgendamento,
		string nomeProfissional)
	{
		return WhatsAppTemplates.GerarTemplateCancelarAgendamentoBarbeiro(nomeBarbearia, nomeCliente, dataAgendamento, nomeProfissional);
	}

	public string GerarTemplateCancelarAgendamentoCliente(string nomeBarbearia, string nomeCliente, DateOnly dataAgendamento,
		string nomeProfissional)
	{
		return WhatsAppTemplates.GerarTemplateCancelarAgendamentoCliente(nomeBarbearia, nomeCliente, dataAgendamento, nomeProfissional);
	}
}
