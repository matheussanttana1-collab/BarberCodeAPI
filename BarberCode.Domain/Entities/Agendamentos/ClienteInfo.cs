namespace BarberCode.Domain.Entities.Agendamentos;

public class ClienteInfo
{
	public string Name { get; private set; }
	public int Phone { get; private set; }

	
	private ClienteInfo() { }

	public ClienteInfo(string name, int phone)
	{
		if (string.IsNullOrWhiteSpace(name))
			throw new ArgumentException("Nome do cliente é obrigatório");

		if (phone <= 0)
			throw new ArgumentException("Telefone deve ser um número válido");

		Name = name;
		Phone = phone;
	}
}