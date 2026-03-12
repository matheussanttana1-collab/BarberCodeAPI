using BarberCode.Domain.Entities.Barbeiros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberCode.Application.Interfaces;

public interface IBarbeiroRepository
{
	void SalvarBarbeiro(Barbeiro barbeiro);
	Barbeiro? BuscarBarbeiroPor(Guid Id);
	void DeletarBarbeiro(Barbeiro barbeiro);
	public void AtualizarBarbeiro(Barbeiro barbeiro);
}
