

using IA_RoboBerto.Contratos.ContratosServicos;
using IA_RoboBerto.DTOs;
using IA_RoboBerto.Modelos.Paginação;
using IA_RoboBerto.Models.Enums;
using IA_RoboBerto.Servico;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace IA_RoboBerto.Controladores
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ChamadosController : Controller
    {

        private readonly IChamadoServico _servico;

        public ChamadosController(IChamadoServico servico)
        {
            _servico = servico;
        }

        [Authorize(Roles = "ADM,TECNICO")]
        [HttpGet]
        public async Task<ActionResult<PagedList<ChamadoDTO>>> Get([FromQuery] int paginaAtual = 0, [FromQuery] int tamanho = 2)
        {
            var resultado = await _servico.ListarTodosAsync(paginaAtual, tamanho);
            return Ok(resultado);
        }

        [Authorize(Roles = "ADM")]
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ChamadoDTO>> GetById(Guid id)
        {
            var chamado = await _servico.ObterPorIdAsync(id);
            return Ok(chamado);
        }

        [Authorize(Roles = "ADM,TECNICO,COLABORADOR")]
        [Authorize]
        [HttpGet("meus")]
        public async Task<ActionResult<PagedList<ChamadoDTO>>> MeusChamados([FromQuery] int paginaAtual = 0, [FromQuery] int tamanho = 10)
        {
         
            var resultado = await _servico.ListarMeusChamadosAsync(paginaAtual, tamanho);
            return Ok(resultado);
        }

        [Authorize(Roles = "ADM,TECNICO")]
        [Authorize]
        [HttpGet("expirados")]
        public async Task<ActionResult<List<ChamadoDTO>>> ListarChamadosSLAExpirado()
        {
            var resultado = await _servico.ListarChamadosSlaExpiradoAsync();
            return Ok(resultado);
        }

        [Authorize(Roles = "COLABORADOR,TECNICO,ADM")]
        [HttpPost]
        public async Task<ActionResult<ChamadoDTO>> Post([FromBody] ChamadoInsertDTO dto)
        {
            var criado = await _servico.AbrirChamadoAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = criado.Id }, criado);
        }
        [Authorize(Roles = "ADM,COLABORADOR")]

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<ChamadoDTO>> Put(Guid id, [FromBody] ChamadoDTO dto)
        {
            var atualizado = await _servico.AtualizarUsuAsync(id, dto);
            return Ok(atualizado);
        }
        [Authorize(Roles = "ADM")]
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _servico.RemoverAsync(id);
            return NoContent();
        }

        [Authorize(Roles = "ADM,COLABORADOR")]
        [HttpPatch("{id:guid}/cancelar")] 
        public async Task<ActionResult<ChamadoDTO>> CancelarChamado(Guid id)
        {
            var atualizado = await _servico.CancelarChamadoAsync(id);

            return Ok(atualizado); 
        }

        [Authorize(Roles = "TECNICO")]
        [HttpPatch("{id:guid}/acatar-chamado")]
        public async Task<ActionResult<ChamadoDTO>> AtribuirTecnicoAsync(Guid id)
        {
            var atualizado = await _servico.AtribuirTecnicoAsync(id);
            return Ok(atualizado);
        }


        [Authorize(Roles = "ADM,COLABORADOR")]
        [HttpPatch("{id:guid}/reabrir")]
        public async Task<ActionResult<ChamadoDTO>> ReabrirChamado(Guid id)
        {
            var atualizado = await _servico.ReabrirChamadoAsync(id);

            return Ok(atualizado); 
        }

        [Authorize(Roles = "ADM,COLABORADOR,TECNICO")]
        [HttpPatch("{id:guid}/comentar")]
        public async Task<ActionResult<ChamadoDTO>> AdicionarComentario(Guid id, [FromBody] MensagemInsertDTO dtoMensagem)
        {
            var chamadoAtualizado = await _servico.ComentarNoChamadoAsync(id, dtoMensagem);

            return Ok(chamadoAtualizado);
        }


    }
}