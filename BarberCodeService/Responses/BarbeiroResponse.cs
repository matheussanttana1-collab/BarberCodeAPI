using System.Runtime.CompilerServices;

namespace BarberCode.Service.Responses;



public class BarbeiroResponse 
{
	Guid Id {  get; set; }
	public string Name { get; set; }
	public string? FotoPerfil { get; set; }
}

