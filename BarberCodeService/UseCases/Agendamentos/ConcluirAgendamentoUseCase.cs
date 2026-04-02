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
	private readonly IBarbeariaRepository _barbeariaRepo;

	public ConcluirAgendamentoUseCase(IAgendamentoRepository repo, IBarbeariaRepository barbeariaRepo)
	{
		_repo = repo;
		_barbeariaRepo = barbeariaRepo;
	}

	public async Task<ResultData> ExecuteAsync(Guid agendamentoId, Guid userId)
	{
		var agendamento = await _repo.BuscarAgendadamentoPorIdAsync(agendamentoId);
		if (agendamento is null)
			return ResultData.Failure(ResultType.NotFound, "Agendamento Não Encotrado");

		if (!agendamento.TemPermissaoDeAcesso(userId))
			return ResultData.Failure(ResultType.Forbidden, "Você não tem permissão para essa tarefa");

		var concluirResult = agendamento.ConcluirAgendamento();
		if (!concluirResult.IsSuccess)
			return ResultData.Failure(concluirResult.Type, concluirResult.Message);
		await _repo.AtualizarAgendadamentoAsync();

		return ResultData.Success();
	}

}
