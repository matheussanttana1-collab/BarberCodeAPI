namespace BarberCode.Service.Responses;

public class BarbeariaResponse
{
	public Guid Id { get; set; }
	public string Name { get; set; }
	public EnderecoResponse Endereco { get; set; }
	public List<HorarioFuncionamentoResponse> Funcionamento { get; set; }
}