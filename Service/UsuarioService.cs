using AutoMapper;
using Microsoft.AspNetCore.Identity;
using UsuariosAPI.Data.Dtos;
using UsuariosAPI.Models;

namespace UsuariosAPI.Service;

public class UsuarioService
{
    private IMapper _mapper;
    private UserManager<Usuario> _user;
    private SignInManager<Usuario> _signInManager;
    private TokenService _TokenService;
    public UsuarioService(IMapper mapper, UserManager<Usuario> userManager, SignInManager<Usuario> signInManager, TokenService tokenService)
    {
        _mapper = mapper;
        _user = userManager;
        _signInManager = signInManager;
        _TokenService = tokenService;

    }
    /// <summary>
    /// Método assíncrono (async) que retornar uma tarefa (Task), que realizar o cadasto do usuário no sistema.
    /// </summary>
    /// <param name="UsuarioDto"> Dados do usuario do cadastro</param>
    /// <returns> </returns>
    /// <exception cref="ApplicationException"></exception>
    public async Task Cadastramento(CreateUsuarioDto UsuarioDto)
    {
        Usuario usuario = _mapper.Map<Usuario>(UsuarioDto);
        /// Método que faz a chamada CreateAsync para realizar cadastramento com base nos dados fornecidos.
        IdentityResult result = await _user.CreateAsync(usuario, UsuarioDto.Password);
        if (!result.Succeeded)
        {
            throw new ApplicationException("Falha ao criar");
        }
        else
        {
            Console.WriteLine("Usuario cadastrado");
        }
    }

    public async Task<string> loginUsuario(LoginUsuarioDto loginDto)
    {
        /// método que chama PasswordSingAsyn que realiza autenticação com base nas credencias fornecidas.
        var result = await _signInManager.PasswordSignInAsync(loginDto.Username, loginDto.Password, false, false);
        if (!result.Succeeded)
        {
            throw new ApplicationException("falha ao realizar login");
        }
        var usuario = _signInManager.UserManager.Users.FirstOrDefault(x => x.NormalizedUserName == loginDto.Username.ToUpper());

        var token = _TokenService.GenerateToken(usuario);
        return token;

    }

}
