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
        public class DepartamentoController : ControllerBase
        {
            private readonly IDepartamentoServico _servico;

            public DepartamentoController(IDepartamentoServico servico)
            {
                _servico = servico;
            }

            [HttpGet]
            public async Task<ActionResult<PagedList<DepartamentoDTO>>> Get([FromQuery]int paginaAtual = 0, [FromQuery] int tamanho = 2)
            {
            var resultado = await _servico.ListarTodosDepComUsuariosAsync(paginaAtual, tamanho);
            return Ok(resultado);

        }
    }
}
