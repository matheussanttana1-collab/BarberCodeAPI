using BarberCode.Domain.Entities.Agendamentos;
using BarberCode.Domain.Entities.Barbeiros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberCode.Application.Interfaces;

public interface IBarbeiroRepository
{
	
	Task SalvarBarbeiroAsync(Barbeiro barbeiro);
	Task<IEnumerable<Barbeiro>> BuscarBarbeirosAsync(Guid barbeariaId);
	Task<Barbeiro?> BuscarBarbeiroPorAsync(Guid Id);
	Task DeletarBarbeiroAsync(Barbeiro barbeiro);
	Task AtualizarBarbeiroAsync();
}
