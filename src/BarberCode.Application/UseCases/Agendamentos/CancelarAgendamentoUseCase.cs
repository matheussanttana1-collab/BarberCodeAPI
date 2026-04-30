using BarberCode.Application.Interfaces;
using BarberCode.Application.EventsHandlers;
using BarberCode.Domain.Entities.Agendamentos;
using BarberCode.Domain.Shared;
using BarberCode.Service.Requests;


namespace BarberCode.Application.UseCases.Agendamentos;

public class CancelarAgendamentoUseCase
{
	private readonly IAgendamentoRepository _repo;
	private readonly IEventBus _eventBus;

	public CancelarAgendamentoUseCase(IAgendamentoRepository repo, IEventBus eventBus)
	{
		_repo = repo;
		_eventBus = eventBus;
	}

	public async Task<ResultData> ExecuteAsync (Guid id, Guid userId) {

		var agendamento = await _repo.BuscarAgendadamentoPorIdAsync(id);

		if (agendamento is null)
			return ResultData.Failure(ResultType.NotFound, "Agendamento não encontrado");

		if (!agendamento.TemPermissaoDeAcesso(userId))
			return ResultData.Failure(ResultType.Forbidden, "Você não tem permissão para essa tarefa");

		await _repo.DeletarAgendadamentoAsync(agendamento);

		// Publica evento para enviar WhatsApp
		await _eventBus.PublishAsync(new CancelarAgendamentoBarbeiroEvent(
			agendamento.Cliente.Celular,
			agendamento.Cliente.Name,
			agendamento.Barbearia.Nome,
			agendamento.Barbeiro.Nome,
			agendamento.Dia,
			agendamento.Barbearia.Slug
		));

		return ResultData.Success();
	}
}
