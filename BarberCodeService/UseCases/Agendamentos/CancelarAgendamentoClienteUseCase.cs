using BarberCode.Application.Interfaces;
using BarberCode.Domain.Shared;

namespace BarberCode.Application.UseCases.Agendamentos;

public class CancelarAgendamentoClienteUseCase
{
	private readonly IAgendamentoRepository _repo;

	public CancelarAgendamentoClienteUseCase(IAgendamentoRepository repo)
	{
		_repo = repo;
	}

	public async Task<ResultData> ExecuteAsync(Guid clienteId, Guid agendamentoId)
	{
		var agendamento = await _repo.BuscarAgendadamentoPorIdAsync(agendamentoId);

		if (agendamento is null)
			return ResultData.Failure(ResultType.NotFound, "Agendamento não encontrado");

		var status = agendamento.ValidarCancelamento(clienteId);
		if(status.Type == ResultType.Validation)
			return ResultData.Failure(status.Type, status.Message);

		await _repo.DeletarAgendadamentoAsync(agendamento);

		return ResultData.Success();
	}

}
