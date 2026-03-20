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
	void SalvarCliente(ClienteInfo cliente);
	ClienteInfo? BuscarClientePeloTelefone(string telefone);

	IEnumerable<ClienteInfo> BuscarClientes(Guid BarbeariaId);
}
