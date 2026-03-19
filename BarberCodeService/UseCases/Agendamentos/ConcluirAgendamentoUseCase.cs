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

	public void Execute(Guid Id)
	{
		var agendamento = _repo.BuscarAgendadamentoPorId(Id);

		if (agendamento is null)
			throw new Exception("Agendamento não Encontrado");

		agendamento.ConcluirAgendamento();
		_repo.AtualizarAgendadamento();
	}

}
