using System.ComponentModel.DataAnnotations;

namespace BarberCode.Service.Requests;

public class EnderecoRequest
{
	[Required(ErrorMessage = "Logradouro é obrigatório")]
	public string Lougradouro { get; set; }

	[Required(ErrorMessage = "Nome é obrigatório")]
	public string Nome { get; set; }

	[Required(ErrorMessage = "Número é obrigatório")]
	public int Numero { get; set; }

	[Required(ErrorMessage = "Cidade é obrigatória")]
	public string Cidade { get; set; }

	[Required(ErrorMessage = "Estado é obrigatório")]
	public string Estado { get; set; }
}
