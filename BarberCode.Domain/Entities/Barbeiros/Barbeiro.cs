using BarberCode.Domain.Entities.Agendamentos;
using BarberCode.Domain.Entities.Barbearias;
using BarberCode.Domain.Shared;


namespace BarberCode.Domain.Entities.Barbeiros;

public class Barbeiro
{
	public Barbeiro(string name, string? fotoPerfil, Guid barbeariaId,TimeOnly horarioAlmoco)
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
	public virtual ICollection<Agendamento> Agendamentos { get; protected set; } = new List<Agendamento>();
	public TimeOnly HorarioAlmoco { get; private set; }

	//--------------------------------- Metodos De Verificação de Horarios ------------------------------
	private bool EstaDisponivel(DateOnly dia, TimeOnly horario, int duracaoMinutos)
	{
		//Junta Dia e Horario para não gerar
		var DiaHorarioEscolhido = dia.ToDateTime(horario);
		if (DiaHorarioEscolhido <= DateTime.Now)
			return false;
		var fim = horario.AddMinutes(duracaoMinutos);
		if (fim > HorarioAlmoco && horario < HorarioAlmoco.AddMinutes(60))
			return false;
		if (Agendamentos.Any(a => a.Dia == dia &&
			horario < a.Horario.AddMinutes(a.Duracao) && fim > a.Horario))
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
	
	public void AlterarBarbeiro (string? nome, TimeOnly? horarioAlmoco)
	{
		if (nome is not null)
			Name = nome;
		if(horarioAlmoco is not null)
			HorarioAlmoco = horarioAlmoco.Value;
	}

	public void AlterarFoto (string caminho)
	{
		FotoPerfil = caminho;
	}

	//---------------------------------- Metodos de Agendamento ----------------------------------------
	public ResultData<Agendamento> NovoAgendamento(Guid clienteId, DateOnly dia, TimeOnly horario, int duracao, Guid servicoId)
	{
		//Junta Dia e Horario para não gerar
		var DiaHorarioEscolhido = dia.ToDateTime(horario);
		if (DiaHorarioEscolhido <  DateTime.Now)
			return ResultData<Agendamento>.Failure(ResultType.Validation, "Agendamento Não pode ser feito" +
			" no passado");
		if (!EstaDisponivel(dia,horario,duracao))
		{
			return ResultData<Agendamento>.Failure(ResultType.Conflict, "Horario Indisponivel");
		}

		var agendamento = new Agendamento(this.Id, this.BarbeariaId, clienteId, dia, horario, duracao, servicoId);
		Agendamentos.Add(agendamento);

		return ResultData<Agendamento>.Success(agendamento);
	}








}