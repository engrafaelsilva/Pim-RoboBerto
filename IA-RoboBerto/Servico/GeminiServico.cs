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
                Você é um assistente virtual de suporte técnico, responsável por oferecer respostas rápidas e assertivas para solicitações de help desk.

                Instruções para sua resposta:
                - Cumprimente o usuário de forma cordial, mencionando seu nome e apresentando-se brevemente como suporte técnico. Exemplo: 'Olá, {nomeUsuario}, aqui é o suporte técnico da equipe.'  
                - Em seguida, explique a possível causa do problema e apresente uma orientação clara e objetiva para solucioná-lo.  
                - Use linguagem profissional, empática e direta — sem termos técnicos desnecessários.  
                - Não utilize emojis, formatações especiais (como **negrito**) ou quebras de linha.  
                - Caso a solicitação não seja relacionada a um tema técnico, responda de forma educada informando que o assunto não faz parte do escopo de suporte.

                Dados do chamado:
                Usuário: {nomeUsuario}
                Descrição: {descricaoChamado}
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


