using BarberCode.Application.Requests;
using FluentValidation;

namespace BarberCode.Application.Validators;

public class AtualizarBarbeiroValidator : AbstractValidator<AtualizarBarbeiroRequest>
{
	public AtualizarBarbeiroValidator()
	{
		RuleFor(x => x.nome)
			.MinimumLength(3)
				.WithMessage("Nome deve ter no mínimo 3 caracteres.")
			.MaximumLength(100)
				.WithMessage("Nome deve ter no máximo 100 caracteres.")
			.Matches(@"^[a-zA-ZÀ-ÿ\s]+$")
				.WithMessage("Nome deve conter apenas letras.")
			.When(x => !string.IsNullOrEmpty(x.nome));
	}
}
