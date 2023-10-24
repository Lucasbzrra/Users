using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UsuariosAPI.Models;

namespace UsuariosAPI.Service;

public class TokenService
{
    private IConfiguration _configuration;
    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public string GenerateToken(Usuario usuario)
    {
       
        Claim[] claims = new Claim[] //<=== Claim e unidade de informaçãi de cada usuário que emitida e armazenada
        {
            new Claim("username",usuario.UserName),
            new Claim("id",usuario.Id),
            new Claim(ClaimTypes.DateOfBirth,usuario.DataNascimento.ToString()),
            new Claim("loginTimestamp", DateTime.UtcNow.ToString())
        };

        // Irá passar chave de segurança 
        var chave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SymmetricSecurityKey"])); //<== Utilizando o Secret 
        // irá fazer criptografia do token para fazer assinatura com um token (HmacSha256: é uma função de hash criptográfica que produz um valor de hash de 256 bits. )
        var signingCredentials = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);
        // Nova instância do token com base nas informaçõs necessaris e com prazo de expirar em 10 minutos
        var Token = new JwtSecurityToken
            (
            expires: DateTime.Now.AddMinutes(10),
            claims: claims,
            signingCredentials: signingCredentials
            ) ;
        // Use o JwtSecurityTokenHandler para escrever o token JWT.
        return new JwtSecurityTokenHandler().WriteToken(Token) ;
    }
}
