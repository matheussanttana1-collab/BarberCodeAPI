namespace BarberCode.Service.Responses;

public record HorarioFuncionamentoResponse(
    DayOfWeek Dia,
    TimeOnly Incio,
    TimeOnly Fim
);
