using BarberCode.Application.Interfaces;
using BarberCode.Domain.Shared;

namespace BarberCode.Application.UseCases.WhatsApp;

public class GerarNovoQrCodeUseCase
{
	private readonly IBarbeariaRepository _repo;
	private readonly IWhatsAppService _whatsAppService;

	public GerarNovoQrCodeUseCase(IBarbeariaRepository repo, IWhatsAppService whatsAppService)
	{
		_repo = repo;
		_whatsAppService = whatsAppService;
	}

	public async Task<ResultData<string>> ExecuteAsync(Guid id)
	{
		var barbearia = await _repo.BuscarBarbeariaPorAsync(id);

		if (barbearia is null)
			return ResultData<string>.Failure(ResultType.NotFound, "Barbearia Não Cadastrada");

		var qrCodeResult = await _whatsAppService.GerarNovoQrCodeWhatsApp(barbearia.Slug);
		if (!qrCodeResult.IsSuccess)
			return ResultData<string>.Failure(qrCodeResult.Type, qrCodeResult.Message);
		return ResultData<string>.Success(qrCodeResult.Data!);
	}
}
