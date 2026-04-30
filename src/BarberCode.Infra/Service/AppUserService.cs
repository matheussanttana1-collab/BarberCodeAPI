using BarberCode.Application.Interfaces;
using BarberCode.Application.Models;
using BarberCode.Domain.Shared;
using BarberCode.Infra.Banco;
using BarberCode.Infra.Models;
using Microsoft.AspNetCore.Identity;

namespace BarberCode.Infra.Service;

public class AppUserService : IAppUserService
{
	private readonly UserManager<AppUser> _userManager;
	private readonly SignInManager<AppUser> _signInManager;

	public AppUserService(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
	{
		_signInManager = signInManager;
		_userManager = userManager;
	}

	/// <summary>
	/// Cadastra um novo usuário no banco de dados
	/// </summary>
	public async Task<ResultData> CadastrarUsuarioAsync(Guid userId,string email,
	string senha, TipoUsuario tipo)
	{
			
		var appUser = new AppUser
		{
			Id = userId.ToString(),
			Email = email,
			UserName = email,
			TipoUsuario = tipo,
		};

		var role = Roles.FromTipoUsuario(tipo);
		var result = await _userManager.CreateAsync(appUser, senha);

		if (!result.Succeeded) 
			return ResultData.Failure(ResultType.Validation, result.Errors.First().Description);


		var roleResult = await _userManager.AddToRoleAsync(appUser, role);

		if (!roleResult.Succeeded)
			return ResultData.Failure(ResultType.Failure, "Erro ao adicionar role");

		return ResultData.Success();
	}

	/// <summary>
	/// Valida se o email existe e retorna o usuário autenticado
	/// Converte AppUser → AuthUser
	/// </summary>
	public async Task<AuthUser?> BuscarPeloEmailAsync(string email)
	{
		var appUser = await _userManager.FindByEmailAsync(email);

		return (appUser is null ?null : MapToAuthUser(appUser)) ;
	}
	public async Task<AuthUser?> BuscarPeloIdAsync(Guid id)
	{
		var appUser = await _userManager.FindByIdAsync(id.ToString());

		return (appUser is null ? null : MapToAuthUser(appUser));
	}

	

	/// <summary>
	/// Valida a senha do usuário
	/// Retorna o usuário se a senha for válida, null caso contrário
	/// </summary>
	public async Task<AuthUser?> ValidarUsuarioAsync(string email, string senha)
	{
		// Busca o AppUser completo pelo ID
		var appUser = await _userManager.FindByEmailAsync(email);
		if (appUser is null)
			return null;

		var result = await _signInManager.CheckPasswordSignInAsync(appUser, senha, false);
		return result.Succeeded ? MapToAuthUser(appUser) : null;
	}

	/// <summary>
	/// Obtém todas as roles (papéis) do usuário
	/// Usa TipoUsuario como fonte de verdade para garantir que sempre retorna a role correta
	/// </summary>
	public async Task<IList<string>> ObterRolesAsync(AuthUser user)
	{
		// TipoUsuario é a fonte de verdade para determinar a role
		var role = Roles.FromTipoUsuario(user.TipoUsuario);
		return new List<string> { role };
	}

	/// <summary>
	/// Cadastra um novo cliente usando apenas celular
	/// Gera email fake: "{celular}@cliente.barbercode.local"
	/// Username = celular (para login do cliente)
	/// </summary>
	public async Task<ResultData> CadastrarClienteAsync(Guid clienteId, string celular, string nome, string? senha = null)
	{
		// Gera email fake baseado no celular
		var emailFake = $"{celular}@cliente.barbercode.local";

		// Se não houver senha, usa uma padrão por enquanto
		var senhaFinal = senha ?? "Cliente@123";

		var appUser = new AppUser
		{
			Id = clienteId.ToString(),
			Email = emailFake,
			UserName = celular, // Username é o celular para o cliente fazer login
			TipoUsuario = TipoUsuario.Cliente,
		};

		var role = Roles.FromTipoUsuario(TipoUsuario.Cliente);
		var result = await _userManager.CreateAsync(appUser, senhaFinal);

		if (!result.Succeeded)
			return ResultData.Failure(ResultType.Validation, result.Errors.First().Description);

		var roleResult = await _userManager.AddToRoleAsync(appUser, role);

		if (!roleResult.Succeeded)
			return ResultData.Failure(ResultType.Failure, "Erro ao adicionar role de cliente");

		return ResultData.Success();
	}

	/// <summary>
	/// Converte AppUser (Infrastructure) para AuthUser (Application)
	/// </summary>
	private static AuthUser MapToAuthUser(AppUser appUser)
	{
		return new AuthUser(
			appUser.Id,
			appUser.Email ?? string.Empty,
			appUser.UserName ?? string.Empty,
			appUser.TipoUsuario
		);
	}


	/// <summary>
	/// Altera a senha do usuário
	/// Assume que as validações já foram feitas no UseCase
	/// </summary>
	public async Task<ResultData> AlterarSenhaAsync(AuthUser user, string token, string novaSenha)
	{
		var appUser = await _userManager.FindByIdAsync(user.Id);

		var result = await _userManager.ResetPasswordAsync(appUser!, token, novaSenha);

		if (!result.Succeeded)
			return ResultData.Failure(ResultType.Validation, result.Errors.First().Description);

		return ResultData.Success();
	}

	/// <summary>
	/// Reseta a senha para uma padrão temporária
	/// Assume que as validações já foram feitas no UseCase
	/// </summary>
	public async Task<string> GerarTokenDeResetSenhaAsync(AuthUser user)
	{
		var Appuser = await _userManager.FindByEmailAsync(user.Email);

		var token = await _userManager.GeneratePasswordResetTokenAsync(Appuser!);

		return token;
	}

	public async Task DeletarUsuarioAsync(Guid id)
	{
		var Appuser = await _userManager.FindByIdAsync(id.ToString());

		await _userManager.DeleteAsync(Appuser!);

	}
}
