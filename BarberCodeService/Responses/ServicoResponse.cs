using System.ComponentModel.DataAnnotations;

namespace BarberCode.Service.Responses;

public record ServicoResponse(
    Guid Id,
    [Required] string Nome,
    int Duracao,
    [Required] string Descricao
);
