namespace BarberCode.Service.Responses;

public record EnderecoResponse(
	Guid Id,
	string Logradouro,
	string Nome,
	int Numero,
	string Cidade,
	string Estado
);
