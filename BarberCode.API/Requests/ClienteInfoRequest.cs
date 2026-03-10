using System.ComponentModel.DataAnnotations;

namespace BarberCode.Service.Requests;

public class ClienteInfoRequest
{
	[Required(ErrorMessage = "Nome do cliente é obrigatório")]
	public string Nome { get; set; }

	[Required(ErrorMessage = "Telefone do cliente é obrigatório")]
	public int Telefone { get; set; }
}
