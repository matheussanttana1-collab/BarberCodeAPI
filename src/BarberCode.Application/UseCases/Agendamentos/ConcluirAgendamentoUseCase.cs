using BarberCode.Application.Interfaces;
using BarberCode.Application.EventsHandlers;
using BarberCode.Domain.Shared;

namespace BarberCode.Application.UseCases.Agendamentos;

public class ConcluirAgendamentoUseCase
{
	private readonly IAgendamentoRepository _repo;
	private readonly IBarbeariaRepository _barbeariaRepo;
	private readonly IEventBus _eventBus;

	public ConcluirAgendamentoUseCase(IAgendamentoRepository repo, IBarbeariaRepository barbeariaRepo, IEventBus eventBus)
	{
		_repo = repo;
		_barbeariaRepo = barbeariaRepo;
		_eventBus = eventBus;
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

		// Publica evento para enviar WhatsApp
		await _eventBus.PublishAsync(new ConcluirAgendamentoEvent(
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
