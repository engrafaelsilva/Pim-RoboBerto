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

        public async Task<string> GerarTextoAsync(string descricaoChamado)
        {
            var prompt = $@"
                Você atua no setor de help desk. Sua função é receber a queixa do usuário e fornecer uma solução para o problema apresentado. A resposta deve ser curta, direta e eficiente, para que o usuário consiga tentar resolver por conta própria, por fim, sem emojis e asteriscos que representem negrito. Comece a mensagem sendo direto se apresntando de forma bem curta, menos de uma linha e apresente a solução. Queixa do usuário: {descricaoChamado} ";

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


