using System.ComponentModel.DataAnnotations;

namespace BarberCode.Service.Responses;

public record BarbeiroResponse(
    Guid Id,
    [Required] string Nome,
    string? FotoPerfil
);
