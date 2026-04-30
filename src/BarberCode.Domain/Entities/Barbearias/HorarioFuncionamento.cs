namespace BarberCode.Domain.Entities.Barbearias;

public class HorarioFuncionamento
{
	public DayOfWeek dia { get; private set; }
	public TimeOnly Inicio { get; private set; }
	public TimeOnly Fim { get; private set; }

	// Construtor privado para EF Core
	protected HorarioFuncionamento() { }

	// Construtor público
	public HorarioFuncionamento(DayOfWeek dia, TimeOnly inicio, TimeOnly fim)
	{
		if (inicio >= fim)
			throw new ArgumentException("Horário de início deve ser menor que o fim");

		this.dia = dia;
		Inicio = inicio;
		Fim = fim;
	}
}
