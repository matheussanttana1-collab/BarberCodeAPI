namespace BarberCode.Service.Requests;

/// <summary>
/// Request para alterar senha
/// </summary>
public record AlterarSenhaRequest(string SenhaNova, string ConfirmarSenha)
{
}
