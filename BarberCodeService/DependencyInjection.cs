using BarberCode.Application.EventsHandlers;
using BarberCode.Application.Interfaces;
using BarberCode.Application.Profiles;
using BarberCode.Application.UseCases.Agendamentos;
using BarberCode.Application.UseCases.AuthAppUser;
using BarberCode.Application.UseCases.Barbearias;
using BarberCode.Application.UseCases.Barbeiros;
using BarberCode.Application.UseCases.Servicos;
using BarberCode.Application.UseCases.WhatsApp;
using Microsoft.Extensions.DependencyInjection;

namespace BarberCode.Application;

public static class DependencyInjection
{
	public static IServiceCollection AddApplication(this IServiceCollection services)
	{
		services.AddAutoMapperConfiguration();
		services.AddUseCases();
		services.AddEvents();

		return services;
	}

	/// <summary>
	/// Configura o AutoMapper com todos os profiles
	/// </summary>
	private static IServiceCollection AddAutoMapperConfiguration(this IServiceCollection services)
	{
		services.AddAutoMapper(cfg => { }, typeof(BarbeariaProfile));

		return services;
	}

	/// <summary>
	/// Registra todos os UseCases da aplicação
	/// </summary>
	private static IServiceCollection AddUseCases(this IServiceCollection services)
	{
		// Barbearias
		services.AddScoped<CriarBarbeariaUseCase>();
		services.AddScoped<DeletarBarbeariaUseCase>();
		services.AddScoped<AlterarEnderecoUseCase>();
		services.AddScoped<AlterarHorarioFuncionamentoUseCase>();

		// Barbeiros
		services.AddScoped<CriarBarbeiroUseCase>();
		services.AddScoped<DeletarBarbeiroUseCase>();
		services.AddScoped<AlterarBarbeiroUseCase>();

		// Serviços
		services.AddScoped<CriarServicoUseCase>();
		services.AddScoped<DeletarServicoUseCase>();
		services.AddScoped<AlterarServicoUseCase>();

		// Agendamentos
		services.AddScoped<CriarAgendamentoUseCase>();
		services.AddScoped<CancelarAgendamentoUseCase>();
		services.AddScoped<CancelarAgendamentoClienteUseCase>();
		services.AddScoped<ConcluirAgendamentoUseCase>();
		services.AddScoped<GerarSlotsUseCase>();
		services.AddScoped<ListarAgendamentosBarbeiroUseCase>();

		// Autenticação
		services.AddScoped<LoginUseCase>();
		services.AddScoped<LoginClienteUseCase>();
		services.AddScoped<AlterarSenhaUseCase>();
		services.AddScoped<EsqueciSenhaUseCase>();
		services.AddScoped<RefreshTokenUseCase>();

		// WhatsApp
		services.AddScoped<CadastrarWhatsAppUseCase>();
		services.AddScoped<GerarNovoQrCodeUseCase>();
		services.AddScoped<LogoutWhatsAppUseCase>();
		services.AddScoped<BuscarStatusConexaoWhatsAppUseCase>();
		services.AddScoped<DeletarWhatsAppUseCase>();

		return services;
	}

	private static IServiceCollection AddEvents(this IServiceCollection services) 
	{
		services.AddScoped<IEventBus, MyEventBus>();
		services.AddScoped<IEventHandler<EnviarMensagemEvent>, EnviarWhatsAppHandler>();
		services.AddScoped<IEventHandler<EmailBoasVindasEvent>, EmailBoasVindasHandler>();

		return services;
	}
}
