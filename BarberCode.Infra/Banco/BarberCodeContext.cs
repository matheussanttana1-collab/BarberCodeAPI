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

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		modelBuilder.Entity<Barbearia>().OwnsOne(b => b.Endereco);
		modelBuilder.Entity<Barbearia>().OwnsMany(b => b.Funcionamento);
		modelBuilder.Entity<Barbearia>().HasKey(b => b.Id);
		modelBuilder.Entity<Agendamento>().HasKey(a => a.Id);
		modelBuilder.Entity<Barbeiro>().HasKey(b => b.Id);

	}

	public DbSet<Barbearia> barbearias { get; set; }
	public DbSet<Agendamento> agendamentos { get; set; }
	public DbSet<Barbeiro> barbeiros{ get; set; }
	public DbSet<Servico> servicos { get; set; }
	public DbSet<ClienteInfo> clientes { get; set; }

}
