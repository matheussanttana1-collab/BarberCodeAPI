using BarberCode.Service.Requests;
using FluentValidation;

namespace BarberCode.Application.Validators;

public class AlterarSenhaValidator : AbstractValidator<AlterarSenhaRequest>
{
	public AlterarSenhaValidator()
	{
		RuleFor(x => x.SenhaNova)
			.NotEmpty()
				.WithMessage("Senha nova é obrigatória.")
			.MinimumLength(6)
				.WithMessage("Senha deve ter no mínimo 6 caracteres.")
			.MaximumLength(100)
				.WithMessage("Senha deve ter no máximo 100 caracteres.");

		RuleFor(x => x.ConfirmarSenha)
			.NotEmpty()
				.WithMessage("Confirmação de senha é obrigatória.")
			.Equal(x => x.SenhaNova)
				.WithMessage("A confirmação de senha deve ser igual à nova senha.");
	}
}
