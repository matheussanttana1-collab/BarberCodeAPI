using System.ComponentModel.DataAnnotations;

namespace BarberCode.Service.Requests;

public class BarbeiroRequest
{
    [Required(ErrorMessage = "Nome é obrigatório")]
    public string Nome { get; set; }

    public string? FotoPerfil { get; set; }
}
