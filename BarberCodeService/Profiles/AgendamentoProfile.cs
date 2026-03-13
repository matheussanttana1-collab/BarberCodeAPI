using AutoMapper;
using BarberCode.Domain.Entities.Agendamentos;
using BarberCode.Service.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberCode.Application.Profiles;

public class AgendamentoProfile : Profile
{
	public AgendamentoProfile()
	{
		CreateMap<AgendamentoRequest, Agendamento>();
	}
}
