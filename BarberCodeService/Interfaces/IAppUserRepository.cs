using BarberCode.Domain.Shared;

namespace BarberCode.Application.Interfaces;

public interface IAppUserRepository
{
	/// <summary>
	/// Cadastra um novo usuário no banco de dados
	/// </summary>
	/// <param name="userId">ID do usuário</param>
	/// <param name="email">Email do usuário</param>
	/// <param name="userName">Nome de usuário</param>
	/// <returns>Task representando a operação assíncrona</returns>
	Task<ResultData> CadastrarUsuarioAsync(string userId, string email, string userName, string senha);

	/// <summary>
	/// Busca um usuário pelo email
	/// </summary>
	/// <param name="email">Email do usuário</param>
	/// <returns>ID do usuário ou null se não encontrado</returns>
	Task<ResultData<string>> Login(string email, string senha);

	/// <summary>
	/// Busca um usuário pelo ID
	/// </summary>
	/// <param name="id">ID do usuário</param>
	/// <returns>ID do usuário ou null se não encontrado</returns>
}
