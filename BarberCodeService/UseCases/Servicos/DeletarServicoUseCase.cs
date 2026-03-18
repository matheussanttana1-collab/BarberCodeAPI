using BarberCode.Application.Interfaces;


namespace BarberCode.Application.UseCases.Servicos;

public class DeletarServicoUseCase
{
	private readonly IServicoRepository _repo;

	public DeletarServicoUseCase(IServicoRepository repo)
	{
		_repo = repo;
	}

	public void Execute (Guid id)
	{
		var servico = _repo.BuscarServicoPor(id);

		if (servico is null)
		{
			throw new Exception("Servico não Encontrado");
		}

		_repo.DeletarServico(servico);	
	}
}
