using BarberCode.Service.Requests;
using FluentValidation;

namespace BarberCode.Application.Validators;

public class CriarServicoValidator : AbstractValidator<CriarServicoRequest>
{
	public CriarServicoValidator()
	{
		RuleFor(s => s.Name)
			.NotEmpty().WithMessage("{PropertyName} é obrigatório.")
			.MaximumLength(100).WithMessage("{PropertyName} deve ter no máximo 100 caracteres.");

		RuleFor(s => s.Descricao)
			.NotEmpty().WithMessage("{PropertyName} é obrigatória.")
			.Length(15, 150).WithMessage("{PropertyName} deve ter entre 15 e 150 caracteres.");

		RuleFor(s => s.Duracao)
			.NotEmpty().WithMessage("Duração do serviço e obrigatorio")
			.GreaterThan(0).WithMessage("{PropertyName} deve ser maior que 0 minutos.");

		RuleFor(s => s.Preco)
			.NotEmpty().WithMessage("Preço do serviço e obrigatorio")
			.GreaterThan(0).WithMessage("{PropertyName} deve ser um valor positivo.");
	}
}

