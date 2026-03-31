namespace BarberCode.Application.Models;

/// <summary>
/// Modelo DTO para representar um usuário autenticado
/// Não expõe a entidade AppUser da Infrastructure Layer
/// </summary>
public class AuthUser
{
	/// <summary>
	/// ID único do usuário
	/// </summary>
	public string Id { get; set; }

	/// <summary>
	/// Email do usuário
	/// </summary>
	public string Email { get; set; }

	/// <summary>
	/// Nome de usuário
	/// </summary>
	public string UserName { get; set; }

	/// <summary>
	/// 
	/// </summary>
	TipoUsuario TipoUsuario { get; set; }
	public AuthUser() { }

	public AuthUser(string id, string email, string userName, TipoUsuario tipo)
	{
		Id = id;
		Email = email;
		UserName = userName;
		TipoUsuario = tipo;
	}
}
