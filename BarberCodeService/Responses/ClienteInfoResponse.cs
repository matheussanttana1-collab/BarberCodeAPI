using System.ComponentModel.DataAnnotations;

namespace BarberCode.Service.Responses;

public record ClienteInfoResponse(
    [Required] string Nome,
    int Telefone
);
