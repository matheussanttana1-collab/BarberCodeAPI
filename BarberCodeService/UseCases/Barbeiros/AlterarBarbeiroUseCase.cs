using BarberCode.Application.Interfaces;
using BarberCode.Application.Requests;


namespace BarberCode.Application.UseCases.Barbeiros;

public class AlterarBarbeiroUseCase
{
	private readonly IBarbeiroRepository _repo;

	public AlterarBarbeiroUseCase(IBarbeiroRepository repo)
	{
		_repo = repo;
	}

	public async Task ExecuteAsync(Guid id, AtualizarBarbeiroRequest request)
	{
		var barbeiro = await _repo.BuscarBarbeiroPorAsync(id);

		if (barbeiro == null)
			throw new Exception("Barbeiro não Encontrado");

		barbeiro.AlterarBarbeiro(request.nome, request.horarioAlmoco);

		await _repo.AtualizarBarbeiroAsync();
	} 
}
