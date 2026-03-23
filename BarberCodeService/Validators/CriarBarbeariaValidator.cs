using BarberCode.Service.Requests;
using FluentValidation;


namespace BarberCode.Application.Validators;

public class CriarBarbeariaValidator : AbstractValidator<CriarBarbeariaRequest>
{
	public CriarBarbeariaValidator()
	{
		RuleFor(x => x.Name)
	  .NotEmpty().WithMessage("Nome é obrigatório.")
	  .MinimumLength(3).WithMessage("Nome deve ter no mínimo 3 caracteres.")
	  .MaximumLength(100).WithMessage("Nome deve ter no máximo 100 caracteres.");

		RuleFor(x => x.Endereco)
			.NotNull().WithMessage("Endereço é obrigatório.")
			.SetValidator(new CriarEnderecoValidator());

		RuleFor(x => x.Funcionamento)
			.NotEmpty().WithMessage("Horários de funcionamento são obrigatórios.")
			.Must(f => f.Select(x => x.Dia).Distinct().Count() == f.Count)
			.WithMessage("Não pode haver dias duplicados no funcionamento.");

		RuleForEach(x => x.Funcionamento)
			.SetValidator(new CriarHorarioFuncionamentoValidator());

		RuleFor(c => c.Celular)
		.NotEmpty().WithMessage("O telefone é obrigatório.")
		.Must(t => t.All(char.IsDigit)).WithMessage("O telefone deve conter apenas números.")
		.Length(11).WithMessage("O telefone deve ter exatamente 11 dígitos (DDD + Número).")
		// 3. Regex para validar: DDD (11-99) + Nono Dígito (9)
		.Matches(@"^([1-9]{2})9[1-9][0-9]{7}$")
		.WithMessage("O DDD deve ser válido (11-99) e o número deve ser um celular (começar com 9).");
	}
}



