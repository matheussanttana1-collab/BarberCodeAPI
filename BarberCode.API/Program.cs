using BarberCode.API.Endpoins;
using BarberCode.Application;
using BarberCode.Application.Profiles;
using BarberCode.Application.Validators;
using BarberCode.Infra;
using BarberCode.Infra.Banco;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("BarberCodeConnection");
builder.Services.AddDbContext<BarberCodeContext>(opts => opts.UseLazyLoadingProxies().UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
builder.Services.AddAutoMapper(cfg => { }, typeof(BarbeariaProfile));

builder.Services.AddApplication();
builder.Services.addInfra();
builder.Services.AddValidatorsFromAssemblyContaining<CriarBarbeariaValidator>();

builder.Services.AddAuthentication(
opitions => opitions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer
(opts => opts.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
{
	ValidateIssuerSigningKey = true,
	IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("nisjdajsdçajdsdsadsadsasdsadadsaffasfsda")),
	ValidateAudience = false,
	ValidateIssuer = false,
	ClockSkew = TimeSpan.Zero
});

//builder.Services.AddAuthorization(opts =>
//{
//	opts.AddPolicy("admin", policy => policy.RequireRole("barbeariaUser"));
//	opts.AddPolicy("employee", policy => policy.RequireRole("barbeiroUser"));
//	opts.AddPolicy("user", policy => policy.RequireRole("clienteUser"));
//});

builder.Services.AddControllers();

// Configurar OpenAPI e Swagger
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen(c =>
{
	c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
	{
		Title = "BarberCode API",
		Version = "v1",
		Description = "API para gerenciamento de barbearias e agendamentos"
	});
});





var app = builder.Build();

// Seed roles padrão do sistema
await app.Services.SeedRolesAsync();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.MapOpenApi();

	// Configurar Swagger UI
	app.UseSwagger();
	app.UseSwaggerUI(c =>
	{
		c.SwaggerEndpoint("/swagger/v1/swagger.json", "BarberCode API v1");
		c.RoutePrefix = "swagger";
	});
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapBarbeariaEndpoints();

app.MapBarbeiroEndpoints();

app.MapServicoEndpoints();

app.MapAgendamentoEndpoints();

app.MapClienteInfoEndpoints();

app.MapAuthEndpoints();

app.Run();
