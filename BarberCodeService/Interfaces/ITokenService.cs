using BarberCode.Application.Models;

namespace BarberCode.Application.Interfaces;

/// <summary>
/// Interface para geração de tokens JWT
/// </summary>
public interface ITokenService
{
	/// <summary>
	/// Gera um token JWT para o usuário autenticado
	/// </summary>
	/// <param name="user">Usuário autenticado</param>
	/// <param name="roles">Lista de roles do usuário</param>
	/// <returns>Token JWT em formato string</returns>
	string GerarToken(AuthUser user, IList<string> roles);
}
