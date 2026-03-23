using BarberCode.Service.Requests;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberCode.Application.Validators;

public class CriarHorarioFuncionamentoValidator : AbstractValidator<CriarHorarioFuncionamentoRequest>
{
	public CriarHorarioFuncionamentoValidator()
	{
		RuleFor(x => x.Dia)
			.IsInEnum().WithMessage("Dia da semana inválido.");

		RuleFor(x => x.Inicio)
			.NotEmpty().WithMessage("Horário de início é obrigatório.")
			.LessThan(x => x.Fim).WithMessage("Horário de início deve ser menor que o fim.");

		RuleFor(x => x.Fim)
			.NotEmpty().WithMessage("Horário de fim é obrigatório.");
	}
}
