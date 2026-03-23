using System.ComponentModel.DataAnnotations;

namespace BarberCode.Service.Requests;

public class CriarClienteInfoRequest
{

	public string Nome { get; set; }

	public string Celular { get; set; }
}
