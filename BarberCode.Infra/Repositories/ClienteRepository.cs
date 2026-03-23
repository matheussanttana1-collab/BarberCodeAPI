using BarberCode.Application.Interfaces;
using BarberCode.Domain.Entities.Barbearias;
using BarberCode.Infra.Banco;
using Microsoft.EntityFrameworkCore;

namespace BarberCode.Infra.Repositories;

public class ClienteRepository : IClienteRepository
{
	private readonly BarberCodeContext _context;

	public ClienteRepository(BarberCodeContext context)
	{
		_context = context;
	}

	public async Task<ClienteInfo?> BuscarClientePeloTelefoneAsync(string telefone)
	{
		return await _context.clientes.FirstOrDefaultAsync(c => c.Celular == telefone);
	}

	public async Task SalvarClienteAsync(ClienteInfo cliente)
	{
		_context.clientes.Add(cliente);
		await _context.SaveChangesAsync();
	}

	public async Task<IEnumerable<ClienteInfo>> BuscarClientesAsync(Guid barbeariaId)
	{
		return await _context.clientes
			.Where(c => c.BarbeariaId == barbeariaId)
			.ToListAsync();
	}
}
