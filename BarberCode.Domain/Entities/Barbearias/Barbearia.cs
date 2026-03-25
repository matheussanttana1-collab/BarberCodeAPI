namespace BarberCode.Domain.Entities.Barbearias;

using Barbeiros;
using BarberCode.Domain.Shared;
using System.Reflection.Metadata.Ecma335;

public class Barbearia
{

	protected Barbearia () { }

	public Barbearia(string name, Endereco endereco, List<HorarioFuncionamento> funcionamento, string celular)
	{
		Id = Guid.NewGuid();
		Name = name;
		Endereco = endereco;
		Funcionamento = funcionamento;
		Celular = celular;
	}

	public Guid Id { get; private set; }
	public string Name { get; private set; }
	public Endereco Endereco { get; private set; }
	public string Celular { get; private set; }
	public List<HorarioFuncionamento> Funcionamento { get; private set; } = new List<HorarioFuncionamento>();
	public virtual ICollection<Barbeiro> Barbeiros { get; private set; } = new List<Barbeiro>();
	public virtual ICollection<Servico> Servicos { get; private set; } = new List<Servico>();
	public virtual ICollection<ClienteInfo> Clientes { get; private set; } = new List<ClienteInfo>();

	public ResultData EstaFuncionando(DateOnly dia, TimeOnly hora) 
	{
		var diaSemana = Funcionamento.FirstOrDefault(d => d.dia == dia.DayOfWeek);
		if (diaSemana is null || hora < diaSemana.Inicio && hora > diaSemana.Fim)
			return ResultData.Failure(ResultType.Validation, "Barbearia Fechada");

		return ResultData.Success();
	}

	public HorarioFuncionamento? ExpedienteDia(DateOnly diaEscolhido) 
	{
		return Funcionamento.FirstOrDefault(f => f.dia == diaEscolhido.DayOfWeek);

	}

	public void AddServico(string name, string descricao, int duracao, double preco)
	{
		if(Servicos.Any(s => s.Name == name))
		{
			throw new Exception("Servico Ja Cadastrado");
		}
		Servicos.Add(new Servico(name,duracao,descricao,preco,this.Id));
	}

	public void AlterarEndereco (Endereco novoEndereco) {
		Endereco = novoEndereco;
	}

	public void AlterarFuncionamento(List<HorarioFuncionamento> funcionamentos)
	{
		Funcionamento.Clear();
		Funcionamento.AddRange(funcionamentos);
	}
	
}
