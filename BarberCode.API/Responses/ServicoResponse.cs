namespace BarberCode.Service.Responses;

public record ServicoResponse(
    Guid Id,
    string Nome,
    int Duracao,
    string Descricao
);
