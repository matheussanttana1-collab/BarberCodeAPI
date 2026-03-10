using System.ComponentModel.DataAnnotations;

namespace BarberCode.Service.Requests;

public record ServicoRequest(
    [Required] string Nome,
    int Duracao,
    [Required] string Descricao
);
