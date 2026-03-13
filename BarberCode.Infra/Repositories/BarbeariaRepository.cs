using BarberCode.Application.Interfaces;
using BarberCode.Domain.Entities.Barbearias;
using BarberCode.Infra.Banco;
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

	public void AtualizarBarbearia()
	{
		_context.SaveChanges();
	}

	public Barbearia? BuscarBarbeariaPor(Guid id)
	{
		var barbearia = _context.barbearias.FirstOrDefault(b => b.Id == id);
		return barbearia;
	}

	public void DeletarBarbearia(Barbearia barbearia)
	{
		_context.barbearias.Remove(barbearia);
		_context.SaveChanges();
	}

	public void SalvarBarbearia(Barbearia barbearia)
	{
		_context.barbearias.Add(barbearia);
		_context.SaveChanges();
	}
}
