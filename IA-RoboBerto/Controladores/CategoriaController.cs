using IA_RoboBerto.Contratos.ContratosServicos;
using IA_RoboBerto.DTOs;
using IA_RoboBerto.Modelos;
using IA_RoboBerto.Modelos.Paginação;
using Microsoft.AspNetCore.Mvc;

namespace IA_RoboBerto.Controladores
{

    [ApiController]
    [Route("[controller]")]
    public class CategoriaController : Controller
    {
        private readonly ICategoriaServico _servico;

        public CategoriaController(ICategoriaServico servico)
        {
            _servico = servico;
        }

        [HttpGet]
        public ActionResult<PagedList<CategoriaDTO>> Get([FromQuery] int paginaAtual = 0, [FromQuery] int tamanho = 2)
        {
            var resultado = _servico.ListarTodos(paginaAtual, tamanho);
            return Ok(resultado);

        }
    }
}
