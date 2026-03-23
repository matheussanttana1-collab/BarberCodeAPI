using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberCode.Application.Requests;

public record AtualizarServicoRequest (string? name, string? descricao, int? duracao, double? preco)
{
}
