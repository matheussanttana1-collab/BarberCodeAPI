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
	

	public CriarBarbeiroUseCase(IBarbeiroRepository barbeiroRepo, IAppUserService userService, 
	IBarbeariaRepository barbeariaRepo)
	{
		_barbeiroRepo = barbeiroRepo;
		_userService = userService;
		_barbeariaRepo = barbeariaRepo;
	}

	
	public async Task<ResultData<Guid>> ExecuteAsync(CriarBarbeiroRequest request, Guid barbeariaId)
	{
		var barbearia = await _barbeariaRepo.BuscarBarbeariaPorAsync(barbeariaId);
		if (barbearia == null)
			return ResultData<Guid>.Failure(ResultType.NotFound, "Barbearia não Encontrada");

		var barbeiro = new Barbeiro(request.Nome,request.FotoPerfil,barbeariaId, request.HorarioAlmoco);
		await _barbeiroRepo.SalvarBarbeiroAsync(barbeiro);

		var result = await _userService.CadastrarUsuarioAsync(barbeiro.Id,request.Email, request.Senha,
		TipoUsuario.Barbeiro);

		if (!result.IsSuccess)
			return ResultData<Guid>.Failure(result.Type, result.Message);

		return ResultData<Guid>.Success(barbeariaId);
	}
}