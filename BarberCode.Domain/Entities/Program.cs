using BarberCode.Domain.Entities;
using BarberCode.Domain.Entities.Agendamentos;
using BarberCode.Domain.Entities.Barbearias;
using BarberCode.Domain.Entities.Barbeiro;
using BarberCode.Domain.Entities.Barbeiros;
using System.Security.Cryptography;

// ===== CRIAR BARBEARIA COM HORÁRIOS E BARBEIRO =====

// Horários de funcionamento (segunda a sábado, 8h às 18h)
var horariosFuncionamento = new List<HorarioFuncionamento>()
{
	new HorarioFuncionamento { dia = DayOfWeek.Monday, Incio = new TimeOnly(8, 0), Fim = new TimeOnly(18, 0) },
	new HorarioFuncionamento { dia = DayOfWeek.Tuesday, Incio = new TimeOnly(8, 0), Fim = new TimeOnly(18, 0) },
	new HorarioFuncionamento { dia = DayOfWeek.Wednesday, Incio = new TimeOnly(8, 0), Fim = new TimeOnly(18, 0) },
	new HorarioFuncionamento { dia = DayOfWeek.Thursday, Incio = new TimeOnly(8, 0), Fim = new TimeOnly(18, 0) },
	new HorarioFuncionamento { dia = DayOfWeek.Friday, Incio = new TimeOnly(8, 0), Fim = new TimeOnly(18, 0) },
	new HorarioFuncionamento { dia = DayOfWeek.Saturday, Incio = new TimeOnly(8, 0), Fim = new TimeOnly(16, 0) }
};

// Criar nova Barbearia
var barbearia = new Barbearia()
{
	Name = "Barbearia Premium",
	Funcionamento = horariosFuncionamento,
};

// Criar um Barbeiro funcionário da barbearia
var barbeiro = new Barbeiro("João Silva", null, barbearia.Id)
{

};

// Adicionar barbeiro à barbearia
barbearia.Barbeiros.Add(barbeiro);

//Console.WriteLine("📅 Horários de Funcionamento:");
//foreach (var horario in horariosFuncionamento)
//{
//	Console.WriteLine($"   {horario.dia}: {horario.Incio} às {horario.Fim}");
//}

// ===== AGENDA DE AGENDAMENTOS =====

// Criar nova Agenda
// Criar um novo Serviço com duração de 45 minutos
var servico = new Servico(new Guid(),"Corte de Cabelo", 30, "Corte padrão")
{
};

var cliente = new ClienteInfo { Name = "Matheus" };

barbeiro.AdicionarServicos(servico);
barbeiro.Agenda.EditarHoraAlmoço(new TimeOnly(12, 00), 60);
barbeiro.NovoAgendamento(cliente,DateOnly.FromDateTime(DateTime.Today.AddDays(1)),new TimeOnly(8, 30), servico.Id)
;
var horarios = barbeiro.GerarSlotsDiponiveis(barbearia,DateOnly.FromDateTime(DateTime.Today.AddDays(1)), servico.Id);

if (horarios.Count == 0)
{
	Console.WriteLine("merda");
}
foreach (var hora in horarios)
{
	Console.Write($"|{hora}| ");
}






