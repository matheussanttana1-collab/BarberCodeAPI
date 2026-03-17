using AutoMapper;
using BarberCode.Domain.Entities.Barbeiros;
using BarberCode.Service.Requests;
using BarberCode.Service.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberCode.Application.Profiles;

public class BarbeiroProfile : Profile
{
	public BarbeiroProfile()
	{
		CreateMap<BarbeiroResponse, Barbeiro>();
	}
}
