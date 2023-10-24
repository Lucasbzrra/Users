using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace UsuariosAPI.Controllers;

[ApiController]
[Route("/teste1")]
public class AcessoController:ControllerBase
{
    [HttpGet]
    [Authorize(Policy = "IdadeMinina")] //<= Utilizando a politica de segurança
    public  IActionResult  Get()
    {
        return  Ok("Acesso autorizado");
    }
}
