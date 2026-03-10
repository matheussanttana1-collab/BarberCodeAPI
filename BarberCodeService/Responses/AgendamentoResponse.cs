using System.ComponentModel.DataAnnotations;

namespace BarberCode.Service.Responses;

public record AgendamentoResponse(
    Guid Id,
    Guid BarbeiroId,
    [Required] string BarbeiroNome,
    Guid BarbeariaId,
    [Required] string BarbeariaNome,
    Guid ServicoId,
    [Required] string ServicoNome,
    DateTime Horario,
    [Required] ClienteInfoResponse Cliente
);
