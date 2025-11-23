using IA_RoboBerto.Contratos.ContratosRepositorio;
using IA_RoboBerto.Contratos.ContratosServicos;
using IA_RoboBerto.DTOs;
using IA_RoboBerto.Hub;
using IA_RoboBerto.Modelos;
using IA_RoboBerto.Modelos.Paginação;
using Microsoft.AspNetCore.SignalR;

namespace IA_RoboBerto.Servico
{
    public class MensagensServico : IMensagensServico
    {
        private readonly IAuthServico _authService;
        private readonly IChamadoRepositorio _chamadoRepo;
        private readonly IMensagensRepositorio _mensagemRepo;
        private readonly IHubContext<ChatHub, IChatCliente> _hubContext;

        public MensagensServico(IAuthServico authService, IChamadoRepositorio chamadoRepo, IMensagensRepositorio mensagemRepo, IHubContext<ChatHub, IChatCliente> hubContext)
        {
            _authService = authService;
            _chamadoRepo = chamadoRepo;
            _mensagemRepo = mensagemRepo;
            _hubContext = hubContext;
        }

        public async Task<MensagemDTO> ComentarAsync(Chamado chamado,MensagemInsertDTO dto)
        {

            var mensagem = new Mensagem();
            await CopiarDtoPraEntidadeAsync(mensagem, dto);

            // persiste o comentário e obtém o chamado atualizado (com mensagens)
            var chamadoAtualizado = await _chamadoRepo.AdicionarComentarioAsync(chamado, mensagem);

            // pega a mensagem que acabou de ser salva — usando a lista do chamado atualizado
            // assumindo que AdicionarComentarioAsync adiciona a mensagem e atualiza chamado.Mensagens
            var mensagemSalva = chamadoAtualizado.Mensagens
                .OrderByDescending(m => m.DataHoraMensagem)
                .FirstOrDefault(m => m.Id == mensagem.Id) ?? mensagem;

            // opcional: garantir que Autor esteja carregado
            if (mensagemSalva.Autor == null && mensagem.Autor != null)
                mensagemSalva.Autor = mensagem.Autor;

            var mensagemDto = new MensagemDTO(mensagemSalva);

            var nomeDoGrupo = $"chamado-{chamado.Id}";
            await _hubContext.Clients.Group(nomeDoGrupo)
                .ReceberMensagem(chamado.Id, mensagemDto);

            return mensagemDto;
        }

        private async Task CopiarDtoPraEntidadeAsync(Mensagem mensagem, MensagemInsertDTO dto)
        {
            mensagem.Autor = await _authService.ObterUsuarioLogadoAsync();
            mensagem.DataHoraMensagem = DateTime.UtcNow;
            mensagem.Texto = dto.Texto;
        }
    }
}
