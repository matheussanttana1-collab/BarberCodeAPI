namespace BarberCode.Service.Responses;

public record AgendamentoResponse(
    Guid Id,
    Guid BarbeiroId,
    string BarbeiroNome,
    Guid BarbeariaId,
    string BarbeariaNome,
    Guid ServicoId,
    string ServicoNome,
    DateTime Horario,
    ClienteInfoResponse Cliente
);
