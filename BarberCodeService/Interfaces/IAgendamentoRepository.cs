using BarberCode.Domain.Entities.Agendamentos;
namespace BarberCode.Application.Interfaces;

public interface IAgendamentoRepository
{
	void SalvarAgendadamento(Agendamento agendamento);
	Agendamento? BuscarAgendadamentoPorId(Guid id);
	Agendamento? BuscarAgendamentoDoCliente(Guid id, Guid ClienteId);
	IEnumerable<Agendamento> BuscarAgendamentos(Guid barbeiroId, StatusAgendamento? status);
	void AtualizarAgendadamento();
	void DeletarAgendadamento(Agendamento agendamento);
	
}
	

