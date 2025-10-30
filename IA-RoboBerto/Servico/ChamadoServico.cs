using IA_RoboBerto.Contratos.ContratosRepositorio;
using IA_RoboBerto.Contratos.ContratosServicos;
using IA_RoboBerto.DTOs;
using IA_RoboBerto.Exceções;
using IA_RoboBerto.Modelos;
using IA_RoboBerto.Modelos.Paginação;
using IA_RoboBerto.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System.Linq;

namespace IA_RoboBerto.Servico
{
    public class ChamadoServico : IChamadoServico
    {
        private readonly IChamadoRepositorio _ChamadoRepo;
        private readonly IUsuarioRepositorio _UsuarioRepo;
        private readonly ICategoriaRepositorio _CategoriaRepo;
        private readonly IAuthServico _authService;
        private readonly ISLAServico _slaService;
        private readonly IGeminiServico _geminiServico;
        private readonly IMensagensServico _mensagemServico;

        public ChamadoServico(IChamadoRepositorio chamadoRepo, IUsuarioRepositorio usuarioRepos, ICategoriaRepositorio categoriaRepo, IMensagensRepositorio mensagensRepo, IAuthServico authService, ISLAServico slaService, IGeminiServico geminiServico, IMensagensServico mensagemServico)
        {
            _ChamadoRepo = chamadoRepo;
            _UsuarioRepo = usuarioRepos;
            _CategoriaRepo = categoriaRepo;
            _mensagemServico = mensagemServico;
            _authService = authService;
            _slaService = slaService;
            _geminiServico = geminiServico;
            _mensagemServico = mensagemServico;
        }

        public async Task<PagedList<ChamadoDTO>> ListarTodosAsync(int paginaAtual, int tamanho)
        {
            var resultado = await _ChamadoRepo.ListarTodosAsync(paginaAtual, tamanho);

            var resultadoDTO = resultado.Select(x => new ChamadoDTO(x)).ToList();

            return new PagedList<ChamadoDTO>(
                resultadoDTO,
                resultado.PaginaAtual,
                resultado.PaginaTamanho,
                resultado.TotalCount
            );
        }

        public async Task<List<ChamadoDTO>> ListarChamadosPendentesTecnicosExpiradosAsync()
        {
            var resultado = await _ChamadoRepo.ListarChamadosPendentesTecnicosExpiradosAsync();

            var resultadoDTO = resultado.Select(x => new ChamadoDTO(x)).ToList();

            return resultadoDTO;
        }
        public async Task<List<ChamadoDTO>> ListarChamadosPendentesTecnicosNaoExpiradosAsync()
        {
            var resultado = await _ChamadoRepo.ListarChamadosPendentesTecnicosNaoExpiradosAsync();

            var resultadoDTO = resultado.Select(x => new ChamadoDTO(x)).ToList();

            return resultadoDTO;
        }

        public async Task<PagedList<ChamadoDTO>> ListarMeusChamadosAsync(int paginaAtual, int tamanho)
        {
            var usuarioAutenticado = await _authService.ObterUsuarioLogadoAsync();

            var resultado = await _ChamadoRepo.ListarMeusChamadosAsync(usuarioAutenticado.Id, paginaAtual, tamanho);

            var resultadoDTO = resultado.Select(x => new ChamadoDTO(x)).ToList();

            return new PagedList<ChamadoDTO>(
                resultadoDTO,
                resultado.PaginaAtual,
                resultado.PaginaTamanho,
                resultado.TotalCount
            );
        }
        public async Task<ChamadoDTO?> ObterPorIdAsync(Guid id)
        {
            var usuario = await _ChamadoRepo.ObterPorIdAsync(id);
            if (usuario == null) throw new ResourceNotFoundException("Recurso não encontrado");
            return new ChamadoDTO(usuario);
        }

        public async Task<ChamadoDTO> AbrirChamadoAsync(ChamadoInsertDTO dto)
        {
            var chamado = new Chamado();
            await CopiarDtoPraEntidadeInsert(chamado, dto);

            chamado = await _ChamadoRepo.AdicionarAsync(chamado);
            return new ChamadoDTO(chamado);
        }

        public async Task<ChamadoDTO> AtribuirTecnicoAsync(Guid chamadoId)
        {
            var chamado = await _ChamadoRepo.ObterPorIdAsync(chamadoId);
            var usuarioAutenticado = await _authService.ObterUsuarioLogadoAsync();
            chamado.Tecnico = usuarioAutenticado;
            return new ChamadoDTO(chamado);
        }

