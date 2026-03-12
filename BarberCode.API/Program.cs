using BarberCode.API.Endpoins;
using BarberCode.Application.Interfaces;
using BarberCode.Application.Profiles;
using BarberCode.Infra.Banco;
using BarberCode.Infra.Repositories;
using BarberCodeService.UseCases;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("BarberCodeConnection");
builder.Services.AddDbContext<BarberCodeContext>(opts => opts.UseLazyLoadingProxies().UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
builder.Services.AddAutoMapper(cfg => { }, typeof(BarbeariaProfile));

builder.Services.AddScoped<CriarBarbeariaUseCase>();
builder.Services.AddScoped<IBarbeariaRepository, BarbeariaRepository>();

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

app.UseAuthorization();

app.MapControllers();

app.MapBarbeariaEndpoints();

app.MapBarbeiroEndpoints();

app.Run();
