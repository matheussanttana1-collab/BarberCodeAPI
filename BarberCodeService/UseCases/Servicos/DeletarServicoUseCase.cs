using BarberCode.Application.Interfaces;


namespace BarberCode.Application.UseCases.Servicos;

public class DeletarServicoUseCase
{
	private readonly IServicoRepository _repo;

	public DeletarServicoUseCase(IServicoRepository repo)
	{
		_repo = repo;
	}

	public async Task ExecuteAsync (Guid id)
	{
		var servico = await _repo.BuscarServicoPorAsync(id);

		if (servico is null)
		{
			throw new Exception("Servico não Encontrado");
		}

		await _repo.DeletarServicoAsync(servico);	
	}
}
