using BarberCode.Domain.Entities.Agendamentos;
using BarberCode.Domain.Entities.Barbearias;
using BarberCode.Domain.Entities.Barbeiro;

namespace BarberCode.Domain.Entities.Barbeiros;

public class Barbeiro
{
	public Barbeiro(string name, string? fotoPerfil, Guid barbeariaId)
	{
		Id = new Guid();
		Name = name;
		FotoPerfil = fotoPerfil;
		BarbeariaId = barbeariaId;
	}

	public Barbeiro()
	{

	}

	public Guid Id { get;}
	public string Name { get;}
	public string? FotoPerfil { get; private set; }
	public Guid BarbeariaId { get;}
	public Barbearia BarbeariaQueTrabalha { get;}
	public ICollection<Servico> ServicosPrestados { get;} = new List<Servico>();
	public Agenda Agenda { get; } = new Agenda();


	public List<TimeOnly> GerarSlotsDiponiveis (Barbearia barbearia,DateOnly diaEscolhido, Guid servicoId)
	{
		var servicoEscolhido = ServicosPrestados.FirstOrDefault(s =>  s.Id == servicoId);
		if (servicoEscolhido is null)
		{
			throw new Exception("Este Barbeiro Nao Oferece Esse Serviço");
		}
		return Agenda.HorariosDisponiveis(barbearia, diaEscolhido, servicoEscolhido.Duracao);
	}

	public void NovoAgendamento(ClienteInfo cliente, DateOnly dia,
		TimeOnly horario, Guid servicoId) 
	{
		var servicoEscolhido = ServicosPrestados.FirstOrDefault(s => s.Id == servicoId);
		if (servicoEscolhido is null)
		{
			throw new Exception("Este Barbeiro Nao Oferece Esse Serviço");
		}
		Agenda.NovoAgendamento(new Agendamento(this.Id, this.BarbeariaId, cliente, dia, horario, servicoId, 
				servicoEscolhido.Duracao)
		{ Servico = servicoEscolhido});
	}

	public void AdicionarServicos(Servico servico) 
	{
		if (ServicosPrestados.Any(s => s.Id == servico.Id))
		{
			throw new Exception("Serviço ja Adicionado a este Barbeiro");
		}

		ServicosPrestados.Add(servico);
	}

	public void EditarHorarioAlmoço()
	{

	}






}