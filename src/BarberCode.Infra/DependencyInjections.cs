using BarberCode.Application.Interfaces;
using BarberCode.Infra.Banco;
using BarberCode.Infra.Models;
using BarberCode.Infra.Repositories;
using BarberCode.Infra.Service;
using BarberCode.Infra.Service.EmailServices;
using BarberCode.Infra.Service.WhatsAppServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BarberCode.Infra;

public static class DependencyInjections
{
	/// <summary>
	/// Método principal que configura toda a camada de infraestrutura
	/// </summary>
	public static IServiceCollection addInfra(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddDatabase(configuration);
		services.AddRepositories();
		services.AddServices();
		services.AddEmailSettings(configuration);
		services.AddWhatsAppSettings(configuration);

		return services;
	}

	/// <summary>
	/// Configura o banco de dados (DbContext, Identity e string de conexão)
	/// </summary>
	private static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
	{
		var connectionString = configuration.GetConnectionString("BarberCodeConnection");

		services.AddDbContext<BarberCodeContext>(opts => 
			opts.UseLazyLoadingProxies()
				.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

		services.AddIdentity<AppUser, IdentityRole>()
			.AddEntityFrameworkStores<BarberCodeContext>()
			.AddDefaultTokenProviders();

		return services;
	}

	/// <summary>
	/// Registra todos os repositórios
	/// </summary>
	private static IServiceCollection AddRepositories(this IServiceCollection services)
	{
		services.AddScoped<IAgendamentoRepository, AgendamentoRepository>();
		services.AddScoped<IServicoRepository, ServicoRepository>();
		services.AddScoped<IBarbeiroRepository, BarbeiroRepository>();
		services.AddScoped<IBarbeariaRepository, BarbeariaRepository>();
		services.AddScoped<IClienteRepository, ClienteRepository>();

		return services;
	}

	/// <summary>
	/// Registra todos os serviços (Services)
	/// </summary>
	private static IServiceCollection AddServices(this IServiceCollection services)
	{
		services.AddScoped<IAppUserService, AppUserService>();
		services.AddScoped<ITokenService, TokenService>();
		services.AddScoped<IEmailService, EmailService>();
		services.AddScoped<IEmailTemplateService, EmailTemplateService>();

		return services;
	}

	/// <summary>
	/// Configura as opções de email (EmailSettings)
	/// </summary>
	private static IServiceCollection AddEmailSettings(this IServiceCollection services, IConfiguration configuration)
	{
		services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));

		return services;
	}

	/// <summary>
	/// Configura as opções de WhatsApp (WhatsAppSettings)
	/// </summary>
	private static IServiceCollection AddWhatsAppSettings(this IServiceCollection services, IConfiguration configuration)
	{
		var baseUrl = configuration["WHATSAPP_BASE_URL"];
		var apiKey = configuration["WHATSAPP_API_KEY"];

	

		services.AddHttpClient<IWhatsAppService, WhatsAppService>(client =>
		{
			client.BaseAddress = new Uri(baseUrl!);
			client.DefaultRequestHeaders.Add("apikey", apiKey);
		});

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
