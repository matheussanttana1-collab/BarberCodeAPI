using BarberCode.Application.Interfaces;
using BarberCode.Domain.Entities.Agendamentos;
using BarberCode.Domain.Shared;
using BarberCode.Service.Responses;
using AutoMapper;

namespace BarberCode.Application.UseCases.Agendamentos;

public class ListarAgendamentosBarbeiroUseCase
{
	private readonly IAgendamentoRepository _agendamentoRepo;
	private readonly IBarbeiroRepository _barbeiroRepo;
	private readonly IMapper _mapper;

	public ListarAgendamentosBarbeiroUseCase(IAgendamentoRepository agendamentoRepo, IBarbeiroRepository barbeiroRepo, IMapper mapper)
	{
		_agendamentoRepo = agendamentoRepo;
		_barbeiroRepo = barbeiroRepo;
		_mapper = mapper;
	}

	public async Task<ResultData<IEnumerable<AgendamentoResponse>>> ExecuteAsync(Guid barbeiroId, Guid userId, StatusAgendamento? status, DateOnly? dia)
	{
		// Busca o barbeiro para obter a barbearia
		var barbeiro = await _barbeiroRepo.BuscarBarbeiroPorAsync(barbeiroId);
		if (barbeiro is null)
			return ResultData<IEnumerable<AgendamentoResponse>>.Failure(ResultType.NotFound, "Barbeiro não encontrado");

		// Verifica se o usuário logado é o próprio barbeiro ou o manager (barbearia)
		bool temPermissao = barbeiroId == userId || barbeiro.BarbeariaId == userId;
		if (!temPermissao)
			return ResultData<IEnumerable<AgendamentoResponse>>.Failure(ResultType.Forbidden, "Você não tem permissão para acessar os agendamentos deste barbeiro");

		// Busca os agendamentos com filtros opcionais
		var agendamentos = await _agendamentoRepo.BuscarAgendamentosAsync(barbeiroId, status, dia);

		var agendamentosResponse = _mapper.Map<IEnumerable<AgendamentoResponse>>(agendamentos);
		return ResultData<IEnumerable<AgendamentoResponse>>.Success(agendamentosResponse);
	}
}
