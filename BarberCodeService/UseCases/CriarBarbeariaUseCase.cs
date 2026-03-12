using AutoMapper;
using BarberCode.Application.Interfaces;
using BarberCode.Domain.Entities.Barbearias;
using BarberCode.Service.Requests;

namespace BarberCodeService.UseCases;

public class CriarBarbeariaUseCase
{
	private readonly IBarbeariaRepository _repository;
	private readonly IMapper _mapper;

	public CriarBarbeariaUseCase(IBarbeariaRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public void Execute(BarbeariaRequest request)
	{ 
		Barbearia barbearia = _mapper.Map<Barbearia>(request);

		_repository.SalvarBarbearia(barbearia);
	}
}
