using BarberCode.Service.Requests;
using FluentValidation;


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
			.Must(DiaNoFuturo).WithMessage("A data do agendamento não pode ser no passado.");

		RuleFor(a => a.Horario)
			.NotEmpty().WithMessage("O horário do agendamento é obrigatório.")
			.Must(HoraNoFuturo).WithMessage("A hora do agendamento não pode ser no passado."); ;
	
		RuleFor(a => a.Cliente)
			.NotNull().WithMessage("As informações do cliente são obrigatórias.")
			.SetValidator(new CriarClienteInfoValidator()!);	
	}

	private bool DiaNoFuturo(DateOnly dia)
	{
		return dia >= DateOnly.FromDateTime(DateTime.Now);
	}
	private bool HoraNoFuturo(TimeOnly hora)
	{
		return hora >= TimeOnly.FromDateTime(DateTime.Now);
	}
}
