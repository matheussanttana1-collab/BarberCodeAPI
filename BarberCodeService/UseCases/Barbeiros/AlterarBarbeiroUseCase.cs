using BarberCode.Application.Interfaces;
using BarberCode.Application.Requests;
using BarberCode.Domain.Shared;


namespace BarberCode.Application.UseCases.Barbeiros;

public class AlterarBarbeiroUseCase
{
	private readonly IBarbeiroRepository _repo;

	public AlterarBarbeiroUseCase(IBarbeiroRepository repo)
	{
		_repo = repo;
	}

	public async Task<ResultData> ExecuteAsync(Guid id, AtualizarBarbeiroRequest request)
	{
		var barbeiro = await _repo.BuscarBarbeiroPorAsync(id);

		if (barbeiro == null)
			return ResultData.Failure(ResultType.NotFound,"Barbeiro não Encontrado");

		barbeiro.AlterarBarbeiro(request.nome, request.horarioAlmoco);

		await _repo.AtualizarBarbeiroAsync();
		return ResultData.Success();
	} 
}
