using IA_RoboBerto.Contratos.ContratosRepositorio;
using IA_RoboBerto.Contratos.ContratosServicos;
using IA_RoboBerto.DTOs;
using IA_RoboBerto.Modelos;
using IA_RoboBerto.Modelos.Paginação;

namespace IA_RoboBerto.Servico
{
    public class MensagensServico : IMensagensServico
    {
        private readonly IAuthService _authService;
        private readonly IChamadoRepositorio _chamadoRepo;
        private readonly IMensagensRepositorio _mensagemRepo;

        public MensagensServico(IAuthService authService, IChamadoRepositorio chamadoRepo, IMensagensRepositorio mensagemRepo)
        {
            _authService = authService;
            _chamadoRepo = chamadoRepo;
            _mensagemRepo = mensagemRepo;
        }

        public async Task<MensagemDTO> ComentarAsync(Chamado chamado,MensagemInsertDTO dto)
        {
        
            Mensagem mensagem = new Mensagem();
            await CopiarDtoPraEntidadeAsync(mensagem, dto);
            var chamadoAtualizado = await _chamadoRepo.AdicionarComentarioAsync(chamado, mensagem);

            return new MensagemDTO(mensagem);
        }

        private async Task CopiarDtoPraEntidadeAsync(Mensagem mensagem, MensagemInsertDTO dto)
        {
            mensagem.Autor = await _authService.ObterUsuarioLogadoAsync();
            mensagem.DataHoraMensagem = DateTime.UtcNow;
            mensagem.Texto = dto.Texto;
        }
    }
}
