using BarberCode.Application.Interfaces;
using BarberCode.Domain.Shared;
using System;
namespace BarberCode.Application.UseCases.Barbearias;
public class CadastrarWhatsApp
{
	private readonly IBarbeariaRepository _repo;
	private readonly IWhatsAppService _whatsAppService;

	public CadastrarWhatsApp(IBarbeariaRepository repo, IWhatsAppService whatsAppService)
	{
		_repo = repo;
		_whatsAppService = whatsAppService;
	}

	public async Task<ResultData<string>> ExecuteAsync(Guid id)
	{
		var barbearia = await _repo.BuscarBarbeariaPorAsync(id);

		if (barbearia is null)
			return ResultData<string>.Failure(ResultType.NotFound, "Barbearia Não Cadastrada");
	
		var qrCodeResult = await _whatsAppService.GerarQrCodeDeCadastroWhatsApp(barbearia.Slug);
		if (!qrCodeResult.IsSuccess)
			return ResultData<string>.Failure(qrCodeResult.Type, qrCodeResult.Message);
		return ResultData<string>.Success(qrCodeResult.Data!);
	}
}
