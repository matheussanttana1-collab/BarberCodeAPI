using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace BarberCode.Service.Requests;

public record AgendamentoRequest(
    Guid BarbeiroId,
    Guid BarbeariaId,
    Guid ServicoId,
    DateTime Horario,
    [Required] string ClienteNome,
    int ClienteTelefone
);
