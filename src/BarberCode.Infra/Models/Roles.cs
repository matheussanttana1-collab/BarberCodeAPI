using BarberCode.Application.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberCode.Infra.Models;

public class Roles
{
	public static string Barbearia = "Barbearia";
	public static string Barbeiro = "Barbeiro";
	public static string Cliente = "Cliente";

	public static string FromTipoUsuario (TipoUsuario tipo) 
	{
		return tipo switch
		{
			TipoUsuario.Barbearia => Barbearia,
			TipoUsuario.Barbeiro => Barbeiro,
			TipoUsuario.Cliente => Cliente,
		};
	}
}
