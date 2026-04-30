using System.ComponentModel.DataAnnotations;

namespace BarberCode.Service.Requests;

public record CriarBarbeariaRequest(string Name, string Celular, EnderecoRequest Endereco, List<HorarioFuncionamentoRequest>? Funcionamento, string Email, string Senha);

