using System.ComponentModel.DataAnnotations;

namespace BarberCode.Service.Requests;

public record CriarAgendamentoRequest(Guid BarbeiroId, Guid BarbeariaId, Guid ServicoId, DateOnly Dia, 
TimeOnly Horario, CriarClienteInfoRequest Cliente);
