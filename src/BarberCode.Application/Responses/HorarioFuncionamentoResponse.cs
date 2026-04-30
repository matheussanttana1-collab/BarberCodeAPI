namespace BarberCode.Service.Responses;

public class HorarioFuncionamentoResponse 
{
	public DayOfWeek Dia { get; set; }
	public TimeOnly Inicio { get; set; }
	public TimeOnly Fim { get; set; }

}
