using System.ComponentModel.DataAnnotations;

namespace BarberCode.Service.Requests;

public record CriarServicoRequest(string Name, int Duracao, string Descricao, double Preco)
{
}
