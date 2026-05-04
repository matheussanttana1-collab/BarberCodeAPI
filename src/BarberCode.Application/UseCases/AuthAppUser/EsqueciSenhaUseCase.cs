using BarberCode.Application.Interfaces;
using BarberCode.Application.EventsHandlers;
using BarberCode.Domain.Shared;

namespace BarberCode.Application.UseCases.AuthAppUser;

/// <summary>
/// UseCase para esqueci de senha
/// Reseta a senha para um padrão temporário
/// </summary>
public class EsqueciSenhaUseCase
{
	private readonly IAppUserService _userService;
	private readonly IEventBus _eventBus;

 public EsqueciSenhaUseCase(IAppUserService userService, IEventBus eventBus)
	{
		_userService = userService;
		_eventBus = eventBus;
	}

	public async Task<ResultData<string>> ExecuteAsync(string email)
	{
		// 1️ Valida input
		if (string.IsNullOrWhiteSpace(email))
			return ResultData<string>.Failure(ResultType.Validation, "Email é obrigatório");

		// 2️ Busca o usuário pelo email
		var user = await _userService.BuscarPeloEmailAsync(email);
		if (user is null)
			return ResultData<string>.Failure(ResultType.NotFound, "Email não encontrado");

		var token = await _userService.GerarTokenDeResetSenhaAsync(user);

		// 3️ Publica evento para enviar email
		await _eventBus.PublishAsync(new EmailResetSenhaEvent(email, token));

		return ResultData<string>.Success("Email Enviado Com Sucesso");
	}
}
