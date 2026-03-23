using System.ComponentModel.Design;

namespace BarberCode.Domain.Entities.Barbearias;

public class Endereco
{
	public Endereco(string logradouro, string bairro, int numero, string cidade, string estado, string cEP)
	{
		Logradouro = logradouro;
		Bairro = bairro;
		Numero = numero;
		Cidade = cidade;
		Estado = estado;
		CEP = cEP;
	}

	protected Endereco() { }

	public string Logradouro { get; private set; }
	public int Numero { get; private set; }
	public string Bairro { get; private set; }
	public string Cidade { get; private set; }
	public string Estado { get; private set; }
	public string CEP { get; private set; }

}
