using AutoMapper;
using BarberCode.Application.Interfaces;
using BarberCode.Domain.Entities.Barbearias;
using BarberCode.Service.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberCode.Application.UseCases.Barbearias;

public class AlterarHorarioFuncionamentoUseCase
{
	private readonly IBarbeariaRepository _repo;
	private readonly IMapper _mapper;


	public AlterarHorarioFuncionamentoUseCase(IBarbeariaRepository repo)
	{
		_repo = repo;
	}

	public void Execute (Guid id, List<HorarioFuncionamentoRequest> request)
	{
		var barbearia = _repo.BuscarBarbeariaPor(id);
		if (barbearia == null)
			throw new Exception("Barbearia não Encontrado");
		var horarios = request
		.Select(h => new HorarioFuncionamento(h.Dia, h.Incio, h.Fim))
		.ToList();

		barbearia.AlterarFuncionamento(horarios);

		_repo.AtualizarBarbearia();
	}	
}
