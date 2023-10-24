using System.ComponentModel.DataAnnotations;

namespace UsuariosAPI.Data.Dtos;

public class LoginUsuarioDto
{
    /// <summary>
    /// Dados do login
    /// </summary>
    [Required]
    public  string Username { get; set; }

    [Required] 
    public string Password { get;set; }
}
