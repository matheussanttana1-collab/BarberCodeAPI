using BarberCode.Domain.Entities.Agendamentos;
using BarberCode.Domain.Entities.Barbearias;


namespace BarberCode.Domain.Entities.Barbeiros;

public class Barbeiro
{
	public Barbeiro(string name, string? fotoPerfil, Guid barbeariaId, List<TimeOnly> horarioAlmoco)
	{
		Id = Guid.NewGuid();
		Name = name;
		FotoPerfil = fotoPerfil;
		BarbeariaId = barbeariaId;
		HorarioAlmoco = horarioAlmoco;
	}

	protected Barbeiro()
	{

	}

	public Guid Id { get; private set; }
	public string Name { get; private set; }
	public string? FotoPerfil { get; private set; }
	public Guid BarbeariaId { get; private set; }
	public virtual Barbearia BarbeariaQueTrabalha { get; private set; }
	public virtual ICollection<Agendamento> Agendamentos { get; private set; } = new List<Agendamento>();
	public List<TimeOnly> HorarioAlmoco { get; private set; } = new List<TimeOnly>();

	//--------------------------------- Metodos De Verificação de Horarios ------------------------------
	private bool EstaDisponivel(DateOnly dia, TimeOnly horario, int duracaoMinutos)
	{
		var fim = horario.AddMinutes(duracaoMinutos);
		if (fim > HorarioAlmoco[0] && horario < HorarioAlmoco[1])
			return false;

		if (Agendamentos.Any(a => a.Dia == dia &&
			horario < a.Horario.AddMinutes(a.Servico.Duracao) && fim > a.Horario))
			return false;

		return true;
	}

	public List<TimeOnly> HorariosDisponiveis(TimeOnly inicioExpediente, TimeOnly fimExpediente, DateOnly diaEscolhido,
			int duracao)
	{
		List<TimeOnly> Slots = new List<TimeOnly>();


		while (inicioExpediente < fimExpediente)
		{
			if (EstaDisponivel(diaEscolhido, inicioExpediente, duracao))
			{
				Slots.Add(inicioExpediente);
			}

			inicioExpediente = inicioExpediente.AddMinutes(30);
		}
		return Slots;
	}
	public void EditarHoraAlmoço(TimeOnly horarioInicio, int duracao)
	{
		HorarioAlmoco.Add(horarioInicio);
		HorarioAlmoco.Add(horarioInicio.AddMinutes(duracao));
	}

	//---------------------------------- Metodos de Agendamento ----------------------------------------
	public void NovoAgendamento(Agendamento agendamento)
	{
		if (!EstaDisponivel(agendamento.Dia, agendamento.Horario, agendamento.Duracao))
		{
			throw new Exception("Horario Indisponivel");
		}
		Agendamentos.Add(agendamento);
	}








}