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
		services.AddScoped<IEmailService, EmailService>();
		services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<BarberCodeContext>()
		
		.AddDefaultTokenProviders();

		return services;
	}

	/// <summary>
	/// Cria as roles padrão do sistema de forma idempotente
	/// </summary>
	public static async Task SeedRolesAsync(this IServiceProvider serviceProvider)
	{
		using (var scope = serviceProvider.CreateScope())
		{
			var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

			// Lista de roles a serem criadas
			var roles = new[] { Roles.Barbearia, Roles.Barbeiro, Roles.Cliente };

			foreach (var role in roles)
			{
				// Verifica se a role já existe
				if (!await roleManager.RoleExistsAsync(role))
				{
					await roleManager.CreateAsync(new IdentityRole { Name = role });
				}
			}
		}
	}
}
