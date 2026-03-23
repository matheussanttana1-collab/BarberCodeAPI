using BarberCode.Application.Interfaces;
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

	public async Task ExecuteAsync(Guid Id)
	{
		var agendamento = await _repo.BuscarAgendadamentoPorIdAsync(Id);

		if (agendamento is null)
			throw new Exception("Agendamento não Encontrado");

		agendamento.ConcluirAgendamento();
		await _repo.AtualizarAgendadamentoAsync();
	}

}
