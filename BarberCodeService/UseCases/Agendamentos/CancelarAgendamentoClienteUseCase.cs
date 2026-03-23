using BarberCode.Application.Interfaces;

namespace BarberCode.Application.UseCases.Agendamentos;

public class CancelarAgendamentoClienteUseCase
{
	private readonly IAgendamentoRepository _repo;

	public CancelarAgendamentoClienteUseCase(IAgendamentoRepository repo)
	{
		_repo = repo;
	}

	public async Task ExecuteAsync(Guid clienteId, Guid agendamentoId)
	{
		var agendamento = await _repo.BuscarAgendadamentoPorIdAsync(agendamentoId);

		if (agendamento is null)
			throw new Exception("Agendamento não Encontrado");

		agendamento.ValidarCancelamento(clienteId);

		await _repo.DeletarAgendadamentoAsync(agendamento);
	}

}
