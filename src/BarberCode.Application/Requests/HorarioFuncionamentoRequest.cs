using System.ComponentModel.DataAnnotations;

namespace BarberCode.Service.Requests;

public class HorarioFuncionamentoRequest
{
    public DayOfWeek Dia { get; set; }

    public TimeOnly Inicio { get; set; }

    public TimeOnly Fim { get; set; }
}
