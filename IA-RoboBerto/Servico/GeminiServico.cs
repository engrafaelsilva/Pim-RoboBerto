using IA_RoboBerto.Contratos.ContratosServicos;
using IA_RoboBerto.Modelos;
using System.Text;
using System.Text.Json;

namespace IA_RoboBerto.Servico
{
    public class GeminiServico : IGeminiServico
    {
        private readonly HttpClient _httpclient;
        private readonly string _apiKey;
        private readonly string _model;

        public GeminiServico(HttpClient httpclient, IConfiguration configuracao)
        {
            _httpclient = httpclient;
            _apiKey = configuracao["Gemini:ApiKey"];
            _model = configuracao["Gemini:Model"];
        }

        public async Task<string> GerarTextoAsync(string nomeUsuario, string descricaoChamado)
        {
            var prompt = $@"
Você é um assistente técnico de Help Desk. Sua função é analisar a queixa do usuário e fornecer uma orientação clara, objetiva e realmente útil.

Diretrizes obrigatórias:
- Comece a resposta dando um cumprimento curto seguido do nome do usuário, e se apresentando como uma IA de Help Desk, tudo em menos de uma linha.
- Em seguida, dê a orientação técnica em forma de texto contínuo, sem quebras de linha.
- A resposta deve ser direta, prática e focada em solução, evitando superficialidade.
- Traga instruções com passos curtos e lógicos, sempre que fizer sentido.
- Não use emojis, negrito, itálico, markdown ou formatação especial.
- Não utilize “barra n” ou quebras de linha.
- pODE USUAR TOPICOS SE VC QUISER.
- Caso a descrição do usuário não tenha relação com suporte técnico, devolva uma resposta curta informando que o assunto não pertence ao escopo técnico do Help Desk.
- Evite respostas longas, mas também não seja raso; priorize objetividade e clareza.

Contexto recebido:
- Nome do usuário: {nomeUsuario}
- Queixa do usuário: {descricaoChamado}

Gere a resposta final agora, seguindo exatamente as regras acima.
";

            var requestBody = new
            {
                contents = new[]
                {
                    new
                    {
                        parts = new[]
                        {
                            new { text = prompt }
                        }
                    }
                }
            };

            var json = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var request = new HttpRequestMessage(HttpMethod.Post, $"https://generativelanguage.googleapis.com/v1beta/models/{_model}:generateContent");

            request.Headers.Add("x-goog-api-key", _apiKey);

            request.Content = content;
            var response = await _httpclient.SendAsync(request);
            var responseBody = await response.Content.ReadAsStringAsync();

            var geminiResponse = JsonSerializer.Deserialize<GeminiResponse>(responseBody);
            var text = geminiResponse?.Candidates?.First()?.Content?.Parts?.First()?.Text;
            return text?.Trim();
        }
    }
}


