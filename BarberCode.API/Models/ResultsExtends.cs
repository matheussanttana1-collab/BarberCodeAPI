using BarberCode.Domain.Shared;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BarberCode.API.Models;

public static class ResultsExtends
{
	/// <summary>
	/// Extension method para converter um ResultData em IResult HTTP para operações de criação (POST).
	/// Usado nos endpoints para transformar o retorno do UseCase em uma resposta HTTP adequada.
	/// 
	/// this ResultData<T> result — o ResultData retornado pelo UseCase
	/// string path — a URL do recurso criado. Ex: "/api/Barbearias/guid-aqui"
	/// 
	/// Retorna IResult — interface do ASP.NET Core que representa uma resposta HTTP
	/// </summary>
	public static IResult ToCreateResult<T>(this ResultData<T> result, string path)
	{
		// Switch no tipo do resultado para determinar qual resposta HTTP retornar
		return result.Type switch
		{
			// Criação bem sucedida → 201 Created
			// Retorna o path (URL do recurso criado) e o result como body
			// Ex: Location: /api/Barbearias/3fa85f64 + body com os dados
			ResultType.Success => Results.Created(path, result),

			// Falha de validação → 400 Bad Request
			// Body contém os erros de validação agrupados por campo
			ResultType.Validation => Results.BadRequest(new 
			{
				result.Type,
				result.Message,
			}),

			// Conflito de dados → 409 Conflict
			// Ocorre quando o recurso já existe
			// Ex: tentar criar uma barbearia com nome já cadastrado
			ResultType.Conflict => Results.Conflict(new
			{
				result.Type,
				result.Message,
			}),

			ResultType.NotFound => Results.NotFound(new 
			{
				result.Type,
				result.Message,
			}),

			// Qualquer outro tipo não mapeado → 500 Internal Server Error
			_ => Results.StatusCode(500)
		};
	}

	public static IResult ToOkSingleResult<T>(this ResultData<T> result)
	{
		return result.Type switch
		{
			ResultType.Success => Results.Ok(result),
			ResultType.Conflict => Results.Conflict(result),
			ResultType.Validation => Results.BadRequest(result),
			ResultType.NotFound => Results.NotFound(result),
			ResultType.Forbidden => Results.Forbid(),
			_ => Results.StatusCode(500)
		};
	}
	public static IResult ToOkResult<T>(this T data)
	{
		return Results.Ok(ResultData<T>.Success(data)); 
	}

	public static IResult ToNoContentResult(this ResultData result)
	{
		return result.Type switch
		{
			ResultType.Success => Results.NoContent(),
			ResultType.Conflict => Results.Conflict(result),
			ResultType.NotFound => Results.NotFound(result),
			ResultType.Forbidden => Results.Forbid(),
			_ => Results.StatusCode(500)
		};
	}

}
