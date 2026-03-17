namespace BarberCode.Service.Responses;

public class HorarioFuncionamentoResponse 
{
	public DayOfWeek Dia { get; set; }
	public TimeOnly Incio { get; set; }
	public TimeOnly Fim { get; set; }

}
