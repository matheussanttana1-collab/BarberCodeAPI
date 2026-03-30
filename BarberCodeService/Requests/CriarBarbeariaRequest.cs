using System.ComponentModel.DataAnnotations;

namespace BarberCode.Service.Requests;

public class CriarBarbeariaRequest
{
    public string Name { get; set; }
    public string Celular { get; set; }
    public EnderecoRequest Endereco { get; set; }
    public List<HorarioFuncionamentoRequest> Funcionamento { get; set; } = new List<HorarioFuncionamentoRequest>();
    public string Email { get; set; }
    public string Senha { get; set; }
}
