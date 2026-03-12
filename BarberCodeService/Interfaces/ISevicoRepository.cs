using BarberCode.Domain.Entities.Barbearias;
using BarberCode.Domain.Entities.Barbeiros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberCode.Application.Interfaces;

public interface IServicoRepository
{
	void SalvarBarbeiro(Servico servico);
	Servico BuscarServicoPor(Guid Id);
	void DeletarServico(Servico servico);
	public void Atualizar(Servico servico);

}
