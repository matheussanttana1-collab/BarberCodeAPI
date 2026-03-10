using BarberCode.Domain.Entities;
using BarberCode.Domain.Entities.Agendamentos;
using BarberCode.Domain.Entities.Barbearias;
using BarberCode.Domain.Entities.Barbeiros;
using Microsoft.EntityFrameworkCore;

namespace BarberCode.Infra.Banco;

public class BarberCodeContext : DbContext
{

	public BarberCodeContext(DbContextOptions<BarberCodeContext> opts) : base(opts)
	{
		
	}

	public DbSet<Barbearia> barbearias { get; set; }
	public DbSet<Agendamento> agendamentos { get; set; }
	public DbSet<Barbeiro> barbeiros{ get; set; }
	public DbSet<Servico> servicos { get; set; }

}
