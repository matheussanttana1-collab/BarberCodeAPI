using AutoMapper;
using BarberCode.Application.Interfaces;
using BarberCode.Domain.Entities.Barbearias;
using BarberCode.Domain.Shared;
using BarberCode.Service.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberCode.Application.UseCases.Barbearias;

public class AlterarHorarioFuncionamentoUseCase
{
	private readonly IBarbeariaRepository _repo;

	public AlterarHorarioFuncionamentoUseCase(IBarbeariaRepository repo)
	{
		_repo = repo;
	}

	public async Task<ResultData> ExecuteAsync (Guid id, List<HorarioFuncionamentoRequest> request)
	{
		var barbearia = await _repo.BuscarBarbeariaPorAsync(id);
		if (barbearia == null)
			return ResultData.Failure(ResultType.NotFound, "Barbearia não Encontrada");
		var horarios = request
		.Select(h => new HorarioFuncionamento(h.Dia, h.Inicio, h.Fim))
		.ToList();

		barbearia.AlterarFuncionamento(horarios);

		await _repo.AtualizarBarbeariaAsync();

		return ResultData.Success();
	}	
}
