namespace BarberCode.Domain.Entities.Barbeiro;

using BarberCode.Domain.Entities.Agendamentos;
using BarberCode.Domain.Entities.Barbearias;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;

public class Agenda
{
	public ICollection<Agendamento> Agendamentos { get;} = new List<Agendamento>();
	public List<TimeOnly> HorarioAlmoço { get;} = new List<TimeOnly>();
	public DateOnly DiaBloqueado { get; private set; } = new DateOnly();
	


	//Adicionar Horario de funciionamento Da Barbearia para que nao seja Possivel Marcar o Almoço Fora do Horario
	public void EditarHoraAlmoço(TimeOnly horarioInicio, int duracao)
	{
		HorarioAlmoço.Add(horarioInicio);
		HorarioAlmoço.Add(horarioInicio.AddMinutes(duracao));
	}

	public void BloquearDia(DateOnly dia)
	{
		DiaBloqueado = dia;
	}

	private bool EstaDisponivel(DateOnly dia, TimeOnly horario, int duracaoMinutos)
	{
		var fim = horario.AddMinutes(duracaoMinutos);
		if(fim > HorarioAlmoço[0] && horario < HorarioAlmoço[1])
			return false;

		if(Agendamentos.Any(a => a.Dia == dia &&
			horario < a.Horario.AddMinutes(a.Servico.Duracao) && fim > a.Horario))
			return false;

		return true;
		
	}

	// TODO: Refatorar - Agenda não deve receber Barbearia diretamente.
	// Mover orquestração para Application Layer e passar apenas HorarioFuncionamento (VO)
	public List<TimeOnly> HorariosDisponiveis (Barbearia barbearia, DateOnly diaEscolhido, int duracao)
	{
		List<TimeOnly> Slots = new List<TimeOnly> ();

		var funcionamento = barbearia.Funcionamento.FirstOrDefault(f => f.dia.Equals(diaEscolhido.DayOfWeek));
		if (funcionamento is null) 
		{
			return Slots;
		}

		var inicio = funcionamento.Incio;
		var fim = funcionamento.Fim;
	
		while(inicio < fim)
		{
			if (EstaDisponivel(diaEscolhido,inicio, duracao))
			{
				Slots.Add(inicio);
			}

			inicio = inicio.AddMinutes(30);
		}

		return Slots;
	}

	public void NovoAgendamento (Agendamento agendamento)
	{

		if (!EstaDisponivel(agendamento.Dia, agendamento.Horario, agendamento.Duracao))
		{
			throw new Exception("Horario Indisponivel");
		}
		Agendamentos.Add(agendamento);	
	}

}