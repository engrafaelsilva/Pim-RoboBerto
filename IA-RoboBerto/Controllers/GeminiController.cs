using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using IA_RoboBerto.Services;

namespace IA_RoboBerto.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GeminiController : ControllerBase
    {
        private readonly ApiGeminiService _geminiService;

        public GeminiController(ApiGeminiService geminiService)
        {
            _geminiService = geminiService;
        }

        [HttpGet("testar")]
        public async Task<IActionResult> TestarConexao([FromQuery] string pergunta = "Olá, Gemini! Tudo bem?")
        {
            var resposta = await _geminiService.EnviarPerguntaAsync(pergunta);
            return Ok(new { pergunta, resposta });
        }
    }
}