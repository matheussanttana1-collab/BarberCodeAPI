using BarberCode.Application.Interfaces;
using BarberCode.Domain.Entities.Agendamentos;
using BarberCode.Domain.Shared;
using BarberCode.Service.Requests;


namespace BarberCode.Application.UseCases.Agendamentos;

public class CancelarAgendamentoUseCase
{
	private readonly IAgendamentoRepository _repo;

	public CancelarAgendamentoUseCase(IAgendamentoRepository repo)
	{
		_repo = repo;
	}

	public async Task<ResultData> ExecuteAsync (Guid id, Guid userId) {

		var agendamento = await _repo.BuscarAgendadamentoPorIdAsync(id);

		if (agendamento is null)
			return ResultData.Failure(ResultType.NotFound, "Agendamento não encontrado");

		if (!agendamento.TemPermissaoDeAcesso(userId))
			return ResultData.Failure(ResultType.Forbidden, "Você não tem permissão para essa tarefa");

		await _repo.DeletarAgendadamentoAsync(agendamento);

		return ResultData.Success();
	}
}
