using BarberCode.Application.Interfaces;
using BarberCode.Infra.Models;
using Microsoft.Extensions.Options;

namespace BarberCode.Infra.Service;

public class EmailTemplateService : IEmailTemplateService
{
	private readonly EmailSettings _emailSettings;

	public EmailTemplateService(IOptions<EmailSettings> emailOptions)
	{
		_emailSettings = emailOptions.Value;
	}

	public string gerarLinkResetSenha(string email, string token)
	{
		var tokenCodificado = Uri.EscapeDataString(token);
		var emailCodificado = Uri.EscapeDataString(email);
		return $"{_emailSettings.BaseUrl.TrimEnd('/')}/{tokenCodificado}?email={emailCodificado}";
	}

	public string gerarTemplateResetSenha(string email, string token)
	{
		var linkReset = gerarLinkResetSenha(email, token);
       return $@"
		<div style='font-family: Arial, sans-serif; max-width: 520px; margin: 0 auto; color: #1f2937; line-height: 1.6;'>
			<h2 style='margin-bottom: 12px; color: #111827;'>Recuperação de senha</h2>
			<p style='margin-bottom: 16px;'>Olá,</p>
			<p style='margin-bottom: 24px;'>Recebemos uma solicitação para redefinir sua senha. Clique no botão abaixo para continuar:</p>
			<p style='margin-bottom: 24px;'>
				<a href='{linkReset}' style='display: inline-block; background-color: #111827; color: #ffffff; text-decoration: none; padding: 10px 18px; border-radius: 6px; font-weight: 600;'>
					Redefinir senha
				</a>
			</p>
			<p style='font-size: 14px; color: #4b5563; margin-bottom: 8px;'>Se você não solicitou essa ação, ignore este e-mail.</p>
			<p style='font-size: 13px; color: #6b7280;'>BarberCode</p>
		</div>";
	}
}
