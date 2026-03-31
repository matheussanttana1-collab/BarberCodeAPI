using BarberCode.Application.Interfaces;
using BarberCode.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace BarberCode.Application.UseCases.Agendamentos;

public class ConcluirAgendamentoUseCase
{
	private readonly IAgendamentoRepository _repo;

	public ConcluirAgendamentoUseCase(IAgendamentoRepository repo)
	{
		_repo = repo;
	}

	public async Task<ResultData> ExecuteAsync(Guid Id)
	{
		var agendamento = await _repo.BuscarAgendadamentoPorIdAsync(Id);

		if (agendamento is null)
			return ResultData.Failure(ResultType.NotFound, "Agendamento Não Encotrado");

		var concluirResult = agendamento.ConcluirAgendamento();
		if (!concluirResult.IsSuccess)
			return ResultData.Failure(concluirResult.Type, concluirResult.Message);
		await _repo.AtualizarAgendadamentoAsync();

		return ResultData.Success();
	}

}
