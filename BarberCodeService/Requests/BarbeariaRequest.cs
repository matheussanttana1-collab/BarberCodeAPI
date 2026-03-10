using System.ComponentModel.DataAnnotations;

namespace BarberCode.Service.Requests;

public record BarbeariaRequest(
    [Required] string Nome
);
