namespace BarberCode.Domain.Entities.Barbearias;

using BarberCode.Domain.Entities.Barbeiros;
using BarberCode.Domain.Shared;

public class Servico
{
	protected Servico()
	{

	}

	public Servico(string name, int duracao, string? descricao,double preco, Guid barbeariaId)
	{
		Id = Guid.NewGuid();
		Nome = name;
		Duracao = duracao;
		Descricao = descricao;
		Preco = preco;
		BarbeariaId = barbeariaId;
	}
	public Guid Id { get; private set; }
	public string Nome { get; private set; }
	public int Duracao { get; private set; }
	public string? Descricao { get; private set; }
	public double Preco { get; private set; }
	public Guid BarbeariaId { get; private set; }
	public virtual Barbearia Barbearia { get; private set; }

	public void AlterarServico (string? nome, string? descricao, int? duracao) {
		if (nome is not null) 
			Nome = nome;
		if (descricao is not null) 
			Descricao = descricao;
		if (duracao is not null)
			Duracao = duracao.Value;
	}

}
