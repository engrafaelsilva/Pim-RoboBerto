using IA_RoboBerto.Contratos.ContratosServicos;
using IA_RoboBerto.DTOs;
using IA_RoboBerto.Modelos.Paginação;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IA_RoboBerto.Controladores
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaServico _servico;

        public CategoriaController(ICategoriaServico servico)
        {
            _servico = servico;
        }
        [Authorize(Roles = "ADM")]
        [HttpGet]
        public async Task<ActionResult<PagedList<CategoriaMinDTO>>> Get([FromQuery] int paginaAtual = 0, [FromQuery] int tamanho = 2)
        {
            var resultado = await _servico.ListarTodosAsync(paginaAtual, tamanho);
            return Ok(resultado);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<CategoriaMinDTO>> GetById(Guid id)
        {
            var categoria = await _servico.ObterPorIdAsync(id);
            if (categoria == null) return NotFound();
            return Ok(categoria);
        }

        [HttpGet("nome/{nome}")]
        public async Task<ActionResult<CategoriaMinDTO>> GetByNome(string nome)
        {
            var categoria = await _servico.ObterPorNomeAsync(nome);
            if (categoria == null) return NotFound();
            return Ok(categoria);
        }

        [HttpPost]
        public async Task<ActionResult<CategoriaMinDTO>> Post([FromBody] CategoriaMinDTO dto)
        {
            var criada = await _servico.AdicionarAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = criada.Id }, criada);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<CategoriaMinDTO>> Put(Guid id, [FromBody] CategoriaMinDTO dto)
        {
            if (id != dto.Id) return BadRequest();
            var atualizada = await _servico.AtualizarAsync(id, dto);
            return Ok(atualizada);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var sucesso = await _servico.RemoverAsync(id);
            if (!sucesso) return NotFound();
            return NoContent();
        }
    }
}
