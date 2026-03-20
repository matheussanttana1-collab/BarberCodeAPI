using BarberCode.Application.Interfaces;
using BarberCode.Domain.Entities.Agendamentos;
using BarberCode.Domain.Entities.Barbearias;
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
	public void Execute (AgendamentoRequest request)
	{
		var barbearia = _barbeariaRepo.BuscarBarbeariaPor(request.BarbeariaId);
		if (barbearia is null)
			throw new ApplicationException("Barbearia Não Cadastrada");
		var barbeiro = _barbeiroRepo.BuscarBarbeiroPor(request.BarbeiroId);
		if (barbeiro is null)
			throw new ApplicationException("Barbeiro não cadastrado, ou nao pertence a esta Barbearia");
		var servico = barbearia.Servicos.FirstOrDefault(s => s.Id == request.ServicoId);
		if (servico is null)
			throw new ApplicationException("Barbearia escolhida nao oferece este serviço");
		var cliente = _clienteRepo.BuscarClientePeloTelefone(request.Cliente.Celular);
		if (cliente is null)
		{
			cliente = new ClienteInfo(request.Cliente.Nome, request.Cliente.Celular, barbearia.Id);
			_clienteRepo.SalvarCliente(cliente);
		}
		barbearia.EstaFuncionando(request.Dia, request.Horario);
		var agendamento = barbeiro.NovoAgendamento(cliente.Id,request.Dia,request.Horario,servico.Duracao,request.ServicoId);

		_agendamentoRepo.SalvarAgendadamento(agendamento);
	}
}