        public async Task<ChamadoDTO> AlterarSugestaoResolveuEStatusAsync(Guid chamadoId, bool sugestaoResolveu)
        {
            var chamado = await _ChamadoRepo.ObterPorIdAsync(chamadoId);
            chamado = await _ChamadoRepo.AlterarResolveuGeminiEStatusAsync(chamado, sugestaoResolveu);
            return new ChamadoDTO(chamado);
        }

        public async Task<ChamadoDTO?> AtualizarUsuAsync(Guid id, ChamadoDTO dto)
        {
            var chamado = await _ChamadoRepo.ObterPorIdAsync(id);
            if (chamado == null) throw new ResourceNotFoundException("Recurso não encontrado");

            await CopiarDtoPraEntidadeUpdateUsuario(chamado, dto);

            var atualizado = await _ChamadoRepo.AtualizarUsuAsync(chamado);

            return new ChamadoDTO(atualizado);
        }

        public async Task<ChamadoDTO?> CancelarChamadoAsync(Guid id)
        {
            var atualizado = await _ChamadoRepo.AlterarStatusParaCanceladoAsync(id);
            return new ChamadoDTO(atualizado);
        }

        public async Task<ChamadoDTO?> FecharChamadoAsync(Guid id)
        {
            var atualizado = await _ChamadoRepo.AlterarStatusParaFechadoAsync(id);
            return new ChamadoDTO(atualizado);
        }
        public async Task<ChamadoDTO?> ComentarNoChamadoAsync(Guid id,MensagemInsertDTO dtoMensagem)
        {
            var chamado = await _ChamadoRepo.ObterPorIdAsync(id);
            await _mensagemServico.ComentarAsync(chamado, dtoMensagem);
            var chamadoAtualizado = await _ChamadoRepo.ObterPorIdAsync(id);
            return new ChamadoDTO(chamadoAtualizado);
        }
        public async Task<ChamadoDTO?> ReabrirChamadoAsync(Guid id)
        {
            var chamado = await _ChamadoRepo.ObterPorIdAsync(id);
            if (chamado.Status != EStatusChamado.CANCELADO) throw new BadHttpRequestException("O chamado deve estar fechado para reabri-lo");
            var atualizado = await _ChamadoRepo.ReabrirChamadoAsync(chamado);
            chamado.SlaVenceEm = _slaService.CalcularSLA(atualizado);
            return new ChamadoDTO(atualizado);
        }

        public async Task<bool> RemoverAsync(Guid id)
        {
            if (!await _ChamadoRepo.IdExisteAsync(id))
                throw new ResourceNotFoundException("Recurso não encontrado");

            try
            {
                await _ChamadoRepo.RemoverAsync(id);
            }
            catch (DbUpdateException ex) when (ex.InnerException is PostgresException pgEx && pgEx.SqlState == "23503")
            {
                throw new DataBaseException("Falha de integridade referencial");
            }
            return true;
        }

        private async Task CopiarDtoPraEntidadeUpdateUsuario(Chamado chamado, ChamadoDTO dto)
        {

            chamado.Categoria = await _CategoriaRepo.ObterPorNomeAsync(dto.Categoria.Nome);
            if (chamado.Categoria == null) throw new ResourceNotFoundException("Recurso não encontrado");
            chamado.Status = dto.Status;
            chamado.Prioridade = dto.Prioridade;

        }

        private async Task CopiarDtoPraEntidadeInsert(Chamado chamado, ChamadoInsertDTO dto)
        {
    
            chamado.Categoria = await _CategoriaRepo.ObterPorNomeAsync(dto.Categoria.Nome);
            if (chamado.Categoria == null) throw new ResourceNotFoundException("Recurso não encontrado");

            chamado.Autor = await _authService.ObterUsuarioLogadoAsync();
            chamado.Descricao = dto.Descricao;
            chamado.Status = EStatusChamado.SUGESTAO_GERADA;
            chamado.Prioridade = dto.Prioridade;
            chamado.Titulo = dto.Titulo;
            chamado.SugestaoGemini = await _geminiServico.GerarTextoAsync(chamado.Autor.Nome, dto.Descricao);
            chamado.DataAbertura = DateTime.UtcNow;
            chamado.SlaVenceEm = _slaService.CalcularSLA(chamado);
            chamado.SugestaoResolveu = null;
        }
    }
}
