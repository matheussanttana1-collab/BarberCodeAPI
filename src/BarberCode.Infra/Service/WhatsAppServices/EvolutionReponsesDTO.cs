using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BarberCode.Infra.Service.WhatsAppServices;

public class EvolutionCreateResponse
{
	[JsonPropertyName("qrcode")]
	public  QrCodeData QrCodeData{ get; set; }
}

public class QrCodeData
{
	[JsonPropertyName("base64")]
	public string Base64 { get; set; }
}

public class EvolutionErrorReponse 
{
	[JsonPropertyName("error")]
	public string Error {  get; set; }
	[JsonPropertyName("response")]
	public ErrorData Response {  get; set; }

}

public class ErrorData
{
	[JsonPropertyName("message")]
	public IEnumerable<string> Message { get; set; }
}

public class EvolutionConnectionStateResponse
{
	[JsonPropertyName("instance")]
	public IntanceData Instance { get; set; }
}

public class IntanceData
{
	[JsonPropertyName("instaceName")]
	public string InstanceName { get; set; }
	[JsonPropertyName("state")]
	public string State { get; set; }
	

}