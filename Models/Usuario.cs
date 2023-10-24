using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace UsuariosAPI.Models;

public class Usuario:IdentityUser
{
	public DateTime DataNascimento { get; set; }
	public Usuario():base() { } //<= puxa todos dados da classe base IdentityUser
}
