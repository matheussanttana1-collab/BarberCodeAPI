using System.ComponentModel.DataAnnotations;

namespace BarberCode.Service.Requests;

public class ClienteInfoRequest
{

	public string Nome { get; set; }

	public int Telefone { get; set; }
}
