using BarberCode.Application.Interfaces;
using BarberCode.Application.Models;
using BarberCode.Infra.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace BarberCode.Infra.Service;

public class TokenService : ITokenService
{
	public string GerarToken(AuthUser user, IList<string> roles)
	{
		var claims = new List<Claim>()
		{
			new Claim("Id", user.Id),
			new Claim("Email", user.Email),
			new Claim("UserName", user.UserName),
		};

		foreach (var role in roles)
		{
			claims.Add(new Claim(ClaimTypes.Role, role));
		}

		var chave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("nisjdajsdçajdsdsadsadsasdsadadsaffasfsda"));

		var signingCredentials = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

		var token = new JwtSecurityToken(
			expires: DateTime.Now.AddHours(2),
			claims: claims,
			signingCredentials: signingCredentials
		);

		return new JwtSecurityTokenHandler().WriteToken(token);
	}
}
