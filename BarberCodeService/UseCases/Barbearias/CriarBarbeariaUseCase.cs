namespace BarberCode.Application.UseCases.Barbearias;
using AutoMapper;
using BarberCode.Application.Interfaces;
using BarberCode.Domain.Entities.Barbearias;
using BarberCode.Domain.Shared;
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

	public async Task<ResultData<Guid>> ExecuteAsync(CriarBarbeariaRequest request)
	{  
		Barbearia barbearia = _mapper.Map<Barbearia>(request);

		await _repository.SalvarBarbeariaAsync(barbearia);

		return ResultData<Guid>.Success(barbearia.Id);
	}
}
