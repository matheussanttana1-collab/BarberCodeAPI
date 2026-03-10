using System.ComponentModel.DataAnnotations;

namespace BarberCode.Service.Requests;

public class BarbeariaRequest
{
    [Required(ErrorMessage = "Nome é obrigatório")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "Endereço é obrigatório")]
    public EnderecoRequest Endereco { get; set; }
}
