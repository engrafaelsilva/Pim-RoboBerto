using IA_RoboBerto.Contratos.ContratosRepositorio;
using IA_RoboBerto.Contratos.ContratosServicos;
using IA_RoboBerto.DTOs;
using IA_RoboBerto.Modelos;
using IA_RoboBerto.Modelos.Paginação;
using IA_RoboBerto.Repositorio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IA_RoboBerto.Controladores
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioServico _servico;

        public UsuarioController(IUsuarioServico servico)
        {
            _servico = servico;
        }

        [Authorize(Roles = "ADM")]
        [HttpGet]
        public async Task<ActionResult<PagedList<UsuarioMaxDTO>>> Get([FromQuery] int paginaAtual = 0, [FromQuery] int tamanho = 2)
        {
            var resultado = await _servico.ListarTodosAsync(paginaAtual, tamanho);
            return Ok(resultado);
        }


        [Authorize(Roles = "ADM")]
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<UsuarioMaxDTO>> GetById(Guid id)
        {
            var usuario = await _servico.ObterPorIdAsync(id);
            return Ok(usuario);
        }


       [Authorize(Roles = "ADM,TECNICO,COLABORADOR")]
        [HttpGet("eu")]
        public async Task<ActionResult<UsuarioMaxDTO>> GetEu()
        {
            var usuario = await _servico.ObterEuAsync();
            return Ok(usuario);
        }



        [HttpPost]
        public async Task<ActionResult<UsuarioInsertDTO>> Post([FromBody] UsuarioInsertDTO dto)
        {
            var criado = await _servico.AdicionarAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = criado.Id }, criado);
        }

        [Authorize(Roles = "ADM")]
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<UsuarioMaxDTO>> Put(Guid id, [FromBody] UsuarioMaxDTO dto)
        {
            var atualizado = await _servico.AtualizarAsync(id, dto);
            return Ok(atualizado);
        }

        [Authorize(Roles = "ADM")]
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _servico.RemoverAsync(id);
            return NoContent();
        }


    }
}
