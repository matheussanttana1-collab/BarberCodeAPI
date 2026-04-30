using BarberCode.Application.Interfaces;
using BarberCode.Domain.Entities.Barbearias;
using BarberCode.Infra.Banco;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberCode.Infra.Repositories;

public class ServicoRepository : IServicoRepository
{
	private readonly BarberCodeContext _context;

	public ServicoRepository(BarberCodeContext context)
	{
		_context = context;
	}

	public async Task SalvarServicoAsync(Servico servico)
	{
		_context.servicos.Add(servico);
		await _context.SaveChangesAsync();
	}

	public async Task<IEnumerable<Servico>> BuscarServicosAsync(Guid barbeariaId)
	{
		return await _context.servicos
			.Where(s => s.BarbeariaId == barbeariaId)
			.ToListAsync();
	}

	public async Task<Servico?> BuscarServicoPorAsync(Guid id)
	{
		return await _context.servicos.FirstOrDefaultAsync(s => s.Id == id);
	}

	public async Task DeletarServicoAsync(Servico servico)
	{
		_context.servicos.Remove(servico);
		await _context.SaveChangesAsync();
	}

	public async Task AtualizarServicoAsync()
	{
		await _context.SaveChangesAsync();
	}
}
