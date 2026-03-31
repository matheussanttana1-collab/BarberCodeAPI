using BarberCode.Application.Models;
using BarberCode.Domain.Shared;

namespace BarberCode.Application.Interfaces;

public interface IAppUserService
{
	/// <summary>
	/// Cadastra um novo usuário no banco de dados
	/// </summary>
	Task<ResultData> CadastrarUsuarioAsync(Guid userId,string email, string senha
	, TipoUsuario tipo);

	/// <summary>
	/// Valida se o email existe e retorna o usuário autenticado
	/// </summary>
	Task<AuthUser?> ValidarEmailAsync(string email);

	/// <summary>
	/// Valida a senha do usuário
	/// Retorna o usuário se a senha for válida, null caso contrário
	/// </summary>
	Task<AuthUser?> ValidarSenhaAsync(AuthUser user, string senha);

	/// <summary>
	/// Obtém todas as roles (papéis) do usuário
	/// </summary>
	Task<IList<string>> ObterRolesAsync(AuthUser user);

	//Task AdicionarRole();
}
