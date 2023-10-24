using Microsoft.AspNetCore.Authorization;

namespace UsuariosAPI.Authorization;

public class IdadeMinina : IAuthorizationRequirement //<=== Implementando a interface para na classe "Program", fazer a politica de segurança.
{
    public IdadeMinina(int idade)
    {
        Idade = idade;
    }
    public int Idade { get; set; }
}
  
