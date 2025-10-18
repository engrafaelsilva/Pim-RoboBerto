using IA_RoboBerto.Contratos.ContratosServicos;
using IA_RoboBerto.DTOs;
using IA_RoboBerto.Modelos;
using IA_RoboBerto.Repositorio;
using IA_RoboBerto.Servico;
using Microsoft.AspNetCore.Mvc;

namespace IA_RoboBerto.Controladores
{
    [ApiController]
    [Route("[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleServico _servico;

        public RoleController(IRoleServico servico)
        {
            _servico = servico;
        }

        [HttpGet]
        public async Task<IEnumerable<RoleDTO>> Get()
        {
            return await _servico.ListarTodosAsync();
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<RoleDTO>> GetById(Guid id)
        {
            var usuario = await _servico.ObterPorIdAsync(id);
            return Ok(usuario);
        }
        [HttpGet("nome/{nome}")]
        public async Task<ActionResult<RoleDTO>> GetByNome(string nome)
        {
            var role = await _servico.ObterPorNomeAsync(nome);
            if (role == null) return NotFound();
            return Ok(role);
        }
    }
}
