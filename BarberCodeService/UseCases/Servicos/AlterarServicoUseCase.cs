using BarberCode.Application.Interfaces;
using BarberCode.Application.Requests;


namespace BarberCode.Application.UseCases.Servicos;

public class AlterarServicoUseCase
{
	private readonly IServicoRepository _repo;

	public AlterarServicoUseCase(IServicoRepository repo)
	{
		_repo = repo;
	}

	public void Execute (Guid id, AtualizarServicoRequest request) 
	{
		var servico = _repo.BuscarServicoPor(id);
		if (servico is null)
			throw new Exception("Servico nao Encontrado");

		servico.AlterarServico(request.name, request.descricao, request.duracao);

		_repo.AtualizarServico();
	}
}
