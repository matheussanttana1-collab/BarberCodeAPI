using BarberCode.Application.Interfaces;
using BarberCode.Domain.Shared;

namespace BarberCode.Application.UseCases.WhatsApp;

public class LogoutWhatsAppUseCase
{
	private readonly IBarbeariaRepository _repo;
	private readonly IWhatsAppService _whatsAppService;

	public LogoutWhatsAppUseCase(IBarbeariaRepository repo, IWhatsAppService whatsAppService)
	{
		_repo = repo;
		_whatsAppService = whatsAppService;
	}

	public async Task<ResultData> ExecuteAsync(Guid id)
	{
		var barbearia = await _repo.BuscarBarbeariaPorAsync(id);

		if (barbearia is null)
			return ResultData.Failure(ResultType.NotFound, "Barbearia Não Cadastrada");

		var logoutResult = await _whatsAppService.LogoutWhatsAppBarbeariaAsync(barbearia.Slug);
		if (!logoutResult.IsSuccess)
			return ResultData.Failure(logoutResult.Type, logoutResult.Message);

		return ResultData.Success();
	}
}
