using FluentValidation;
using BarberCode.Service.Requests;

namespace BarberCode.Application.Validators;

public class LoginClienteValidator : AbstractValidator<LoginClienteRequest>
{
	public LoginClienteValidator()
	{
		RuleFor(x => x.Celular)
			.NotEmpty().WithMessage("Celular é obrigatório")
			.Length(10, 15).WithMessage("Celular deve ter entre 10 e 15 caracteres");
	}
}
