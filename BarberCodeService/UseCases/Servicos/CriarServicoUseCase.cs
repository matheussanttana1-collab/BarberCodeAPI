using AutoMapper;
using BarberCode.Application.Interfaces;
using BarberCode.Domain.Entities.Barbearias;
using BarberCode.Service.Requests;

namespace BarberCode.Application.UseCases.Servicos;

public class CriarServicoUseCase
{
	private readonly IServicoRepository _repo;
	private readonly IMapper _mapper;

	public CriarServicoUseCase(IServicoRepository repo
		, IMapper mapper)
	{
		_repo = repo;
		_mapper = mapper;
	}

	public void Execute(Guid BarbeariaId,ServicoRequest request)
	{
		var servico = new Servico(request.Name, request.Duracao, request.Descricao, BarbeariaId);
		
		_repo.SalvarServico(servico);
		
	}


}
