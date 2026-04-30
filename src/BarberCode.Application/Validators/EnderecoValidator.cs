using BarberCode.Service.Requests;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberCode.Application.Validators;

public class EnderecoValidator : AbstractValidator<EnderecoRequest>
{
	public EnderecoValidator()
	{
		RuleFor(x => x.Logradouro)
			.NotEmpty().WithMessage("Logradouro é obrigatório.")
			.MaximumLength(200).WithMessage("Logradouro deve ter no máximo 200 caracteres.");

		RuleFor(x => x.Bairro)
			.NotEmpty().WithMessage("Nome do endereço é obrigatório.")
			.MaximumLength(100).WithMessage("Nome deve ter no máximo 100 caracteres.");

		RuleFor(x => x.Numero)
			.NotEmpty().WithMessage("Número é obrigatório.");

		RuleFor(x => x.Cidade)
			.NotEmpty().WithMessage("Cidade é obrigatória.")
			.MaximumLength(100).WithMessage("Cidade deve ter no máximo 100 caracteres.");

		RuleFor(x => x.Estado)
			.NotEmpty().WithMessage("Estado é obrigatório.")
			.Length(2).WithMessage("Estado deve ter 2 caracteres.");

		RuleFor(x => x.CEP)
		.NotEmpty().WithMessage("CEP é obrigatório.")
		.Matches(@"^\d{5}-?\d{3}$").WithMessage("CEP inválido.");
	}
}
