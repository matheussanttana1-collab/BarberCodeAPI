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
