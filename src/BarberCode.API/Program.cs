using BarberCode.API.Endpoins;
using BarberCode.API.Models;
using BarberCode.Application;
using BarberCode.Application.Validators;
using BarberCode.Domain.Shared;
using BarberCode.Infra;
using BarberCode.Infra.Banco;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
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
	// ======================== CONFIGURAÇÃO DE SEGURANÇA JWT NO SWAGGER ========================

	// Define o esquema de segurança Bearer/JWT no Swagger
	// OpenApiSecurityScheme: Define o tipo de autenticação que a API utiliza
	// Neste caso, configuramos o Bearer Token (JWT) para validar requisições autenticadas
	c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
	{
		// Descrição exibida no Swagger UI explicando ao usuário como usar o token
		Description = "Insira o token JWT desta maneira: Bearer {seu_token}",

		// Nome do header que receberá o token (Authorization)
		Name = "Authorization",

		// Tipo de esquema utilizado (Bearer Token)
		Scheme = "Bearer",

		// Formato específico do token (JWT - Json Web Token)
		BearerFormat = "JWT",

		// Localização do token na requisição (Header)
		In = ParameterLocation.Header,

		// Tipo de segurança (ApiKey significa que é um token/chave na requisição)
		Type = SecuritySchemeType.ApiKey
	});

	// ======================== APLICAR REQUISITO DE SEGURANÇA AOS ENDPOINTS ========================

	// AddSecurityRequirement: Informa que todos os endpoints (exceto os públicos) requerem autenticação
	// Isso garante que o Swagger UI mostre um botão de "Authorize" para o usuário inserir o token JWT
	c.OperationFilter<SwaggerFilters>();
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
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
	app.MapOpenApi();

	// Configurar Swagger UI
	app.UseSwagger();
	app.UseSwaggerUI(c =>
	{
		c.SwaggerEndpoint("/swagger/v1/swagger.json", "BarberCode API v1");
		c.RoutePrefix = "swagger";
		// Salva o token no cache do navegador para não perder no F5!
		c.EnablePersistAuthorization();
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
