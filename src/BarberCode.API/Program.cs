using BarberCode.API.Endpoins;
using BarberCode.Application;
using BarberCode.Application.Validators;
using BarberCode.Domain.Shared;
using BarberCode.Infra;
using BarberCode.Infra.Banco;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient();

// ---------------------------- DI Camadas de Infra / Application ----------------------------------
builder.Services.AddApplication();
builder.Services.addInfra(builder.Configuration);
builder.Services.AddValidatorsFromAssemblyContaining<CriarBarbeariaValidator>();

//---------------------------------- Authorization e Jwt Config --------------------------------------------------
builder.Services.AddAuthentication
(opitions => {

	opitions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	opitions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

})
.AddJwtBearer(opts => {
	var secretKey = builder.Configuration["SymmetricSecurityKey"] 
		?? throw new InvalidOperationException("JWT SecretKey não foi configurada. Defina a variável de ambiente 'SymmetricSecurityKey'.");

	opts.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
	{
		ValidateIssuerSigningKey = true,
		IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
		ValidateAudience = false,
		ValidateIssuer = false,
		ClockSkew = TimeSpan.Zero
	};

	// Configurar resposta customizada para Unauthorized (401)
	opts.Events = new JwtBearerEvents
	{
		OnChallenge = context =>
		{
			context.HandleResponse();

			context.Response.StatusCode = StatusCodes.Status401Unauthorized;
			context.Response.ContentType = "application/json";

			// Monta o seu padrão
			var result = ResultData.Failure(ResultType.Unauthorized, "Você precisa estar logado para " +
			"acessar este recurso.");

			// Converte para JSON e escreve na resposta
			var json = JsonSerializer.Serialize(result, new JsonSerializerOptions
			{ PropertyNamingPolicy = JsonNamingPolicy.CamelCase ,
			  Converters = { new JsonStringEnumConverter() }
			});
			return context.Response.WriteAsync(json);
		},

		// 🆕 Configurar resposta customizada para Forbidden (403)
		OnForbidden = context =>
		{
			context.Response.StatusCode = StatusCodes.Status403Forbidden;
			context.Response.ContentType = "application/json";

			var result = ResultData.Failure(ResultType.Forbidden, "Você não tem permissão para realizar" +
			" esta ação.");

			var json = JsonSerializer.Serialize(result, new JsonSerializerOptions 
			{ PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
			 Converters = { new JsonStringEnumConverter() }
			});
			return context.Response.WriteAsync(json);
		}
	};
});

// ------------------------------------ Authorization e Roles ----------------------------------------------------
builder.Services.AddAuthorization(opts =>
{
	opts.AddPolicy("manager", policy => policy.RequireRole("Barbearia"));
	opts.AddPolicy("employee", policy => policy.RequireRole("Barbeiro"));
	opts.AddPolicy("user", policy => policy.RequireRole("Cliente"));
	opts.AddPolicy("managerOrEmployee", policy =>
		policy.RequireRole("Barbearia", "Barbeiro"));
});


// ------------------------------- Config (Tranformar enum's em Strings) --------------------------------------------
builder.Services.ConfigureHttpJsonOptions(options =>
{
	// Isso faz com que TODOS os enums da API virem strings no JSON
	options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddControllers();

// -------------------------------  Configurar OpenAPI e Swagger -------------------------------------------------------
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



/// --------------------------- App builder's / Swagger / Endponts / etc ---------------------------------------

var app = builder.Build();

// Aplicar migrations e criar banco de dados automaticamente
using (var scope = app.Services.CreateScope())
{
	var dbContext = scope.ServiceProvider.GetRequiredService<BarberCodeContext>();
	dbContext.Database.Migrate();
}
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

app.MapWhatsAppEndpoints();

app.MapBarbeiroEndpoints();

app.MapServicoEndpoints();

app.MapAgendamentoEndpoints();

app.MapClienteInfoEndpoints();

app.MapAuthEndpoints();

app.Run();
