using System.ComponentModel.DataAnnotations;

namespace BarberCode.Service.Requests;

public class CriarAgendamentoRequest
{
    [Required(ErrorMessage = "BarbeiroId é obrigatório")]
    public Guid BarbeiroId { get; set; }

    [Required(ErrorMessage = "BarbeariaId é obrigatório")]
    public Guid BarbeariaId { get; set; }

    [Required(ErrorMessage = "ServicoId é obrigatório")]
    public Guid ServicoId { get; set; }

    [Required(ErrorMessage = "Dia é obrigatório")]
    public DateOnly Dia { get; set; }
    [Required(ErrorMessage = "Horário é obrigatório")]
    public TimeOnly Horario { get; set; }

    [Required(ErrorMessage = "Cliente é obrigatório")]
    public CriarClienteInfoRequest Cliente { get; set; }
}
