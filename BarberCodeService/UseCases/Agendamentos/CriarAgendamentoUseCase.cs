using BarberCode.Application.Interfaces;
using BarberCode.Service.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberCode.Application.UseCases.Agendamentos;

public class CriarAgendamentoUseCase
{
	private readonly IBarbeiroRepository _repo;

	public CriarAgendamentoUseCase(IBarbeiroRepository repo)
	{
		_repo = repo;
	}
	public void Execute (AgendamentoRequest request)
	{

	}
}
