using AutoMapper;
using BarberCode.Domain.Entities.Agendamentos;
using BarberCode.Service.Requests;
using BarberCode.Service.Responses;

namespace BarberCode.Application.Profiles;

public class AgendamentoProfile : Profile
{
	public AgendamentoProfile()
	{
		CreateMap<Agendamento, AgendamentoResponse>();
		CreateMap<ClienteInfo, ClienteInfoResponse>();
	}
}
