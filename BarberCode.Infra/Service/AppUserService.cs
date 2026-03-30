using BarberCode.Application.Interfaces;
using BarberCode.Domain.Shared;
using BarberCode.Infra.Banco;
using BarberCode.Infra.Models;
using Microsoft.AspNetCore.Identity;

namespace BarberCode.Infra.Service;

public class AppUserService : IAppUserRepository
{
	private readonly UserManager<AppUser> _userManager;
	private readonly SignInManager<AppUser> _signInManager;
	private readonly TokenService _tokenService;

	public AppUserService(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, 
	RoleManager<AppUser> roleManager, TokenService tokenService)
	{
		_signInManager = signInManager;
		_userManager = userManager;
		_tokenService = tokenService;
	}


	/// <summary>
	/// Cadastra um novo usuário no banco de dados
	/// </summary>
	/// <param name="userId">ID do usuário (Guid em string)</param>
	/// <param name="email">Email do usuário</param>
	/// <param name="userName">Nome de usuário</param>
	public async Task<ResultData> CadastrarUsuarioAsync(string userId, string email, string userName, 
	string senha)
	{
		var appUser = new AppUser
		{
			Id = userId,
			Email = email,
			UserName = userName,
			PasswordHash = senha
		};

		var result = await _userManager.CreateAsync(appUser);

		if(!result.Succeeded) 
			return ResultData.Failure(ResultType.Validation, result.Errors.First().ToString()!);

		return ResultData.Success();
	}

	
	public async Task<ResultData<string>> Login(string email, string senha)
	{
		var user = await _signInManager.UserManager.FindByEmailAsync(email);
		if (user is null)
			return ResultData<string>.Failure(ResultType.Validation, "Email invalido ou não cadastrado");
		var result = await _signInManager.CheckPasswordSignInAsync(user,senha,false);
		if (!result.Succeeded)
			return ResultData<string>.Failure(ResultType.Validation, "Senha Invalida");

		var roles = await _userManager.GetRolesAsync(user!);

		var token = _tokenService.GerarToken(user, roles);

		return ResultData<string>.Success(token);
	}
	
}
