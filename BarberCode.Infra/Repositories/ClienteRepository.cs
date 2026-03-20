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

public class ClienteRepository : IClienteRepository
{
	private readonly BarberCodeContext _context;

	public ClienteRepository(BarberCodeContext context)
	{
		_context = context;
	}

	public ClienteInfo? BuscarClientePeloTelefone(string telefone)
	{
		return _context.clientes.FirstOrDefault(c => c.Celular == telefone);
	}

	public void SalvarCliente(ClienteInfo cliente)
	{
		_context.clientes.Add(cliente);
		_context.SaveChanges();
	}

	public IEnumerable<ClienteInfo> BuscarClientes(Guid barbeariaId) 
	{
		var clientes = _context.clientes.Where(c => c.BarbeariaId == barbeariaId);

		return clientes;
	}
}
