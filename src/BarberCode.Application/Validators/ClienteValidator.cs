using BarberCode.Service.Requests;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberCode.Application.Validators;

public class ClienteInfoValidator : AbstractValidator<CriarClienteInfoRequest>
{
	public ClienteInfoValidator()
	{
		RuleFor(c => c.Nome)
		.MaximumLength(100).WithMessage("{PropertyName} deve ter no máximo 100 caracteres.")
		.When(c => c.Nome is not null);

		RuleFor(c => c.Celular)
		.NotEmpty().WithMessage("O telefone é obrigatório.")
		.Must(t => t.All(char.IsDigit)).WithMessage("O telefone deve conter apenas números.")
		.Length(11).WithMessage("O telefone deve ter exatamente 11 dígitos (DDD + Número).")
		// 3. Regex para validar: DDD (11-99) + Nono Dígito (9)
		.Matches(@"^([1-9]{2})9[1-9][0-9]{7}$")
		.WithMessage("O DDD deve ser válido (11-99) e o número deve ser um celular (começar com 9).");
	}
}
