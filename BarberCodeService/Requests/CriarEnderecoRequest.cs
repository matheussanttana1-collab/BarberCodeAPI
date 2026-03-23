using System.ComponentModel.DataAnnotations;

namespace BarberCode.Service.Requests;

public class CriarEnderecoRequest
{
	public string Logradouro { get; set; }
	public int Numero { get; set; }
	public string Bairro { get; set; }
	public string Cidade { get; set; }
	public string Estado { get; set; }
	public string CEP { get; set; }
}
