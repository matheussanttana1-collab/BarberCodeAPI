using AutoMapper;
using BarberCode.Domain.Entities.Barbearias;
using BarberCode.Service.Requests;

namespace BarberCode.Application.Profiles;

public class BarbeariaProfile : Profile
{

	public BarbeariaProfile()
	{
		CreateMap<BarbeariaRequest, Barbearia>();
		CreateMap<EnderecoRequest, Endereco>();
		CreateMap<HorarioFuncionamentoRequest, HorarioFuncionamento>();
	}
}
