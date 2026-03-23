using AutoMapper;
using BarberCode.Domain.Entities.Barbearias;
using BarberCode.Service.Requests;
using BarberCode.Service.Responses;

namespace BarberCode.Application.Profiles;

public class BarbeariaProfile : Profile
{
	public BarbeariaProfile()
	{
		CreateMap<CriarBarbeariaRequest, Barbearia>();
		CreateMap<CriarEnderecoRequest, Endereco>();
		CreateMap<CriarHorarioFuncionamentoRequest, HorarioFuncionamento>();
		CreateMap<Barbearia, BarbeariaResponse>();
		CreateMap<Endereco, EnderecoResponse>();
		CreateMap<HorarioFuncionamento, HorarioFuncionamentoResponse>();
	}
}
