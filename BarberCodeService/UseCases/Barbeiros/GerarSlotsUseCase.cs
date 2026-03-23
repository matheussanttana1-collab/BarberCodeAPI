using AutoMapper;
using BarberCode.Application.Interfaces;
using BarberCode.Application.Responses;
using BarberCode.Domain.Entities.Barbearias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberCode.Application.UseCases.Barbeiros;

public class GerarSlotsUseCase
{
	private readonly IBarbeariaRepository _barbeariaRepo;
	private readonly IBarbeiroRepository _barbeiroRep;


	public GerarSlotsUseCase(IBarbeariaRepository barbeariaRepo, IBarbeiroRepository barbeiroRep)
	{
		_barbeariaRepo = barbeariaRepo;
		_barbeiroRep = barbeiroRep;
	
	}

	public async Task<GerarSlotsResponse> ExecuteAsync(Guid barbeiroId, Guid barbeariaId, Guid servicoId, DateOnly dia)
	{
		var barbearia = await _barbeariaRepo.BuscarBarbeariaPorAsync(barbeariaId);
		if (barbearia is null)
			throw new Exception("Barbearia não encontrada.");

		var servico = barbearia.Servicos.FirstOrDefault(s => s.Id == servicoId);
		if (servico is null)
			throw new Exception("Barbearia não oferece este serviço.");

		var barbeiro = barbearia.Barbeiros.FirstOrDefault(b => b.Id == barbeiroId);
		if (barbeiro is null)
			throw new Exception("Barbeiro não cadastrado ou não pertence a esta barbearia.");

		var expedienteDia = barbearia.ExpedienteDia(dia);
		if (expedienteDia is null)
			return new GerarSlotsResponse(barbeiroId, dia, []);

		var slots = barbeiro.HorariosDisponiveis(expedienteDia.Incio, expedienteDia.Fim, dia, servico.Duracao);

		return new GerarSlotsResponse(barbeiroId, dia, slots);
	}
}


