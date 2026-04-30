using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace BarberCode.Application.Responses;

public record GerarSlotsResponse (Guid barbeiroId,DateOnly dia, List<TimeOnly> slots) { }

