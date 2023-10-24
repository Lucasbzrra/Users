using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace UsuariosAPI.Authorization;

public class IdadeAuthorization : AuthorizationHandler<IdadeMinina>//Herdando da classe authorization
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IdadeMinina requirement)
    {
        // Irá utilizar base de dados do context para acessar a tabela "user" e consultar por campo "DateOfBirth"
        var DataNascimentoClaim =context.User.Claims.FirstOrDefault(claim=>claim.Type==ClaimTypes.DateOfBirth);
        //Se não encontrar irá retornar um Task.CompletedTask (representar uma task completa) 
        if (DataNascimentoClaim is null) { return Task.CompletedTask; }
        //Convertando a data nascimento do Claim para datetime
        var DataNascimento=Convert.ToDateTime(DataNascimentoClaim.Value);
        //subtrair a datanascimento dio usuario com data do ano atual
        var idadeUsuario = DateTime.Now.Year - DataNascimento.Year;
        //
        if (DataNascimento > DateTime.Today.AddYears(-idadeUsuario)) { idadeUsuario--; }
        // se a idade do usuario for maior que requerimento passado na politica de acesso
        if (idadeUsuario >= requirement.Idade)
        {
            // rquisito cumprido com sucesso.
             context.Succeed(requirement);
        }
       return Task.CompletedTask;
    }
}
