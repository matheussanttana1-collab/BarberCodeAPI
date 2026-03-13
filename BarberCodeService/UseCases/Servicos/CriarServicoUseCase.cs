using AutoMapper;
using BarberCode.Application.Interfaces;

namespace BarberCode.Application.UseCases.Servicos;

public class CriarServicoUseCase
{
	private readonly IBarbeariaRepository _repo;
	private readonly IMapper _mapper;

	public CriarServicoUseCase(IBarbeariaRepository repo
		, IMapper mapper)
	{
		_repo = repo;
		_mapper = mapper;
	}




}
