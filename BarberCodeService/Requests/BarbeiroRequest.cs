using System.ComponentModel.DataAnnotations;

namespace BarberCode.Service.Requests;

public class BarbeiroRequest
{
    [Required(ErrorMessage = "Nome é obrigatório")]
    public string Nome { get; set; }
    [Required(ErrorMessage = "O Horario de Almoço é obrigatório")]
    public List<TimeOnly> HorarioAlmoco {  get; set; } = new List<TimeOnly>();
    public string? FotoPerfil { get; set; }
 
}
