namespace BarberCode.Domain.Entities.Agendamentos;

public class ClienteInfo
{
	public string Name { get; private set; }
	public string Celular { get; private set; }

	
	protected ClienteInfo() { }

	public ClienteInfo(string name, string celular)
	{
		Name = name;
		Celular = celular;
	}
}