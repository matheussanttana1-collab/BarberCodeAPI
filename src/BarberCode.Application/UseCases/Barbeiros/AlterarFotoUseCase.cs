using BarberCode.Application.Interfaces;
using System.Net.Http.Headers;

namespace BarberCode.Application.UseCases.Barbeiros;

public class AlterarFotoUseCase
{
	private readonly IBarbeiroRepository _repo;

	public AlterarFotoUseCase(IBarbeiroRepository repo)
	{
		_repo = repo;
	}

	public async Task ExecuteAsync(Guid id, Stream foto, string nomeArquivo) 
	{
		var barbeiro = await _repo.BuscarBarbeiroPorAsync(id);

		if (barbeiro == null)
			throw new Exception("Barbeiro não Encontrado");

		var extencoesPermitidas = new[] { ".jpg", ".jpeg", ".png" };
		var extencao = Path.GetExtension(nomeArquivo).ToLower();
		if(!extencoesPermitidas.Contains(extencao)) 
			throw new Exception("Formato de imagem invalido");

		if (foto.Length > 5 * 1024 * 1024)
			throw new Exception("Foto deve ter no Maximo 5MB");

		//var nomeUnico = $"{newGuid}"
	}

}
