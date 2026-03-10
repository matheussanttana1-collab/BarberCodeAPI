using System.ComponentModel.DataAnnotations;

namespace BarberCode.Service.Requests;

public record BarbeiroRequest(
    [Required] string Nome,
    string? FotoPerfil
);
