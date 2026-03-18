using BarberCode.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace BarberCode.Application.UseCases.Barbearias;

public class DeletarBarbeariaUseCase
{
	private readonly IBarbeariaRepository _repo;

	public DeletarBarbeariaUseCase(IBarbeariaRepository repo)
	{
		_repo = repo;
	}

	public void Execute(Guid id)
	{
		var barbearia = _repo.BuscarBarbeariaPor(id);

		if (barbearia == null)
			throw new Exception("Barbearia não Encontrado");

		_repo.DeletarBarbearia(barbearia);
	}
}
