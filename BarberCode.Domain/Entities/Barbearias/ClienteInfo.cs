using BarberCode.Domain.Shared;

namespace BarberCode.Domain.Entities.Barbearias;

public class ClienteInfo
{
	public Guid Id { get; private set; }
	public string Name { get; private set; }
	public string Celular { get; private set; }
	public Guid BarbeariaId { get; private set; }

	
	protected ClienteInfo() { }

	private ClienteInfo(string name, string celular, Guid barbeariaId)
	{
		Id = Guid.NewGuid();
		Name = name;
		Celular = celular;
		BarbeariaId = barbeariaId;
	}

	public static ResultData<ClienteInfo> CriarCliente(string name, string celular, Guid barbeariaId)
	{
		if (string.IsNullOrWhiteSpace(name))
			return ResultData<ClienteInfo>.Failure(ResultType.Validation, "Nome do cliente não pode " +
			"ser nulo");

		return ResultData<ClienteInfo>.Success(new ClienteInfo(name, celular, barbeariaId));
	}

}