using IA_RoboBerto.Contratos.ContratosServicos;
using IA_RoboBerto.DTOs;
using IA_RoboBerto.Modelos;
using IA_RoboBerto.Repositorio;
using IA_RoboBerto.Servico;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IA_RoboBerto.Controladores
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleServico _servico;

        public RoleController(IRoleServico servico)
        {
            _servico = servico;
        }
        [Authorize(Roles = "ADM")]
        [HttpGet]
        public async Task<IEnumerable<RoleDTO>> Get()
        {
            return await _servico.ListarTodosAsync();
        }

        [Authorize(Roles = "ADM")]
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<RoleDTO>> GetById(Guid id)
        {
            var usuario = await _servico.ObterPorIdAsync(id);
            return Ok(usuario);
        }
        [Authorize(Roles = "ADM")]
        [HttpGet("nome/{nome}")]
        public async Task<ActionResult<RoleDTO>> GetByNome(string nome)
        {
            var role = await _servico.ObterPorNomeAsync(nome);
            if (role == null) return NotFound();
            return Ok(role);
        }
    }
}
