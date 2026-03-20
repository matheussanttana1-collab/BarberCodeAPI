using BarberCode.Application.Interfaces;

namespace BarberCode.Application.UseCases.Agendamentos;

public class CancelarAgendamentoClienteUseCase
{
	private readonly IAgendamentoRepository _repo;

	public CancelarAgendamentoClienteUseCase(IAgendamentoRepository repo)
	{
		_repo = repo;
	}

	public void Execute(Guid clienteId, Guid agendamentoId)
	{

		var agendamento = _repo.BuscarAgendadamentoPorId(agendamentoId);

		if (agendamento is null)
			throw new Exception("Agendamento não Encontrado");

		agendamento.ValidarCancelamento(clienteId);

		_repo.DeletarAgendadamento(agendamento);
	}

}
