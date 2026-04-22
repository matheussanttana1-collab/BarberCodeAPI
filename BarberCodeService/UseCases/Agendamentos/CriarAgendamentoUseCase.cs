using BarberCode.Application.EventsHandlers;
using BarberCode.Application.Interfaces;
using BarberCode.Domain.Entities.Barbearias;
using BarberCode.Domain.Shared;
using BarberCode.Service.Requests;
using Microsoft.Extensions.Logging.Abstractions;


namespace BarberCode.Application.UseCases.Agendamentos;

public class CriarAgendamentoUseCase
{

	private readonly IBarbeariaRepository _barbeariaRepo;
	private readonly IAgendamentoRepository _agendamentoRepo;
	private readonly IClienteRepository _clienteRepo;
	private readonly IAppUserService _appUserService;
	private readonly IEventBus _eventBus;

	public CriarAgendamentoUseCase(IBarbeariaRepository barbeariaRepo, IAgendamentoRepository agendamentoRepo,
	IClienteRepository clienteRepo, IAppUserService appUserService, IEventBus eventBus)
	{
		_barbeariaRepo = barbeariaRepo;
		_agendamentoRepo = agendamentoRepo;
		_clienteRepo = clienteRepo;
		_appUserService = appUserService;
		_eventBus = eventBus;
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
			if (!clienteResult.IsSuccess)
				return ResultData<Guid>.Failure(clienteResult.Type, clienteResult.Message);
			cliente = clienteResult.Data;

			// 🆕 Cria o AppUser para o cliente quando é o primeiro agendamento
			var usuarioClienteResult = await _appUserService.CadastrarClienteAsync(
				cliente!.Id,
				cliente.Celular,
				cliente.Name
			);

			if (!usuarioClienteResult.IsSuccess)
				return ResultData<Guid>.Failure(usuarioClienteResult.Type, usuarioClienteResult.Message);
			await _clienteRepo.SalvarClienteAsync(cliente!);
		}

		var status = barbearia.EstaFuncionando(request.Dia, request.Horario);
		if (!status.IsSuccess)
			return ResultData<Guid>.Failure(status.Type, status.Message);

		var agendamentoResult = barbeiro.NovoAgendamento(cliente!.Id, request.Dia, request.Horario, 
		servico.Duracao, request.ServicoId);
		if (!agendamentoResult.IsSuccess)
			return ResultData<Guid>.Failure(agendamentoResult.Type, agendamentoResult.Message);

		await _agendamentoRepo.SalvarAgendadamentoAsync(agendamentoResult.Data!);

		await _eventBus.PublishAsync(new EnviarMensagemEvent(cliente.Celular, barbearia.Slug, barbearia.Nome, 
		cliente.Name, request.Dia,barbeiro.Nome, barbearia.Endereco));

		return ResultData<Guid>.Success(agendamentoResult.Data!.Id);
	}
}

