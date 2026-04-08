using BarberCode.Application.UseCases.Agendamentos;
using BarberCode.Application.UseCases.AuthAppUser;
using BarberCode.Application.UseCases.Barbearias;
using BarberCode.Application.UseCases.Barbeiros;
using BarberCode.Application.UseCases.Servicos;
using Microsoft.Extensions.DependencyInjection;

namespace BarberCode.Application;

public static class DependencyInjection
{
	public static IServiceCollection AddApplication(
	this IServiceCollection services)
	{
	 // Barbearias

	 // Barbeiros

	 // Agendamentos

	 // Servicos

	 // AppUser
		services.AddScoped<CriarBarbeariaUseCase>();
		services.AddScoped<CriarBarbeiroUseCase>();
		services.AddScoped<CriarServicoUseCase>();
		services.AddScoped<CriarAgendamentoUseCase>();
		services.AddScoped<GerarSlotsUseCase>();
		services.AddScoped<DeletarBarbeariaUseCase>();
		services.AddScoped<DeletarBarbeiroUseCase>();
		services.AddScoped<DeletarServicoUseCase>();
		services.AddScoped<CancelarAgendamentoUseCase>();
		services.AddScoped<CancelarAgendamentoClienteUseCase>();
		services.AddScoped<AlterarEnderecoUseCase>();
		services.AddScoped<AlterarServicoUseCase>();
		services.AddScoped<AlterarHorarioFuncionamentoUseCase>();
		services.AddScoped<AlterarBarbeiroUseCase>();
		services.AddScoped<ConcluirAgendamentoUseCase>();
		services.AddScoped<LoginUseCase>();
		services.AddScoped<LoginClienteUseCase>();
		services.AddScoped<ListarAgendamentosBarbeiroUseCase>();
		services.AddScoped<AlterarSenhaUseCase>();
		services.AddScoped<EsqueciSenhaUseCase>();
		services.AddScoped<RefreshTokenUseCase>();
		services.AddScoped<CadastrarWhatsApp>();
		return services;
	}

}
