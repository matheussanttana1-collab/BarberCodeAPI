using System.Runtime.CompilerServices;

namespace BarberCode.Service.Responses;



public class BarbeiroResponse 
{
	public Guid Id {  get; set; }
	public string Nome { get; set; }
	public string? FotoPerfil { get; set; }
}

