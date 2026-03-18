using BarberCode.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberCode.Application.UseCases.Barbeiros;

public class DeletarBarbeiroUseCase
{
	private readonly IBarbeiroRepository _repo;

	public DeletarBarbeiroUseCase(IBarbeiroRepository repo)
	{
		_repo = repo;
	}

	public void Execute(Guid id) 
	{
		var barbeiro = _repo.BuscarBarbeiroPor(id);

		if (barbeiro == null)
			throw new Exception("Barbeiro não Encontrado");

		_repo.DeletarBarbeiro(barbeiro);	
	}
}
