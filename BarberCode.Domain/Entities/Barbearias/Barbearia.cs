namespace BarberCode.Domain.Entities.Barbearias;

using Barbeiros;
public class Barbearia
{
	public Guid Id { get; set; }
	public string Name { get; set; }
	public Endereco Endereco { get; set; }
	public ICollection<HorarioFuncionamento> Funcionamento{ get; set; }
	public ICollection<Barbeiro> Barbeiros { get; set; } = new List<Barbeiro>();
	public ICollection<Servico> Servicos { get; set; } = new List<Servico>();
	private int MaximoFuncionarios { get; set; } = 2;

	public bool EstaFuncionando(DateOnly dia, TimeOnly hora) 
	{
		var diaSemana = Funcionamento.FirstOrDefault(d => d.dia == dia.DayOfWeek);
		if (diaSemana is null) 
			return false;

		if(hora < diaSemana.Incio && hora > diaSemana.Fim)
			return false;

		return true;
			
	
	}

	public void AdicionarNovoFuncionario (string nome, string foto)
	{
		

	}

	public void AddServico()
	{

	}
	public void AlterarHorarioFuncionamento () { }


	

	
}
