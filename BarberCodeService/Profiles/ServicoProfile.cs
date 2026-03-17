using AutoMapper;
using BarberCode.Domain.Entities.Barbearias;
using BarberCode.Service.Requests;
using BarberCode.Service.Responses;

namespace BarberCode.Application.Profiles;

public class ServicoProfile : Profile
{
	public ServicoProfile()
	{
		CreateMap<Servico , ServicoResponse>();
	}
}
