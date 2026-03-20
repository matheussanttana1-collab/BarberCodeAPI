using BarberCode.Domain.Entities.Agendamentos;
namespace BarberCode.Application.Interfaces;

public interface IAgendamentoRepository
{
	void SalvarAgendadamento(Agendamento agendamento);
	Agendamento? BuscarAgendadamentoPorId(Guid id);
	IEnumerable<Agendamento> BuscarAgendamentosDoCliente(Guid ClienteId);
	IEnumerable<Agendamento> BuscarAgendamentos(Guid barbeiroId, StatusAgendamento? status);
	void AtualizarAgendadamento();
	void DeletarAgendadamento(Agendamento agendamento);
	
}
	

