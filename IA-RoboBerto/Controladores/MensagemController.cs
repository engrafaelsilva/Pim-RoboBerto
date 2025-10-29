using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using IA_RoboBerto.Contratos.ContratosServicos;
using IA_RoboBerto.DTOs;

namespace IA_RoboBerto.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChamadosController : ControllerBase
    {
        private readonly IChamadoServico _chamadoServico;

        public ChamadosController(IChamadoServico chamadoServico)
        {
            _chamadoServico = chamadoServico;
        }

    //  [HttpPost("{id}/comentarios")]
    //  public async Task<IActionResult> AdicionarComentario(Guid id, [FromBody] MensagemDTO dtoMensagem)
    //  {
    //      var chamadoAtualizado = await _chamadoServico.ComentarNoChamadoAsync(id, dtoMensagem);
    //      if (chamadoAtualizado == null)
    //          return NotFound("Chamado não encontrado.");
    //
    //      return Ok(chamadoAtualizado);
    //  }
    }
}
