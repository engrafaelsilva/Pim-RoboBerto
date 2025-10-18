using IA_RoboBerto.Contratos.ContratosRepositorio;
using IA_RoboBerto.Contratos.ContratosServicos;
using IA_RoboBerto.DTOs;
using IA_RoboBerto.Modelos;
using IA_RoboBerto.Modelos.Paginação;
using IA_RoboBerto.Repositorio;
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

        [HttpGet]
        public async Task<ActionResult<PagedList<UsuarioMaxDTO>>> Get([FromQuery] int paginaAtual = 0, [FromQuery] int tamanho = 2)
        {
            var resultado = await _servico.ListarTodosAsync(paginaAtual, tamanho);
            return Ok(resultado);
        }

       
           [HttpGet("{id:guid}")]
           public async Task<ActionResult<UsuarioMaxDTO>> GetById(Guid id)
           {
               var usuario = await _servico.ObterPorIdAsync(id);
               return Ok(usuario);
           }
        
           [HttpPost]
           public async Task<ActionResult<UsuarioInsertDTO>> Post([FromBody] UsuarioInsertDTO dto)
           {
               var criado = await _servico.AdicionarAsync(dto);
               return CreatedAtAction(nameof(GetById), new { id = criado.Id }, criado);
           }
        
           [HttpPut("{id:guid}")]
           public async Task<ActionResult<UsuarioMaxDTO>> Put(Guid id, [FromBody] UsuarioMaxDTO dto)
           {
               var atualizado = await _servico.AtualizarAsync(id, dto);
               return Ok(atualizado);
           }
        
           [HttpDelete("{id:guid}")]
           public async Task<ActionResult> Delete(Guid id)
           {
               await _servico.RemoverAsync(id);
               return NoContent();
           }


    }
}
