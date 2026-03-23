using BarberCode.Application.Interfaces;
using BarberCode.Domain.Entities.Barbearias;
using BarberCode.Infra.Banco;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberCode.Infra.Repositories;

public class BarbeariaRepository : IBarbeariaRepository
{

	private readonly BarberCodeContext _context;

	public BarbeariaRepository(BarberCodeContext context)
	{
		_context = context;
	}

	public async Task AtualizarBarbeariaAsync()
	{
		await _context.SaveChangesAsync();
	}

	public async Task<IEnumerable<Barbearia>> BuscarBarbeariasAsync()
	{
		return await _context.barbearias.ToListAsync();
	}

	public async Task<Barbearia?> BuscarBarbeariaPorAsync(Guid id)
	{
		return await _context.barbearias.FirstOrDefaultAsync(b => b.Id == id);
	}

	public async Task DeletarBarbeariaAsync(Barbearia barbearia)
	{
		_context.barbearias.Remove(barbearia);
		await _context.SaveChangesAsync();
	}

	public async Task SalvarBarbeariaAsync(Barbearia barbearia)
	{
		_context.barbearias.Add(barbearia);
		await _context.SaveChangesAsync();
	}
}
