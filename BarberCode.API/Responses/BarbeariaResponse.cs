namespace BarberCode.Service.Responses;

public record BarbeariaResponse(
    Guid Id,
    string Nome,
    EnderecoResponse Endereco
);
