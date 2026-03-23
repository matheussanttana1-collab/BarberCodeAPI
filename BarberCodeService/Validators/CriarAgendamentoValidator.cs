using BarberCode.Service.Requests;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberCode.Application.Validators;
public class CriarAgendamentoValidator : AbstractValidator<CriarAgendamentoRequest>
{
	public CriarAgendamentoValidator()
	{
		RuleFor(a => a.BarbeiroId)
			.NotEmpty().WithMessage("O Barbeiro deve ser selecionado.");

		RuleFor(a => a.BarbeariaId)
			.NotEmpty().WithMessage("A Barbearia deve ser identificada.");

		RuleFor(a => a.ServicoId)
			.NotEmpty().WithMessage("O Serviço deve ser selecionado.");

		RuleFor(a => a.Dia)
			.NotEmpty().WithMessage("A data do agendamento é obrigatória.")
			.Must(BeAFutureDate).WithMessage("A data do agendamento não pode ser no passado.");

		
		RuleFor(a => a.Horario)
			.NotEmpty().WithMessage("O horário do agendamento é obrigatório.");

	
		RuleFor(a => a.Cliente)
			.NotNull().WithMessage("As informações do cliente são obrigatórias.")
			.SetValidator(new CriarClienteInfoValidator()!);	
	}

	private bool BeAFutureDate(DateOnly dia)
	{
		return dia >= DateOnly.FromDateTime(DateTime.Now);
	}
}
