using System.ComponentModel.DataAnnotations;

namespace UsuariosAPI.Data.Dtos;

public class CreateUsuarioDto
{
    [Required ]
    public string Username { get; set; }

    public DateTime DataNascimento { get; set; }
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    [Required]
    [Compare("Password")]
    public string Repassword { get; set; }

}
