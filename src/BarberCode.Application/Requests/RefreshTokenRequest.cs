namespace BarberCode.Service.Requests;

/// <summary>
/// Request para refresh de token
/// </summary>
public record RefreshTokenRequest(string Token)
{
}
