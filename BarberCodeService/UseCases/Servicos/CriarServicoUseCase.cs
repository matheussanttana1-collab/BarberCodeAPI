using AutoMapper;
using BarberCode.Application.Interfaces;
using BarberCode.Domain.Entities.Barbearias;
using BarberCode.Domain.Shared;
using BarberCode.Service.Requests;

namespace BarberCode.Application.UseCases.Servicos;

public class CriarServicoUseCase
{
	private readonly IServicoRepository _repo;


	public CriarServicoUseCase(IServicoRepository repo
		, IMapper mapper)
	{
		_repo = repo;
	}

	public async Task<ResultData<Guid>> ExecuteAsync(Guid barbeariaId,CriarServicoRequest request)
	{
		var servico = new Servico(request.Name, request.Duracao, request.Descricao,request.Preco, barbeariaId);
		
		await _repo.SalvarServicoAsync(servico);

		return ResultData<Guid>.Success(servico.Id);
		
	}


}
