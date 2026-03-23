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

	public async Task ExecuteAsync (Guid id, AtualizarServicoRequest request) 
	{
		var servico = await _repo.BuscarServicoPorAsync(id);
		if (servico is null)
			throw new Exception("Servico nao Encontrado");

		servico.AlterarServico(request.name, request.descricao, request.duracao);

		await _repo.AtualizarServicoAsync();
	}
}
