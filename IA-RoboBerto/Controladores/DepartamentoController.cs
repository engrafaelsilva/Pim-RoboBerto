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
    public class DepartamentoController : ControllerBase
    {
        private readonly IDepartamentoServico _servico;

        public DepartamentoController(IDepartamentoServico servico)
        {
            _servico = servico;
        }

        [Authorize(Roles = "ADM,COLABORADOR,TECNICO")]
        [HttpGet]
        public async Task<ActionResult<PagedList<DepartamentoDTO>>> Get([FromQuery] int paginaAtual = 0, [FromQuery] int tamanho = 2)
        {
            var resultado = await _servico.ListarTodosDepComUsuariosAsync(paginaAtual, tamanho);
            return Ok(resultado);
        }

        [Authorize(Roles = "ADM,COLABORADOR,TECNICO")]
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<DepartamentoDTO>> GetById(Guid id)
        {
            var departamento = await _servico.ObterPorIdAsync(id);
            if (departamento == null) return NotFound();
            return Ok(departamento);
        }

        [Authorize(Roles = "ADM,COLABORADOR,TECNICO")]
        [HttpGet("nome/{nome}")]
        public async Task<ActionResult<DepartamentoDTO>> GetByNome(string nome)
        {
            var departamento = await _servico.ObterPorNomeAsync(nome);
            if (departamento == null) return NotFound();
            return Ok(departamento);
        }

        [Authorize(Roles = "ADM")]
        [HttpPost]
        public async Task<ActionResult<DepartamentoDTO>> Post([FromBody] DepartamentoDTO dto)
        {
            var criado = await _servico.AdicionarAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = criado.Id }, criado);
        }

        [Authorize(Roles = "ADM")]
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<DepartamentoDTO>> Put(Guid id, [FromBody] DepartamentoDTO dto)
        {
            if (id != dto.Id) return BadRequest();
            var atualizado = await _servico.AtualizarAsync(id, dto);
            return Ok(atualizado);
        }

        [Authorize(Roles = "ADM")]
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var sucesso = await _servico.RemoverAsync(id);
            if (!sucesso) return NotFound();
            return NoContent();
        }
    }
}
