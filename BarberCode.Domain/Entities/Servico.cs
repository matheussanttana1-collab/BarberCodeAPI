namespace BarberCode.Domain.Entities;

public class Servico
{
	public Servico(Guid id, string name, int duracao, string? descricao)
	{
		Id = id;
		Name = name;
		Duracao = duracao;
		Descricao = descricao;
	}

	public Guid Id { get; set; }
	public string Name { get; set; }

	public int Duracao { get; set; }
	public string? Descricao { get; set; }

}
