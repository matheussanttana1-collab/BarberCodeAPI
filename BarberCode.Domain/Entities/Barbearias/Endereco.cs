namespace BarberCode.Domain.Entities.Barbearias;

public class Endereco
{
	public Endereco(int id, string lougradouro, string nome, int numero, string cidade, string estado)
	{
		Id = id;
		Lougradouro = lougradouro;
		Nome = nome;
		Numero = numero;
		Cidade = cidade;
		Estado = estado;
	}

	public int Id { get;}
	public string Lougradouro { get;}
	public string Nome{ get;}
	public int Numero { get;}
	public string Cidade { get;}
	public string Estado { get;}

}
