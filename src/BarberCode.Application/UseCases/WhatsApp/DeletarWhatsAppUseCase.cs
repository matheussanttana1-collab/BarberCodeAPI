using BarberCode.Application.Interfaces;
using BarberCode.Domain.Shared;

namespace BarberCode.Application.UseCases.WhatsApp;

public class DeletarWhatsAppUseCase
{
	private readonly IBarbeariaRepository _repo;
	private readonly IWhatsAppService _whatsAppService;

	public DeletarWhatsAppUseCase(IBarbeariaRepository repo, IWhatsAppService whatsAppService)
	{
		_repo = repo;
		_whatsAppService = whatsAppService;
	}

	public async Task<ResultData> ExecuteAsync(Guid id)
	{
		var barbearia = await _repo.BuscarBarbeariaPorAsync(id);

		if (barbearia is null)
			return ResultData.Failure(ResultType.NotFound, "Barbearia Não Cadastrada");

		var deleteResult = await _whatsAppService.DeletarWhatsAppBarbeariaAsync(barbearia.Slug);
		if (!deleteResult.IsSuccess)
			return ResultData.Failure(deleteResult.Type, deleteResult.Message);

		return ResultData.Success();
	}
}
