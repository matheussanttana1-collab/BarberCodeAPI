using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberCode.Domain.Entities.Barbearias;

public class HorarioFuncionamento
{
	public DayOfWeek dia {  get; set; }
	public TimeOnly Incio { get; set; }
	public TimeOnly Fim { get; set; }
}
