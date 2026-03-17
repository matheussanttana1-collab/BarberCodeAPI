namespace BarberCode.Domain.Entities.Agendamentos;

public class ClienteInfo
{
	public string Name { get; private set; }
	public int Phone { get; private set; }

	
	protected ClienteInfo() { }

	public ClienteInfo(string name, int phone)
	{
		Name = name;
		Phone = phone;
	}
}