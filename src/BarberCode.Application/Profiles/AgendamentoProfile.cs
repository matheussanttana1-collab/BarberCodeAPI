using AutoMapper;
using BarberCode.Domain.Entities.Agendamentos;
using BarberCode.Domain.Entities.Barbearias;
using BarberCode.Service.Requests;
using BarberCode.Service.Responses;

namespace BarberCode.Application.Profiles;

public class AgendamentoProfile : Profile
{
	public AgendamentoProfile()
	{
		CreateMap<Agendamento, AgendamentoResponse>()
		.ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));

		CreateMap<ClienteInfo, ClienteInfoResponse>();
	}
}
