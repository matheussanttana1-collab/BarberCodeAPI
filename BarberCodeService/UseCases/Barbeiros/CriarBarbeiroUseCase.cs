using AutoMapper;
using BarberCode.Application.Interfaces;
using BarberCode.Application.Models;
using BarberCode.Domain.Entities.Barbeiros;
using BarberCode.Domain.Shared;
using BarberCode.Service.Requests;

namespace BarberCode.Application.UseCases.Barbeiros;

public class CriarBarbeiroUseCase
{

	private readonly IBarbeiroRepository _barbeiroRepo;
	private readonly IBarbeariaRepository _barbeariaRepo;
	private readonly IAppUserService _userService;
	private readonly IEmailService _emailService;
	private readonly IEmailTemplateService _emailTemplateService;
	

	public CriarBarbeiroUseCase(IBarbeiroRepository barbeiroRepo, IAppUserService userService, 
 IBarbeariaRepository barbeariaRepo, IEmailService emailService, IEmailTemplateService emailTemplateService)
	{
		_barbeiroRepo = barbeiroRepo;
		_userService = userService;
		_barbeariaRepo = barbeariaRepo;
       _emailService = emailService;
		_emailTemplateService = emailTemplateService;
	}

	
	public async Task<ResultData<Guid>> ExecuteAsync(CriarBarbeiroRequest request, Guid barbeariaId)
	{
		var barbearia = await _barbeariaRepo.BuscarBarbeariaPorAsync(barbeariaId);
		if (barbearia == null)
			return ResultData<Guid>.Failure(ResultType.NotFound, "Barbearia não Encontrada");

		var barbeiro = new Barbeiro(request.Nome,request.FotoPerfil,barbeariaId, request.HorarioAlmoco);

		var result = await _userService.CadastrarUsuarioAsync(barbeiro.Id,request.Email, request.Senha,
		TipoUsuario.Barbeiro);

		if (!result.IsSuccess)
			return ResultData<Guid>.Failure(result.Type, result.Message);

		await _barbeiroRepo.SalvarBarbeiroAsync(barbeiro);

        var body = _emailTemplateService.gerarTemplateBoasVindasBarbeiro(barbeiro.Nome, barbearia.Nome);
		await _emailService.sendEmailAsync(request.Email, "Bem-vindo à BarberCode", body);

		return ResultData<Guid>.Success(barbeariaId);
	}
}