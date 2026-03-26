using BarberCode.Application.Interfaces;
using BarberCode.Domain.Shared;
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

	public async Task<ResultData> ExecuteAsync(Guid id) 
	{
		var barbeiro = await _repo.BuscarBarbeiroPorAsync(id);
		if (barbeiro == null)
			return ResultData.Failure(ResultType.NotFound, "Barbeiro não Encontrado");

		await _repo.DeletarBarbeiroAsync(barbeiro);

		return ResultData.Success();
	}
}
