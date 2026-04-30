using AutoMapper;
using BarberCode.Application.Interfaces;
using BarberCode.Application.Responses;
using BarberCode.Domain.Entities.Barbearias;
using BarberCode.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberCode.Application.UseCases.Barbeiros;

public class GerarSlotsUseCase
{
	private readonly IBarbeiroRepository _repo;


	public GerarSlotsUseCase(IBarbeiroRepository repo)
	{
		_repo = repo;
	
	}

	public async Task<ResultData<GerarSlotsResponse>> ExecuteAsync(Guid barbeiroId, Guid servicoId, DateOnly dia)
	{

		var barbeiro = await _repo.BuscarBarbeiroPorAsync(barbeiroId);
		if (barbeiro is null)
			return ResultData<GerarSlotsResponse>.Failure(ResultType.NotFound, "Barbeiro não encontrado");
		var barbearia = barbeiro.BarbeariaQueTrabalha;
		var servico = barbearia.Servicos.FirstOrDefault(s => s.Id == servicoId);
		if (servico is null)
			return ResultData<GerarSlotsResponse>.Failure(ResultType.NotFound, "Barbearia não oferece esse Serviço");
		;

		var expedienteDia = barbearia.ExpedienteDia(dia);
		if (expedienteDia is null)
			return ResultData<GerarSlotsResponse>.Success(new GerarSlotsResponse(barbeiroId, dia, []));

		var slots = barbeiro.HorariosDisponiveis(expedienteDia.Inicio, expedienteDia.Fim, dia, 
		servico.Duracao);

		return ResultData<GerarSlotsResponse>.Success(new GerarSlotsResponse(barbeiroId, dia, slots));
	}
}


