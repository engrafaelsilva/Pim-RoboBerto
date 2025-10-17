using IA_RoboBerto.Contratos.ContratosServicos;
using IA_RoboBerto.DTOs;
using IA_RoboBerto.Modelos.Paginação;
using Microsoft.AspNetCore.Mvc;

namespace IA_RoboBerto.Controladores
{
    [ApiController]
    [Route("[controller]")]
    public class DepartamentoController : ControllerBase
    {
        private readonly IDepartamentoServico _servico;

        public DepartamentoController(IDepartamentoServico servico)
        {
            _servico = servico;
        }

        // Listar todos com paginação
        [HttpGet]
        public async Task<ActionResult<PagedList<DepartamentoDTO>>> Get([FromQuery] int paginaAtual = 0, [FromQuery] int tamanho = 2)
        {
            var resultado = await _servico.ListarTodosDepComUsuariosAsync(paginaAtual, tamanho);
            return Ok(resultado);
        }

        // Obter por ID
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<DepartamentoDTO>> GetById(Guid id)
        {
            var departamento = await _servico.ObterPorIdAsync(id);
            if (departamento == null) return NotFound();
            return Ok(departamento);
        }

        // Obter por nome
        [HttpGet("nome/{nome}")]
        public async Task<ActionResult<DepartamentoDTO>> GetByNome(string nome)
        {
            var departamento = await _servico.ObterPorNomeAsync(nome);
            if (departamento == null) return NotFound();
            return Ok(departamento);
        }

        // Adicionar novo departamento
        [HttpPost]
        public async Task<ActionResult<DepartamentoDTO>> Post([FromBody] DepartamentoDTO dto)
        {
            var criado = await _servico.AdicionarAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = criado.Id }, criado);
        }

        // Atualizar departamento existente
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<DepartamentoDTO>> Put(Guid id, [FromBody] DepartamentoDTO dto)
        {
            if (id != dto.Id) return BadRequest();
            var atualizado = await _servico.AtualizarAsync(id, dto);
            return Ok(atualizado);
        }

        // Remover departamento
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var sucesso = await _servico.RemoverAsync(id);
            if (!sucesso) return NotFound();
            return NoContent();
        }
    }
}
