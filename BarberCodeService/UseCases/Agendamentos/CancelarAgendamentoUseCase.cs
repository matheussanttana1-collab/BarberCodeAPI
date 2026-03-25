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

	public async Task<ResultData> ExecuteAsync (Guid Id) {

		var agendamento = await _repo.BuscarAgendadamentoPorIdAsync(Id);

		if (agendamento is null)
			return ResultData.Failure(ResultType.NotFound, "Agendamento não encontrado");

		await _repo.DeletarAgendadamentoAsync(agendamento);

		return ResultData.Success();
	}
}
