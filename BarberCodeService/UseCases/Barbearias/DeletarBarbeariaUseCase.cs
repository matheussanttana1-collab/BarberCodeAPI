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

	public async Task ExecuteAsync(Guid id)
	{
		var barbearia = await _repo.BuscarBarbeariaPorAsync(id);

		if (barbearia == null)
			throw new Exception("Barbearia não Encontrado");

		await _repo.DeletarBarbeariaAsync(barbearia);
	}
}
