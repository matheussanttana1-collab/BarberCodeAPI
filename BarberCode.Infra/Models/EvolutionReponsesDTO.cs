using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BarberCode.Infra.Models;

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
	public string Message { get; set; }
}