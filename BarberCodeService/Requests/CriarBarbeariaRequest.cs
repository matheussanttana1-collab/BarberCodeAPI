using System.ComponentModel.DataAnnotations;

namespace BarberCode.Service.Requests;

public class CriarBarbeariaRequest
{
    public string Name { get; set; }
    public string Celular { get; set; }
    public CriarEnderecoRequest Endereco { get; set; }
    public List<CriarHorarioFuncionamentoRequest> Funcionamento { get; set; } = new List<CriarHorarioFuncionamentoRequest>();
}
