using BarberCode.Application.Interfaces;
using BarberCode.Application.EventsHandlers;
using BarberCode.Domain.Shared;

namespace BarberCode.Application.UseCases.Agendamentos;

public class CancelarAgendamentoClienteUseCase
{
	private readonly IAgendamentoRepository _repo;
	private readonly IEventBus _eventBus;

	public CancelarAgendamentoClienteUseCase(IAgendamentoRepository repo, IEventBus eventBus)
	{
		_repo = repo;
		_eventBus = eventBus;
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

		// Publica evento para enviar WhatsApp
		await _eventBus.PublishAsync(new CancelarAgendamentoClienteEvent(
			agendamento.Cliente.Celular,agendamento.Cliente.Name,agendamento.Barbearia.Nome,
			agendamento.Barbeiro.Nome,agendamento.Dia,agendamento.Barbearia.Slug
		));

		return ResultData.Success();
	}

}
