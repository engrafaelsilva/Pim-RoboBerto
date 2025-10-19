using IA_RoboBerto.Autenticação;
using IA_RoboBerto.Contratos.ContratosServicos;
using IA_RoboBerto.Modelos;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks.Dataflow;

namespace IA_RoboBerto.Servico
{
    public class TokenServico : ITokenServico
    {

        public string GerarToken(Usuario usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Configuracoes.Secret);
            var claims = new List<Claim>
            {
                 new Claim(ClaimTypes.Name, usuario.Nome)
            };

            foreach(var role in usuario.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.Nome));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(8),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
