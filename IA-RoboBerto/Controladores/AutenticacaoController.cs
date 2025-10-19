using IA_RoboBerto.Contratos.ContratosServicos;
using IA_RoboBerto.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace IA_RoboBerto.Controladores
{

    [ApiController]
    [Route("[controller]")]
    public class AutenticacaoController : Controller
    {

        private readonly IUsuarioServico _usuarioServico;
        private readonly ITokenServico _tokenServico;

        public AutenticacaoController(IUsuarioServico usuarioServico, ITokenServico tokenServico)
        {
            _usuarioServico = usuarioServico;
            _tokenServico = tokenServico;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO dto)
        {

            var usuario = await _usuarioServico.ValidarUsuarioAsync(dto.Email,dto.Senha);

            var token = _tokenServico.GerarToken(usuario);
            return Ok(new { token });
        }

    }
}
