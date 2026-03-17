namespace BarberCode.Application.UseCases;
using AutoMapper;
using BarberCode.Application.Interfaces;
using BarberCode.Domain.Entities.Barbearias;
using BarberCode.Service.Requests;

public class CriarBarbeariaUseCase
{
	private readonly IBarbeariaRepository _repository;
	private readonly IMapper _mapper;

	public CriarBarbeariaUseCase(IBarbeariaRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public Barbearia Execute(BarbeariaRequest request)
	{  
		Barbearia barbearia = _mapper.Map<Barbearia>(request);

		_repository.SalvarBarbearia(barbearia);

		return barbearia;
	}
}
