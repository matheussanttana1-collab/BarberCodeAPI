using BarberCode.Domain.Entities.Barbeiros;
namespace BarberCode.Domain.Entities.Agendamentos;

using Barbeiros;
using Barbearias;

public class Agendamento
{
	public Guid Id { get; private set; }
	public Guid BarbeiroID { get; private set; }
	public virtual Barbeiro Barbeiro { get; private set; }
	public Guid BarbeariaID { get; private set; }
	public virtual Barbearia Barbearia { get; private set; }
	public ClienteInfo Cliente { get; private set; }
	public DateOnly Dia { get; private set; }
	public TimeOnly Horario { get; private set; }
	public int Duracao { get; private set; }
	public Guid ServicoId { get; private set; }
	public virtual Servico Servico { get; private set; }

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
