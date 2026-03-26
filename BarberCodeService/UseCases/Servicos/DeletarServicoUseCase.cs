using BarberCode.Application.Interfaces;
using BarberCode.Domain.Shared;


namespace BarberCode.Application.UseCases.Servicos;

public class DeletarServicoUseCase
{
	private readonly IServicoRepository _repo;

	public DeletarServicoUseCase(IServicoRepository repo)
	{
		_repo = repo;
	}

	public async Task<ResultData> ExecuteAsync (Guid id)
	{
		var servico = await _repo.BuscarServicoPorAsync(id);

		if (servico is null)
			return ResultData.Failure(ResultType.NotFound, "Servico encontrado");
		await _repo.DeletarServicoAsync(servico);

		return ResultData.Success();
	}
}
