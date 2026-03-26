using BarberCode.Application.Interfaces;
using BarberCode.Application.Requests;
using BarberCode.Domain.Shared;


namespace BarberCode.Application.UseCases.Servicos;

public class AlterarServicoUseCase
{
	private readonly IServicoRepository _repo;

	public AlterarServicoUseCase(IServicoRepository repo)
	{
		_repo = repo;
	}

	public async Task<ResultData>ExecuteAsync (Guid id, AtualizarServicoRequest request) 
	{
		var servico = await _repo.BuscarServicoPorAsync(id);
		if (servico is null)
			return ResultData.Failure(ResultType.NotFound, "Servico encontrado");
		servico.AlterarServico(request.name, request.descricao, request.duracao);

		await _repo.AtualizarServicoAsync();

		return ResultData.Success();
	}
}
