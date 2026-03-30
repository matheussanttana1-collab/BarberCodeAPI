using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberCode.Infra.Models;

public class AppUser : IdentityUser
{
	public Guid? BarbeariaId { get; set; }
	public Guid? ClienteId { get; set; }
	public Guid? BarbeiroId { get; set; }
}
