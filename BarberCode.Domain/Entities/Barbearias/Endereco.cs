using System.ComponentModel.Design;

namespace BarberCode.Domain.Entities.Barbearias;

public class Endereco
{
	public Endereco(string lougradouro, string nome, int numero, string cidade, string estado)
	{
		Lougradouro = lougradouro;
		Nome = nome;
		Numero = numero;
		Cidade = cidade;
		Estado = estado;
	}

	private Endereco() { }

	public string Lougradouro { get; private set; }
	public string Nome { get; private set; }
	public int Numero { get; private set; }
	public string Cidade { get; private set; }
	public string Estado { get; private set; }

}
