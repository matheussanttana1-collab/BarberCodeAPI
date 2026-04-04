using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberCode.Application.Interfaces;

public interface IEmailService
{
	Task sendEmailAsync(string toEmail, string subject, string body);
}
