using BarberCode.Application.Interfaces;
using BarberCode.Domain.Shared;
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

	public async Task<ResultData> ExecuteAsync(Guid id)
	{
		var barbearia = await _repo.BuscarBarbeariaPorAsync(id);
		if (barbearia == null)
			return ResultData.Failure(ResultType.NotFound, "Barbearia não Encontrada");
		await _repo.DeletarBarbeariaAsync(barbearia);

		return ResultData.Success();
	}
}
