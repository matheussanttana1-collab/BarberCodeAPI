using BarberCode.Domain.Entities.Barbeiros;
namespace BarberCode.Domain.Entities.Agendamentos;

using Barbeiros;
using Barbearias;

public class Agendamento
{
	public Guid Id { get; set; }
	public Guid BarbeiroID { get; set; }
	public virtual Barbeiro Barbeiro { get; set; }
	public Guid BarbeariaID { get; set; }
	public virtual Barbearia Barbearia { get; set; }
	public ClienteInfo Cliente { get; set; }
	public DateOnly Dia { get; set; }
	public TimeOnly Horario { get; set; }
	public int Duracao { get; set; }
	public Guid ServicoId { get; set; }
	public virtual Servico Servico { get; set; }

	private Agendamento()
	{
	}

	public Agendamento(Guid barbeiroID, Guid barbeariaID, ClienteInfo cliente, DateOnly dia, 
		TimeOnly horario, Guid servicoId, int duracao)
	{
		Id = Guid.NewGuid();
		BarbeiroID = barbeiroID;
		BarbeariaID = barbeariaID;
		Cliente = cliente;
		Dia = dia;
		Horario = horario;
		ServicoId = servicoId;
		Duracao = duracao;
	}
}
