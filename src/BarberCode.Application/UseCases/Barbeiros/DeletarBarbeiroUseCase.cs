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
	private readonly IAppUserService _userService;

	public DeletarBarbeiroUseCase(IBarbeiroRepository repo, IAppUserService userService)
	{
		_repo = repo;
		_userService = userService;
	}

	public async Task<ResultData> ExecuteAsync(Guid id, Guid barbeariaId) 
	{
		var barbeiro = await _repo.BuscarBarbeiroPorAsync(id);
		if (barbeiro == null)
			return ResultData.Failure(ResultType.NotFound, "Barbeiro não Encontrado");
		if (barbeiro.BarbeariaId != barbeariaId)
			return ResultData.Failure(ResultType.Forbidden, "Você não tem permissão para deletar barbeiros de" +
			" outra barbearia.");

		await _userService.DeletarUsuarioAsync(id);
		await _repo.DeletarBarbeiroAsync(barbeiro);

		return ResultData.Success();
	}
}
