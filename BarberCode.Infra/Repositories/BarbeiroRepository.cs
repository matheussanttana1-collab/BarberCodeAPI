using BarberCode.Application.Interfaces;
using BarberCode.Domain.Entities.Agendamentos;
using BarberCode.Domain.Entities.Barbearias;
using BarberCode.Domain.Entities.Barbeiros;
using BarberCode.Infra.Banco;
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

	public void AtualizarBarbeiro()
	{
		_context.SaveChanges();
	}

	public IEnumerable<Barbeiro> BuscarBarbeiros (Guid BarbeariaId) 
	{
		return _context.barbeiros.Where(b => b.BarbeariaId == BarbeariaId);
	}

	public Barbeiro? BuscarBarbeiroPor(Guid id)
	{
		var barbeiro = _context.barbeiros.FirstOrDefault(b => b.Id == id);
		return barbeiro;
	}

	public void DeletarBarbeiro(Barbeiro barbeiro)
	{
		_context.barbeiros.Remove(barbeiro);
		_context.SaveChanges();
	}

	public void SalvarBarbeiro(Barbeiro barbeiro)
	{
		_context.barbeiros.Add(barbeiro);
		_context.SaveChanges();
	}

}
