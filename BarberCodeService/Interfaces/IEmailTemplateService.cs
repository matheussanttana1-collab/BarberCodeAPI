namespace BarberCode.Application.Interfaces;

public interface IEmailTemplateService
{
	string gerarLinkResetSenha(string email, string token);
	string gerarTemplateResetSenha(string email, string token);
}
