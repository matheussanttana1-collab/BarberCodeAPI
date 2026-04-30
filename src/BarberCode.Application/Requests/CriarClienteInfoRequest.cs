using System.ComponentModel.DataAnnotations;

namespace BarberCode.Service.Requests;

public record CriarClienteInfoRequest(string Nome, string Celular)
{
}
