namespace BarberCode.Service.Responses;

public class BarbeariaResponse
{
	public Guid Id { get; set; }
	public string Nome { get; set; }
	public string Celular { get; set; }
	public EnderecoResponse Endereco { get; set; }
	public List<HorarioFuncionamentoResponse> Funcionamento { get; set; }
}