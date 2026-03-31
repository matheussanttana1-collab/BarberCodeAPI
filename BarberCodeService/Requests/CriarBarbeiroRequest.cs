using System.ComponentModel.DataAnnotations;

namespace BarberCode.Service.Requests;

public record CriarBarbeiroRequest(string Nome, TimeOnly HorarioAlmoco, string? FotoPerfil, string Email, string Senha)
{
}
