using BarberCode.Domain.Entities.Agendamentos;

namespace BarberCode.Service.Responses;


public class AgendamentoResponse 
{
	public Guid Id {  get; set; }
	public Guid BarbeiroId { get; set; }
	public Guid BarbeariaId { get; set; }
	public Guid ServicoId { get; set; }
	public DateOnly Dia {  get; set; }
	public TimeOnly Horario { get; set; }
	public ClienteInfoResponse Cliente { get; set; }
	public string Status { get; set; }
}
