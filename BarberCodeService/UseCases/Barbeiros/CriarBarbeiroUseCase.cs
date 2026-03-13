using AutoMapper;
using BarberCode.Application.Interfaces;
using BarberCode.Domain.Entities.Barbeiros;
using BarberCode.Service.Requests;

namespace BarberCode.Application.UseCases.Barbeiros;

public class CriarBarbeiroUseCase
{

	private readonly IBarbeiroRepository _barbeiroRepo;
	private readonly IMapper _mapper;

	public CriarBarbeiroUseCase(IBarbeiroRepository barbeiroRepo
		, IMapper mapper)
	{
		_barbeiroRepo = barbeiroRepo;
		_mapper = mapper;
	}

	public void Execute(BarbeiroRequest request, Guid barbeariaId)
	{
		var barbeiro = new Barbeiro(request.Nome,request.FotoPerfil,barbeariaId, request.HorarioAlmoco);

		_barbeiroRepo.SalvarBarbeiro(barbeiro);

	}
}