using BarberCode.Domain.Entities.Agendamentos;
namespace BarberCode.Application.Interfaces;

public interface IAgendamentoRepository
{
	Task SalvarAgendadamentoAsync(Agendamento agendamento);
	Task<Agendamento?> BuscarAgendadamentoPorIdAsync(Guid id);
	Task<IEnumerable<Agendamento>> BuscarAgendamentosDoClienteAsync(Guid ClienteId);
	Task<IEnumerable<Agendamento>> BuscarAgendamentosAsync(Guid barbeiroId, StatusAgendamento? status, DateOnly? dia);
	Task AtualizarAgendadamentoAsync();
	Task DeletarAgendadamentoAsync(Agendamento agendamento);
	
}
	

