using BarberCode.Application.Interfaces;
using BarberCode.Domain.Shared;

namespace BarberCode.Application.UseCases.WhatsApp;

public class BuscarStatusConexaoWhatsAppUseCase
{
	private readonly IBarbeariaRepository _repo;
	private readonly IWhatsAppService _whatsAppService;

	public BuscarStatusConexaoWhatsAppUseCase(IBarbeariaRepository repo, IWhatsAppService whatsAppService)
	{
		_repo = repo;
		_whatsAppService = whatsAppService;
	}

	public async Task<ResultData<string>> ExecuteAsync(Guid id)
	{
		var barbearia = await _repo.BuscarBarbeariaPorAsync(id);

		if (barbearia is null)
			return ResultData<string>.Failure(ResultType.NotFound, "Barbearia Não Cadastrada");

		var statusResult = await _whatsAppService.BuscarStatusConexaoWhatsApp(barbearia.Slug);
		if (!statusResult.IsSuccess)
			return ResultData<string>.Failure(statusResult.Type, statusResult.Message);

		return ResultData<string>.Success(statusResult.Data!);
	}
}
