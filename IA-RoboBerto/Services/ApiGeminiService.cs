// Importa a biblioteca para trabalhar com requisições HTTP (GET, POST, etc.)
using System.Net.Http;
// Importa funcionalidades para manipulação de texto e codificação (UTF8, etc.)
using System.Text;
// Importa a biblioteca para serializar e desserializar JSON no .NET
using System.Text.Json;
// Importa recursos para programação assíncrona (async/await)
using System.Threading.Tasks;

namespace IA_RoboBerto.Services // Define o namespace (organiza a classe dentro do projeto)
{
    // Classe responsável por se comunicar com a API do Gemini
    public class ApiGeminiService
    {
        // Cliente HTTP usado para enviar requisições à API
        private readonly HttpClient _httpClient;
        // Chave da API (lida do appsettings.json)
        private readonly string _apiKey;

        // Construtor da classe: recebe HttpClient e IConfiguration (injeção de dependência)
        public ApiGeminiService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient; // Atribui o HttpClient recebido ao campo interno
            _apiKey = config["Gemini:ApiKey"]; // Busca a chave da API do Gemini no appsettings.json
        }

        // Método assíncrono que envia uma pergunta (prompt) para o Gemini
        public async Task<string> PerguntarGeminiAsync(string prompt)
        {
            // Cria o objeto que será enviado no corpo da requisição
            // Esse formato segue o esperado pela API do Gemini
            var request = new
            {
                contents = new[] {
                    new {
                        role = "user", // Define o papel como "usuário"
                        parts = new[] { new { text = prompt } } // Envia o texto digitado no prompt
                    }
                }
            };

            // Converte o objeto 'request' em JSON e define que o conteúdo é UTF-8 e do tipo "application/json"
            var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

            // Envia a requisição POST para a API do Gemini, passando a chave da API na URL
            var response = await _httpClient.PostAsync(
                $"https://generativelanguage.googleapis.com/v1beta/models/gemini-pro:generateContent?key={_apiKey}",
                content
            );

            // Garante que a resposta seja bem-sucedida (200 OK).
            // Caso contrário, lança uma exceção.
            response.EnsureSuccessStatusCode();

            // Lê o corpo da resposta como string (JSON retornado pelo Gemini)
            var responseString = await response.Content.ReadAsStringAsync();

            // Retorna a resposta em formato de string
            return responseString;
        }
    }
}