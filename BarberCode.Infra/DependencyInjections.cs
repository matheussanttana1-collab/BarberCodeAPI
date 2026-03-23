using BarberCode.Application.Interfaces;
using BarberCode.Infra.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace BarberCode.Infra;

public static class DependencyInjections
{
	public static IServiceCollection addInfra(this IServiceCollection services) 
	{
		services.AddScoped<IAgendamentoRepository, AgendamentoRepository>();
		services.AddScoped<IServicoRepository, ServicoRepository>();
		services.AddScoped<IBarbeiroRepository, BarbeiroRepository>();
		services.AddScoped<IBarbeariaRepository, BarbeariaRepository>();
		services.AddScoped<IClienteRepository, ClienteRepository>();

		return services;
	}
}
