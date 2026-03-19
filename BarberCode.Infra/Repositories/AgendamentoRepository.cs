using BarberCode.Application.Interfaces;
using BarberCode.Domain.Entities.Agendamentos;
using BarberCode.Infra.Banco;

namespace BarberCode.Infra.Repositories;

public class AgendamentoRepository : IAgendamentoRepository
{
	private readonly BarberCodeContext _context;

	public AgendamentoRepository(BarberCodeContext context)
	{
		_context = context;
	}

	public void AtualizarAgendadamento()
	{
		_context.SaveChanges();
		
	}

	public Agendamento? BuscarAgendadamentoPorId(Guid id)
	{
		var agendamento = _context.agendamentos.FirstOrDefault(a => a.Id == id);
		return agendamento;
	}

	public IEnumerable<Agendamento> BuscarAgendamentos(Guid BarbeiroId) {
		var agendamentos = _context.agendamentos.Where(a => a.BarbeiroId == BarbeiroId);
		return agendamentos;
	}

	public void DeletarAgendadamento(Agendamento agendamento)
	{
		_context.agendamentos.Remove(agendamento);
		_context.SaveChanges();
	}

	public void SalvarAgendadamento(Agendamento agendamento)
	{
		_context.agendamentos.Add(agendamento);
		_context.SaveChanges();
	}
}
