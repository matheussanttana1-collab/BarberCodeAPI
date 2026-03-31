using BarberCode.Service.Requests;
using FluentValidation;

namespace BarberCode.Application.Validators;

public class LoginValidator : AbstractValidator<LoginRequest>
{
	public LoginValidator()
	{
		RuleFor(x => x.Email)
			.NotEmpty()
				.WithMessage("Email é obrigatório.")
			.EmailAddress()
				.WithMessage("Email deve ser um endereço válido.")
			.MaximumLength(254)
				.WithMessage("Email deve ter no máximo 254 caracteres.");

		RuleFor(x => x.Senha)
			.NotEmpty()
				.WithMessage("Senha é obrigatória.")
			.MinimumLength(6)
				.WithMessage("Senha deve ter no mínimo 6 caracteres.")
			.MaximumLength(100)
				.WithMessage("Senha deve ter no máximo 100 caracteres.");
	}
}
