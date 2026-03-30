using BarberCode.Infra.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace BarberCode.Infra.Service;

public class TokenService
{
	public string GerarToken(AppUser user, IList<string> roles)
	{
		var claims = new List<Claim>()
		{
			new Claim(ClaimTypes.Email, user.Email!),
			new Claim(ClaimTypes.Name, user.UserName!),

			new Claim("BarbeariaId", user.BarbeariaId.ToString() ?? ""),
			new Claim("BarbeiroId", user.BarbeiroId.ToString() ?? ""),
			new Claim("ClienteId", user.ClienteId.ToString() ?? ""),


		};

		foreach (var role in roles)
		{
			claims.Add(new Claim(ClaimTypes.Role, role));
		}


		var chave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("nisjdajsdçajdsdsadsadsa"));

		var signingCredentials = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

		var token = new JwtSecurityToken(
			expires: DateTime.Now.AddHours(2),
			claims: claims,
			signingCredentials: signingCredentials
		);

		return new JwtSecurityTokenHandler().WriteToken(token);
	}
}
