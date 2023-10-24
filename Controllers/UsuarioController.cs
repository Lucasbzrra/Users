using Microsoft.AspNetCore.Mvc;
using UsuariosAPI.Data.Dtos;
using UsuariosAPI.Service;

namespace UsuariosAPI.Controllers;

[ApiController]
[Route("/controller")]

public class UsuarioController: ControllerBase
{
    private UsuarioService _UsuarioService;
    public UsuarioController(UsuarioService usuarioService)
    {
        _UsuarioService = usuarioService;

    }
    /// <summary>
    /// Método que realiza o cadastramento do usuario
    /// </summary>
    /// <param name="dto"> Dados da criação do usuario</param>
    /// <returns> </returns>
    [HttpPost("/Cadastro")]
    public async Task<IActionResult> CadastrarUsuario(CreateUsuarioDto dto)
    {
        await _UsuarioService.Cadastramento(dto);
        return Ok("Usuario Cadastrado");
      
    }/// <summary>
    /// Método que realiza o login do usuario no sistema 
    /// </summary>
    /// <param name="loginDto"> dados do login </param>
    /// <returns></returns>
    [HttpPost("/Login")]
    public  async  Task <IActionResult> Login(LoginUsuarioDto loginDto)
    {
        
        var token=await _UsuarioService.loginUsuario(loginDto);
        
        return Ok(token);

    }
}
