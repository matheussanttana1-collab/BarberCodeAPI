using BarberCode.Domain.Entities.Barbeiros;
namespace BarberCode.Domain.Entities.Agendamentos;

using Barbeiros;
using Barbearias;

public class Agendamento
{
	public Guid Id { get; private set; }
	public Guid BarbeiroId { get; private set; }
	public virtual Barbeiro Barbeiro { get; private set; }
	public Guid BarbeariaId { get; private set; }
	public virtual Barbearia Barbearia { get; private set; }
	public ClienteInfo Cliente { get; private set; }
	public DateOnly Dia { get; private set; }
	public TimeOnly Horario { get; private set; }
	public int Duracao { get; private set; }
	public Guid ServicoId { get; private set; }
	public virtual Servico Servico { get; private set; }
	public StatusAgendamento Status {  get; private set; }

	protected Agendamento()
	{
	}

	public Agendamento(Guid barbeiroId, Guid barbeariaId,ClienteInfo cliente, DateOnly dia, 
		TimeOnly horario,int duracao, Guid servicoId)
	{
		Id = Guid.NewGuid();
		BarbeiroId = barbeiroId;
		BarbeariaId = barbeariaId;
		Cliente = cliente;
		Dia = dia;
		Horario = horario;
		Duracao = duracao;
		ServicoId = servicoId;
		Status = StatusAgendamento.Pendente;
	}

	public void ConcluirAgendamento()
	{
		if (Status == StatusAgendamento.Concluido)
			throw new Exception("Agendamento Ja foi Concluido");

		Status = StatusAgendamento.Concluido;

	}
}
