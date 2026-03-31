using BarberCode.Application.Interfaces;
using BarberCode.Infra.Banco;
using BarberCode.Infra.Models;
using BarberCode.Infra.Repositories;
using BarberCode.Infra.Service;
using Microsoft.AspNetCore.Identity;
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
		services.AddScoped<IAppUserService, AppUserService>();
		services.AddScoped<ITokenService, TokenService>();
		services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<BarberCodeContext>()
		.AddDefaultTokenProviders();

		return services;
	}
}
