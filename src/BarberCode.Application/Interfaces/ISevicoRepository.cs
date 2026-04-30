using BarberCode.Domain.Entities.Barbearias;

namespace BarberCode.Application.Interfaces;

public interface IServicoRepository
{
	Task SalvarServicoAsync(Servico servico);
	Task<IEnumerable<Servico>> BuscarServicosAsync(Guid barbeariaId);
	Task<Servico?> BuscarServicoPorAsync(Guid Id);
	Task DeletarServicoAsync(Servico servico);
	Task AtualizarServicoAsync();
}
