namespace BarberCode.Application.UseCases.Barbearias;
using AutoMapper;
using BarberCode.Application.Interfaces;
using BarberCode.Application.Models;
using BarberCode.Domain.Entities.Barbearias;
using BarberCode.Domain.Shared;
using BarberCode.Service.Requests;

public class CriarBarbeariaUseCase
{
	private readonly IBarbeariaRepository _repository;
	private readonly IMapper _mapper;
	private readonly IAppUserService _userService;

	public CriarBarbeariaUseCase(IBarbeariaRepository repository, IMapper mapper, IAppUserService userService)
	{
		_repository = repository;
		_mapper = mapper;
		_userService = userService;
	}

	public async Task<ResultData<Guid>> ExecuteAsync(CriarBarbeariaRequest request)
	{
		var endereco = _mapper.Map<Endereco>(request.Endereco);
		var funcionamenro = _mapper.Map<List<HorarioFuncionamento>>(request.Funcionamento);
		Barbearia barbearia = new Barbearia(request.Name, endereco, funcionamenro, request.Celular);

		var result = await _userService.CadastrarUsuarioAsync
		(barbearia.Id,request.Email,request.Senha, TipoUsuario.Barbearia)
		;
		if (!result.IsSuccess)
			return ResultData<Guid>.Failure(result.Type, result.Message);

		await _repository.SalvarBarbeariaAsync(barbearia);

		return ResultData<Guid>.Success(barbearia.Id);
	}
}
