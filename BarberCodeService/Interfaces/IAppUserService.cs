using BarberCode.Application.Models;
using BarberCode.Domain.Shared;

namespace BarberCode.Application.Interfaces;

/// <summary>
/// Interface para operações de usuário da aplicação
/// Responsável apenas por CONSULTAS e OPERAÇÕES no banco de dados
/// Validações devem ser feitas nos UseCases
/// </summary>
public interface IAppUserService
{
	/// <summary>
	/// Cadastra um novo usuário no banco de dados
	/// </summary>
	Task<ResultData> CadastrarUsuarioAsync(Guid userId, string email, string senha, TipoUsuario tipo);

	/// <summary>
	/// Cadastra um novo cliente usando apenas celular
	/// Gera email fake baseado no celular: "{celular}@cliente.barbercode.local"
	/// </summary>
	Task<ResultData> CadastrarClienteAsync(Guid clienteId, string celular, string nome, string? senha = null);

	/// <summary>
	/// Valida se o email existe e retorna o usuário autenticado
	/// </summary>
	Task<AuthUser?> BuscarPeloEmailAsync(string email);

	/// <summary>
	/// Valida a senha do usuário
	/// Retorna o usuário se a senha for válida, null caso contrário
	/// </summary>
	Task<AuthUser?> ValidarUsuarioAsync(string email, string senha);

	/// <summary>
	/// Obtém todas as roles (papéis) do usuário
	/// </summary>
	Task<IList<string>> ObterRolesAsync(AuthUser user);

	/// <summary>
	/// Altera a senha do usuário
	/// As validações devem ser feitas no UseCase
	/// </summary>
	Task<ResultData> AlterarSenhaAsync(AuthUser user, string token, string novaSenha);

	/// <summary>
	/// Redefine a senha para um padrão temporário
	/// As validações devem ser feitas no UseCase
	/// </summary>
	Task<string> GerarTokenDeResetSenhaAsync(AuthUser user);
}