using BarberCode.Service.Requests;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberCode.Application.Validators;

public class ListaHorarioFuncionamentoValidator : AbstractValidator<List<HorarioFuncionamentoRequest>>
{
	public ListaHorarioFuncionamentoValidator()
	{
		RuleFor(x => x)
			.NotEmpty().WithMessage("Horários de funcionamento são obrigatórios.")
			.Must(f => f.Select(x => x.Dia).Distinct().Count() == f.Count)
			.WithMessage("Não pode haver dias duplicados no funcionamento.");
		RuleForEach(h => h).SetValidator(new HorarioFuncionamentoValidator());
	}
}
