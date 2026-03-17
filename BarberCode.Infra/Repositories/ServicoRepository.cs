using BarberCode.Application.Interfaces;
using BarberCode.Domain.Entities.Barbearias;
using BarberCode.Infra.Banco;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberCode.Infra.Repositories;

public class ServicoRepository : IServicoRepository
{
	private readonly BarberCodeContext _context;

	public ServicoRepository(BarberCodeContext context)
	{
		_context = context;
	}

	public void SalvarServico(Servico servico)
	{
		_context.servicos.Add(servico);
		_context.SaveChanges();
	}

	public IEnumerable<Servico> BuscarServicos (Guid BarbeariaId) 
	{
		var servicos = _context.servicos.Where(s => s.BarbeariaId == BarbeariaId).ToList();
		return servicos;
	}
	public Servico? BuscarServicoPor(Guid Id)
	{
		var servico = _context.servicos.FirstOrDefault(s => s.Id == Id);
		return servico;
	}

	public void DeletarServico(Servico servico)
	{
		throw new NotImplementedException();
	}

	public void Atualizar(Servico servico)
	{
		throw new NotImplementedException();
	}
}
