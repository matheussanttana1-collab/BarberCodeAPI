using AutoMapper;
using BarberCode.Domain.Entities.Barbearias;
using BarberCode.Service.Requests;

namespace BarberCode.Application.Profiles;

public class ServicoProfile : Profile
{
	public ServicoProfile()
	{
		CreateMap<ServicoRequest, Servico>();
	}
}
