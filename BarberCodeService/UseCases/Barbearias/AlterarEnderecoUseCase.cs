using AutoMapper;
using BarberCode.Application.Interfaces;
using BarberCode.Domain.Entities.Barbearias;
using BarberCode.Domain.Shared;
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


	public async Task<ResultData> ExecuteAsync(Guid id, EnderecoRequest request)
	{
		var barbearia = await _repo.BuscarBarbeariaPorAsync(id);
		if (barbearia == null)
			return ResultData.Failure(ResultType.NotFound, "Barbearia não Encontrada");

		barbearia.AlterarEndereco(new Endereco(request.Logradouro, request.Bairro, request.Numero,
		request.Cidade, request.Estado, request.CEP));

		await _repo.AtualizarBarbeariaAsync();

		return ResultData.Success();
	}
}
