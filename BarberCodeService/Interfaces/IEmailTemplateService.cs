namespace BarberCode.Application.Interfaces;

public interface IEmailTemplateService
{
	string gerarTemplateResetSenha(string email, string token);
    string gerarTemplateBoasVindasBarbearia(string nomeBarbearia);
	string gerarTemplateBoasVindasBarbeiro(string nomeBarbeiro, string nomeBarbearia);
}
