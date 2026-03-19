using AutoMapper;
using BarberCode.Application.Interfaces;
using BarberCode.Domain.Entities.Barbearias;
using BarberCode.Service.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberCode.Application.UseCases.Barbearias;

public class AlterarEnderecoUseCase
{
	private readonly IBarbeariaRepository _repo;

	public AlterarEnderecoUseCase(IBarbeariaRepository repo)
	{
		_repo = repo;
	}


	public void Execute(Guid id, EnderecoRequest request)
	{
		var barbearia = _repo.BuscarBarbeariaPor(id);
		if (barbearia == null)
			throw new Exception("Barbearia não Encontrado");

		barbearia.AlterarEndereco(new Endereco(request.Lougradouro, request.Nome, request.Numero, request.Cidade
		, request.Estado));

		_repo.AtualizarBarbearia();
	}
}
