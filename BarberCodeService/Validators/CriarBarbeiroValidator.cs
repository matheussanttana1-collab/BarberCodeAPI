using BarberCode.Service.Requests;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberCode.Application.Validators;

public class CriarBarbeiroValidator : AbstractValidator<CriarBarbeiroRequest>
{
	public CriarBarbeiroValidator()
	{
		RuleFor(x => x.Nome)
			.NotEmpty()
				.WithMessage("Nome é obrigatório.")
			.MinimumLength(3)
				.WithMessage("Nome deve ter no mínimo 3 caracteres.")
			.MaximumLength(100)
				.WithMessage("Nome deve ter no máximo 100 caracteres.")
			.Matches(@"^[a-zA-ZÀ-ÿ\s]+$")
				.WithMessage("Nome deve conter apenas letras.");

		RuleFor(x => x.HorarioAlmoco)
			.NotEmpty()
				.WithMessage("Horário de almoço é obrigatório.");
	}
}
