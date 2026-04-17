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
	private readonly IEmailService _emailService;
	private readonly IEmailTemplateService _emailTemplateService;

  public CriarBarbeariaUseCase(IBarbeariaRepository repository, IMapper mapper, IAppUserService userService,
	IEmailService emailService, IEmailTemplateService emailTemplateService)
	{
		_repository = repository;
		_mapper = mapper;
		_userService = userService;
		_emailService = emailService;
		_emailTemplateService = emailTemplateService;
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

		var body = _emailTemplateService.gerarTemplateBoasVindasBarbearia(barbearia.Nome);
		await _emailService.SendEmailAsync(request.Email, "Bem-vindo à BarberCode", body);

		return ResultData<Guid>.Success(barbearia.Id);
	}
}
