using System.ComponentModel.DataAnnotations;

namespace BarberCode.Service.Requests;
 
public class CriarBarbeiroRequest
{

    [Required(ErrorMessage = "Nome é obrigatório")]
    public string Nome { get; set; }
    [Required(ErrorMessage = "O Horario de Almoço é obrigatório")]
    public TimeOnly HorarioAlmoco {  get; set; }
    public string? FotoPerfil { get; set; }
 
}
