using System.ComponentModel.DataAnnotations;

namespace BarberCode.Service.Requests;

public class CriarHorarioFuncionamentoRequest
{
    [Required(ErrorMessage = "Dia da semana é obrigatório")]
    public DayOfWeek Dia { get; set; }

    [Required(ErrorMessage = "Horário de início é obrigatório")]
    public TimeOnly Inicio { get; set; }

    [Required(ErrorMessage = "Horário de fim é obrigatório")]
    public TimeOnly Fim { get; set; }
}
