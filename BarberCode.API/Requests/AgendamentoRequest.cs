using System.ComponentModel.DataAnnotations;

namespace BarberCode.Service.Requests;

public class AgendamentoRequest
{
    [Required(ErrorMessage = "BarbeiroId é obrigatório")]
    public Guid BarbeiroId { get; set; }

    [Required(ErrorMessage = "BarbeariaId é obrigatório")]
    public Guid BarbeariaId { get; set; }

    [Required(ErrorMessage = "ServicoId é obrigatório")]
    public Guid ServicoId { get; set; }

    [Required(ErrorMessage = "Horário é obrigatório")]
    public DateTime Horario { get; set; }

    [Required(ErrorMessage = "Cliente é obrigatório")]
    public ClienteInfoRequest Cliente { get; set; }
}
