namespace BarberCode.Service.Requests;

/// <summary>
/// Request para autenticação de Barbearia
/// </summary>
public record LoginRequest(string Email, string Senha)
{
}
