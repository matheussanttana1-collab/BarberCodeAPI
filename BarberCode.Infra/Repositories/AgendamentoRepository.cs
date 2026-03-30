using BarberCode.Application.Interfaces;
using BarberCode.Domain.Entities.Agendamentos;
using BarberCode.Domain.Entities.Barbeiros;
using BarberCode.Infra.Banco;
using Microsoft.EntityFrameworkCore;
using System.Net.NetworkInformation;

namespace BarberCode.Infra.Repositories;

public class AgendamentoRepository : IAgendamentoRepository
{
	private readonly BarberCodeContext _context;

	public AgendamentoRepository(BarberCodeContext context)
	{
		_context = context;
	}

	public async Task AtualizarAgendadamentoAsync()
	{
		 _context.SaveChanges();

	}

	public async Task<Agendamento?> BuscarAgendadamentoPorIdAsync(Guid id)
	{
		return await _context.agendamentos.FirstOrDefaultAsync(a => a.Id == id);
	}

	public async Task<IEnumerable<Agendamento>> BuscarAgendamentosDoClienteAsync(Guid clienteId)
	{
		return await _context.agendamentos
			.Where(a => a.ClienteId == clienteId)
			.Where(a => a.Status == StatusAgendamento.Pendente)
			.ToListAsync();
	}

	public async Task<IEnumerable<Agendamento>> BuscarAgendamentosAsync(Guid barbeiroId, StatusAgendamento? status)
	{
		return await _context.agendamentos
			.Where(a => a.BarbeiroId == barbeiroId)
			.Where(a => status == null || a.Status == status)
			.ToListAsync();
	}

	public async Task DeletarAgendadamentoAsync(Agendamento agendamento)
	{
		_context.agendamentos.Remove(agendamento);
		await _context.SaveChangesAsync();
	}

	public async Task SalvarAgendadamentoAsync(Agendamento agendamento)
	{
		_context.agendamentos.Add(agendamento);
		await _context.SaveChangesAsync();
	}
}