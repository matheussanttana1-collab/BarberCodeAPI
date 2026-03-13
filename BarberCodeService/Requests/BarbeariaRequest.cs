using System.ComponentModel.DataAnnotations;

namespace BarberCode.Service.Requests;

public class BarbeariaRequest
{
    [Required(ErrorMessage = "Nome é obrigatório")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Endereço é obrigatório")]
    public EnderecoRequest Endereco { get; set; }

    [Required(ErrorMessage = "Horários de funcionamento são obrigatórios")]
    public IEnumerable<HorarioFuncionamentoRequest> Funcionamento { get; set; } = new List<HorarioFuncionamentoRequest>();
}
