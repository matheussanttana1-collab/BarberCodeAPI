using BarberCode.Application.Requests;
using FluentValidation;

namespace BarberCode.Application.Validators;

public class AtualizarServicoValidator : AbstractValidator<AtualizarServicoRequest>
{
	public AtualizarServicoValidator()
	{
		RuleFor(s => s.name)
			.MaximumLength(100)
				.WithMessage("{PropertyName} deve ter no máximo 100 caracteres.")
			.When(x => !string.IsNullOrEmpty(x.name));

		RuleFor(s => s.descricao)
			.Length(15, 150)
				.WithMessage("{PropertyName} deve ter entre 15 e 150 caracteres.")
			.When(x => !string.IsNullOrEmpty(x.descricao));

		RuleFor(s => s.duracao)
			.GreaterThan(0)
				.WithMessage("{PropertyName} deve ser maior que 0 minutos.")
			.When(x => x.duracao.HasValue);

		RuleFor(s => s.preco)
			.GreaterThan(0)
				.WithMessage("{PropertyName} deve ser um valor positivo.")
			.When(x => x.preco.HasValue);
	}
}
