using BarberCode.Domain.Shared;
using FluentValidation;

namespace BarberCode.API.Models;


/// <summary>
/// Filtro genérico de validação para Minimal APIs.
/// Intercepta a requisição antes de chegar no endpoint e valida o request usando FluentValidation.
/// T é o tipo do request que será validado (ex: CriarBarbeariaRequest)
/// </summary>
public class ValidationFilter<T> : IEndpointFilter where T : class
{
	// Validator específico para o tipo T, injetado via DI
	// Ex: se T = CriarBarbeariaRequest, injeta CriarBarbeariaRequestValidator
	private readonly IValidator<T> _validator;

	public ValidationFilter(IValidator<T> validator)
	{
		_validator = validator;
	}

	/// <summary>
	/// Método principal do filtro — executado em toda requisição antes do endpoint.
	/// 
	/// context — contém todas as informações da requisição atual:
	///     context.Arguments    — lista de todos os parâmetros do endpoint (request, services, etc)
	///     context.HttpContext  — acesso ao HttpContext completo (headers, claims, cancellation token, etc)
	/// 
	/// next — delegate que representa o próximo passo na pipeline.
	///     Chamar await next(context) significa "pode continuar, deixa passar para o endpoint"
	///     Não chamar next significa "barra aqui, não chega no endpoint"
	/// 
	/// Retorna object? pois pode retornar:
	///     - IResult (BadRequest, Ok, etc) quando barra a requisição
	///     - O retorno do endpoint quando deixa passar
	/// </summary>
	public async ValueTask<object?> InvokeAsync(
		EndpointFilterInvocationContext context, // contexto completo da requisição
		EndpointFilterDelegate next)             // delegate para o próximo passo da pipeline
	{
		// Busca nos argumentos do endpoint o parâmetro que é do tipo T
		// OfType<T> filtra apenas o argumento que bate com o tipo esperado
		// Ex: endpoint tem (Guid id, CriarBarbeariaRequest request) — pega só o request
		var request = context.Arguments.OfType<T>().FirstOrDefault();

		// Só valida se encontrou o argumento do tipo T
		// Evita NullReferenceException caso o argumento não exista
		if (request is not null)
		{
			// Executa todas as regras do validator de forma assíncrona
			// RequestAborted — CancellationToken que cancela a validação se o cliente desconectar
			// result contém:
			//     result.IsValid  — true se passou em todas as regras
			//     result.Errors   — lista de erros se alguma regra falhou
			var result = await _validator.ValidateAsync(request, context.HttpContext.RequestAborted);

			// Se alguma regra falhou, barra a requisição aqui
			// Não chama next() — o endpoint nunca é executado
			// ToDictionary() agrupa os erros por campo:
			// { "Nome": ["Nome é obrigatório", "Nome muito curto"], "Cep": ["CEP inválido"] }
			if (!result.IsValid)
			{
				// Retorna 400 com os erros de validação agrupados por campo
				// ResultData<T>.Invalid — seu wrapper do Result Pattern com os erros
				return Results.ValidationProblem(result.ToDictionary());
			}
		}

		// Validação passou — continua para o próximo passo da pipeline
		// Se não houver mais filtros, executa o endpoint
		return await next(context);
	}
}