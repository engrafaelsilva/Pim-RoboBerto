

using IA_RoboBerto.Contratos.ContratosServicos;
using IA_RoboBerto.DTOs;
using IA_RoboBerto.Modelos.Paginação;
using Microsoft.AspNetCore.Mvc;

namespace IA_RoboBerto.Controladores
{

    [ApiController]
    [Route("[controller]")]
    public class ChamadosController : Controller
    {

        private readonly IChamadoServico _servico;

        public ChamadosController(IChamadoServico servico)
        {
            _servico = servico;
        }

        [HttpGet]
        public async Task<ActionResult<PagedList<ChamadoDTO>>> Get([FromQuery] int paginaAtual = 0, [FromQuery] int tamanho = 2)
        {
            var resultado = await _servico.ListarTodosAsync(paginaAtual, tamanho);
            return Ok(resultado);
        }


        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ChamadoDTO>> GetById(Guid id)
        {
            var chamado = await _servico.ObterPorIdAsync(id);
            return Ok(chamado);
        }

        [HttpPost]
        public async Task<ActionResult<ChamadoDTO>> Post([FromBody] ChamadoDTO dto)
        {
            var criado = await _servico.AbrirChamadoAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = criado.Id }, criado);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<ChamadoDTO>> Put(Guid id, [FromBody] ChamadoDTO dto)
        {
            var atualizado = await _servico.AtualizarUsuAsync(id, dto);
            return Ok(atualizado);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _servico.RemoverAsync(id);
            return NoContent();
        }




        //   private readonly ChamadoRepository _repository;
        //   private readonly ApiGeminiService _geminiService;
        //
        //   // Injeção de Dependência
        //   public ChamadosController(ChamadoRepository repository, ApiGeminiService geminiService)
        //   {
        //       _repository = repository;
        //       _geminiService = geminiService;
        //   }
        //
        //   // GET: /Chamados/Index
        //   public IActionResult Index()
        //   {
        //       var chamados = _repository.ObterTodos();
        //       return View(chamados);
        //   }
        //
        //   // GET: /Chamados/Novo
        //   public IActionResult Novo()
        //   {
        //       return View();
        //   }
        //
        //   // POST: /Chamados/AbrirChamado
        //   [HttpPost]
        //// public async Task<IActionResult> AbrirChamado(Chamado chamado)
        //// {
        ////     if (!ModelState.IsValid)
        ////     {
        ////         return View("Novo", chamado);
        ////     }
        ////
        ////     // 1. Gerar resposta da IA
        ////     string prompt = $"Suporte técnico para: {chamado.Descricao}";
        ////     string respostaDaIA = await _geminiService.GerarResposta(prompt);
        ////
        ////     // 2. Preencher e salvar o Chamado
        ////     chamado.RespostaIA = respostaDaIA;
        ////     chamado.DataAbertura = System.DateTime.Now;
        ////     chamado.Resolvido = false;
        ////     chamado.UsuarioId = 1; // Usuário fixo (simplificado)
        ////
        ////     _repository.Adicionar(chamado);
        //
        //     // 3. Redirecionar para os detalhes
        //     return RedirectToAction("Detalhes", new { id = chamado.Id });
        // }
        //
        // // GET: /Chamados/Detalhes/5
        // public IActionResult Detalhes(int id)
        // {
        //     var chamado = _repository.ObterPorId(id);
        //     if (chamado == null)
        //     {
        //         return NotFound();
        //     }
        //     return View(chamado);
        // }
    }
}