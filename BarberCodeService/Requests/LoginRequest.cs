namespace BarberCode.Service.Requests;

/// <summary>
/// Request para autenticação de Barbearia
/// </summary>
public class LoginRequest
{
	/// <summary>
	/// Email do usuário
	/// </summary>
	public string Email { get; set; }

	/// <summary>
	/// Senha do usuário
	/// </summary>
	public string Senha { get; set; }

	public LoginRequest() { }

	public LoginRequest(string email, string senha)
	{
		Email = email;
		Senha = senha;
	}
}
