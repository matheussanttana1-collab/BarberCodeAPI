using BarberCode.Domain.Entities.Barbearias;

namespace BarberCode.Application.Interfaces;

public interface IBarbeariaRepository
{
	Task<IEnumerable<Barbearia>> BuscarBarbeariasAsync();
	Task SalvarBarbeariaAsync(Barbearia barbearia);
	Task<Barbearia?> BuscarBarbeariaPorAsync(Guid id);
	Task  AtualizarBarbeariaAsync();
	Task  DeletarBarbeariaAsync(Barbearia barbearia);
}
