using BarberCode.Domain.Entities.Barbearias;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberCode.Application.Interfaces;

public interface IClienteRepository
{
	Task  SalvarClienteAsync(ClienteInfo cliente);
	Task <ClienteInfo?> BuscarClientePeloTelefoneAsync(string telefone);
	Task <IEnumerable<ClienteInfo>> BuscarClientesAsync(Guid BarbeariaId);
}
