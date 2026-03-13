using BarberCode.Domain.Entities.Barbearias;

namespace BarberCode.Application.Interfaces;

public interface IBarbeariaRepository
{
	void SalvarBarbearia(Barbearia barbearia);
	Barbearia? BuscarBarbeariaPor(Guid id);
	void AtualizarBarbearia();
	void DeletarBarbearia(Barbearia barbearia);
}
