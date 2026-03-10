namespace BarberCode.Service.Responses;

public record BarbeiroResponse(
    Guid Id,
    string Nome,
    string? FotoPerfil
);
