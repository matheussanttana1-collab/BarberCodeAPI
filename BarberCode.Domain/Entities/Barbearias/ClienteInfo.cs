namespace BarberCode.Domain.Entities.Barbearias;

public class ClienteInfo
{
	public Guid Id { get; private set; }
	public string Name { get; private set; }
	public string Celular { get; private set; }
	public Guid BarbeariaId { get; private set; }

	
	protected ClienteInfo() { }

	public ClienteInfo(string name, string celular, Guid barbeariaId)
	{
		Id = Guid.NewGuid();
		Name = name;
		if (Name is null)
			throw new Exception("Nome do Cliente Nao Pode Ser Nulo");
		Celular = celular;
		BarbeariaId = barbeariaId;
	}


}