using BarberCode.Application.Interfaces;
using BarberCode.Domain.Entities.Agendamentos;
using BarberCode.Domain.Entities.Barbearias;
using BarberCode.Domain.Shared;
using BarberCode.Service.Requests;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberCode.Application.UseCases.Agendamentos;

public class CriarAgendamentoUseCase
{
	private readonly IBarbeiroRepository _barbeiroRepo;
	private readonly IBarbeariaRepository _barbeariaRepo;
	private readonly IAgendamentoRepository _agendamentoRepo;
	private readonly IClienteRepository _clienteRepo;

	public CriarAgendamentoUseCase(IBarbeiroRepository barbeiroRepo, IBarbeariaRepository barbeariaRepo
	, IAgendamentoRepository agendamentoRepo,IClienteRepository clienteRepo)
	{
		_barbeariaRepo = barbeariaRepo;
		_barbeiroRepo = barbeiroRepo;
		_agendamentoRepo = agendamentoRepo;
		_clienteRepo = clienteRepo;
	}
	public async Task<ResultData<Guid>> ExecuteAsync(CriarAgendamentoRequest request)
	{
		var barbearia = await _barbeariaRepo.BuscarBarbeariaPorAsync(request.BarbeariaId);
		if (barbearia is null)
			return ResultData<Guid>.Failure(ResultType.NotFound, "Barbearia Não Encontrada");

		var barbeiro = barbearia.Barbeiros.FirstOrDefault(b => b.Id == request.BarbeiroId);
		if (barbeiro is null)
			return ResultData<Guid>.Failure(ResultType.NotFound, "Barbeairo não cadastrado ou Não" +
			"pertence a esta Barbearia");

		var servico = barbearia.Servicos.FirstOrDefault(s => s.Id == request.ServicoId);
		if (servico is null)
			return ResultData<Guid>.Failure(ResultType.NotFound, "Barbearia Não Oferece este Serviço");

		var cliente = await _clienteRepo.BuscarClientePeloTelefoneAsync(request.Cliente.Celular);
		if (cliente is null)
		{
			var clienteResult = ClienteInfo.CriarCliente(request.Cliente.Nome, request.Cliente.Celular, 
			barbearia.Id);
			if (clienteResult.Type == ResultType.Validation)
				return ResultData<Guid>.Failure(clienteResult.Type, clienteResult.Message);
			cliente = clienteResult.Data;
			await _clienteRepo.SalvarClienteAsync(cliente);
		}

		var status = barbearia.EstaFuncionando(request.Dia, request.Horario);
		if (status.Type == ResultType.Validation)
			return ResultData<Guid>.Failure(status.Type, status.Message);

		var agendamentoResult = barbeiro.NovoAgendamento(cliente.Id, request.Dia, request.Horario, 
		servico.Duracao, request.ServicoId);
		if (agendamentoResult.Type == ResultType.Conflict)
			return ResultData<Guid>.Failure(ResultType.Conflict, agendamentoResult.Message);

		await _agendamentoRepo.SalvarAgendadamentoAsync(agendamentoResult.Data);

		return ResultData<Guid>.Success(agendamentoResult.Data.Id);
	}
}

