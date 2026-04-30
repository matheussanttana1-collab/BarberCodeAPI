using BarberCode.Application.Interfaces;
using BarberCode.Application.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace BarberCode.Infra.Service;

public class TokenService : ITokenService
{
	private readonly IConfiguration _configuration;

	public TokenService(IConfiguration configuration)
	{
		_configuration = configuration;
	}

	public string GerarToken(AuthUser user, IList<string> roles)
	{
		var claims = new List<Claim>()
		{
			new Claim(ClaimTypes.NameIdentifier, user.Id),
			new Claim(ClaimTypes.Email, user.Email),
		};

		foreach (var role in roles)
		{
			claims.Add(new Claim(ClaimTypes.Role, role));
		}

		var chaveSecreta = _configuration["SymmetricSecurityKey"];
		var chave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(chaveSecreta!));

		var signingCredentials = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

		var token = new JwtSecurityToken(
			expires: DateTime.Now.AddMinutes(15),
			claims: claims,
			signingCredentials: signingCredentials
		);

		return new JwtSecurityTokenHandler().WriteToken(token);
	}

	/// <summary>
	/// Valida e regenera um token JWT
	/// Lê os claims do token anterior e gera um novo
	/// </summary>
	public string? RefreshToken(string token)
	{
		try
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			var chaveSecreta = _configuration["SymmetricSecurityKey"];
			var chave = Encoding.UTF8.GetBytes(chaveSecreta!);

			// Valida o token (mesmo se expirado)
			var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
			{
				ValidateIssuerSigningKey = true,
				IssuerSigningKey = new SymmetricSecurityKey(chave),
				ValidateIssuer = false,
				ValidateAudience = false,
				ValidateLifetime = false // Permite tokens expirados
			}, out SecurityToken validatedToken);

			// Extrai os claims do token anterior
			var userId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			var email = principal.FindFirst(ClaimTypes.Email)?.Value;
			var roles = principal.FindAll(ClaimTypes.Role);

			if (userId is null || email is null)
				return null;

			// Gera um novo token com os mesmos claims
			var novosClaims = new List<Claim>
			{
				new Claim(ClaimTypes.NameIdentifier, userId),
				new Claim(ClaimTypes.Email, email),
			};

			foreach (var role in roles)
			{
				novosClaims.Add(new Claim(ClaimTypes.Role, role.Value));
			}

			var chaveSegura = new SymmetricSecurityKey(chave);
			var signingCredentials = new SigningCredentials(chaveSegura, SecurityAlgorithms.HmacSha256);

			var novoToken = new JwtSecurityToken(
				expires: DateTime.Now.AddHours(2),
				claims: novosClaims,
				signingCredentials: signingCredentials
			);

			return new JwtSecurityTokenHandler().WriteToken(novoToken);
		}
		catch
		{
			return null;
		}
	}
}
