using System.ComponentModel.DataAnnotations;

namespace BarberCode.Service.Requests;

public class ServicoRequest
{
    [Required(ErrorMessage = "Nome é obrigatório")]
    public string Nome { get; set; }
	[Required(ErrorMessage = "Duração é obrigatória")]
	public int Duracao { get; set; }

    [Required(ErrorMessage = "Descrição é obrigatória")]
    public string Descricao { get; set; }
}
