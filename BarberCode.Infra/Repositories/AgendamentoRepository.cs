using BarberCode.Application.Interfaces;
using BarberCode.Domain.Entities.Agendamentos;
using BarberCode.Domain.Entities.Barbeiros;
using BarberCode.Infra.Banco;
using System.Net.NetworkInformation;

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

	public IEnumerable<Agendamento> BuscarAgendamentosDoCliente(Guid clienteId)
	{
		var agendamentos = _context.agendamentos.Where(a => a.ClienteId == clienteId).
		Where(a => a.Status == StatusAgendamento.Pendente).ToList();
		return agendamentos;

	}

	public IEnumerable<Agendamento> BuscarAgendamentos(Guid BarbeiroId, StatusAgendamento? status) {
		var agendamentos = _context.agendamentos.Where(a => a.BarbeiroId == BarbeiroId).
		Where(a => status == null || a.Status == status).ToList();
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
