using System.ComponentModel.DataAnnotations;

namespace BarberCode.Service.Responses;

public record BarbeariaResponse(
    Guid Id,
    [Required] string Nome
);
