namespace BarberCode.Domain.Entities.Barbearias;

using BarberCode.Domain.Entities.Barbeiros;


public class Servico
{
	private Servico()
	{

	}

	public Servico(string name, int duracao, string? descricao)
	{
		Id = Guid.NewGuid();
		Name = name;
		Duracao = duracao;
		Descricao = descricao;
	}

	public Guid Id { get; private set; }
	public string Name { get; private set; }
	public int Duracao { get; private set; }
	public string? Descricao { get; private set; }
	public int BarbeariaId { get; private set; }
	public virtual Barbearia Barbearia { get; private set; }

}
