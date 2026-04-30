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

	public async Task<ResultData> ExecuteAsync (Guid id, Guid barbeariaId)
	{
		var servico = await _repo.BuscarServicoPorAsync(id);

		if (servico is null)
			return ResultData.Failure(ResultType.NotFound, "Servico encontrado");
		if (servico.BarbeariaId != barbeariaId)
		{
			return ResultData.Failure(ResultType.Forbidden, "Você não tem permissão para deletar serviços de" +
			" outra barbearia.");
		}
		await _repo.DeletarServicoAsync(servico);

		return ResultData.Success();
	}
}
