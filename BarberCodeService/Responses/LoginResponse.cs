namespace BarberCode.Service.Responses;

/// <summary>
/// Response para autenticação de Barbearia
/// Retorna o token JWT gerado
/// </summary>
public record LoginResponse(
	string Token,
	string Email,
	string UserName
);
