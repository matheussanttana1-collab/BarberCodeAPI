using AutoMapper;
using BarberCode.Application.Interfaces;
using BarberCode.Domain.Entities.Barbeiros;
using BarberCode.Service.Requests;

namespace BarberCode.Application.UseCases.Barbeiros;

public class CriarBarbeiroUseCase
{

	private readonly IBarbeiroRepository _barbeiroRepo;
	

	public CriarBarbeiroUseCase(IBarbeiroRepository barbeiroRepo)
	{
		_barbeiroRepo = barbeiroRepo;

	}

	public async Task<Guid> ExecuteAsync(CriarBarbeiroRequest request, Guid barbeariaId)
	{
		var barbeiro = new Barbeiro(request.Nome,request.FotoPerfil,barbeariaId, request.HorarioAlmoco);

		await _barbeiroRepo.SalvarBarbeiroAsync(barbeiro);

		return barbeiro.Id;
	}
}