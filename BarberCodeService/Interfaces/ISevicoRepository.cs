using BarberCode.Domain.Entities.Barbearias;

namespace BarberCode.Application.Interfaces;

public interface IServicoRepository
{
	void SalvarServico(Servico servico);
	IEnumerable<Servico> BuscarServicos(Guid barbeariaId);
	Servico? BuscarServicoPor(Guid Id);
	void DeletarServico(Servico servico);
	public void AtualizarServico();

}
