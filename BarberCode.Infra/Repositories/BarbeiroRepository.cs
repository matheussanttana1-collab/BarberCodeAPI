using BarberCode.Application.Interfaces;
using BarberCode.Domain.Entities.Agendamentos;
using BarberCode.Domain.Entities.Barbearias;
using BarberCode.Domain.Entities.Barbeiros;
using BarberCode.Infra.Banco;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberCode.Infra.Repositories;

public class BarbeiroRepository : IBarbeiroRepository
{
	private readonly BarberCodeContext _context;

	public BarbeiroRepository(BarberCodeContext context)
	{
		_context = context;
	}

	public async Task AtualizarBarbeiroAsync()
	{
		await _context.SaveChangesAsync();
	}

	public async Task<IEnumerable<Barbeiro>> BuscarBarbeirosAsync(Guid barbeariaId)
	{
		return await _context.barbeiros
			.Where(b => b.BarbeariaId == barbeariaId)
			.ToListAsync();
	}

	public async Task<Barbeiro?> BuscarBarbeiroPorAsync(Guid id)
	{
		return await _context.barbeiros
			.Include(b => b.Agendamentos)
			.FirstOrDefaultAsync(b => b.Id == id);
	}

	public async Task DeletarBarbeiroAsync(Barbeiro barbeiro)
	{
		_context.barbeiros.Remove(barbeiro);
		await _context.SaveChangesAsync();
	}

	public async Task SalvarBarbeiroAsync(Barbeiro barbeiro)
	{
		_context.barbeiros.Add(barbeiro);
		await _context.SaveChangesAsync();
	}

}
