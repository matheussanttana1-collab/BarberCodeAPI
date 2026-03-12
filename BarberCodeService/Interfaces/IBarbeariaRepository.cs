using BarberCode.Domain.Entities.Barbearias;

namespace BarberCode.Application.Interfaces;

public interface IBarbeariaRepository
{
	void SalvarBarbearia(Barbearia barbearia);
	Barbearia? BuscarBarbeariaPor(Guid id);
	void AtualizarBarbearia(Barbearia barbearia);
	void DeletarBarbearia(Barbearia barbearia);
}
