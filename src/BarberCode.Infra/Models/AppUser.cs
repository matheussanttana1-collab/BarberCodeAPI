using BarberCode.Application.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberCode.Infra.Models;

public class AppUser : IdentityUser
{
	public TipoUsuario TipoUsuario { get; set; }
}
