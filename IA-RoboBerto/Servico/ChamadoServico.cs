using IA_RoboBerto.Contratos.ContratosRepositorio;
using IA_RoboBerto.Contratos.ContratosServicos;
using IA_RoboBerto.DTOs;
using IA_RoboBerto.Exceções;
using IA_RoboBerto.Modelos;
using IA_RoboBerto.Modelos.Paginação;
using IA_RoboBerto.Models.Enums;
using System.Linq;

namespace IA_RoboBerto.Servico
{
    public class ChamadoServico : IChamadoServico
    {
       // .Include(c => c.Autor)
       // .Include(c => c.Tecnico)
       // .Include(c => c.Categoria)
       // .Include(c => c.Mensagens)
        private readonly IChamadoRepositorio _ChamadoRepo;
        private readonly IUsuarioRepositorio _UsuarioRepo;
        private readonly ICategoriaRepositorio _CategoriaRepo;
        private readonly IMensagensRepositorio _MensagensRepo;

        public ChamadoServico(IChamadoRepositorio chamadoRepo, IUsuarioRepositorio usuarioRepos, ICategoriaRepositorio categoriaRepo, IMensagensRepositorio mensagensRepo)
        {
            _ChamadoRepo = chamadoRepo;
            _UsuarioRepo = usuarioRepos;
            _CategoriaRepo = categoriaRepo;
            _MensagensRepo = mensagensRepo;
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
      public async Task<ChamadoDTO?> ObterPorIdAsync(Guid id)
      {
          var usuario = await _ChamadoRepo.ObterPorIdAsync(id);
          if (usuario == null) throw new ResourceNotFoundException("Recurso não encontrado");
          return new ChamadoDTO(usuario);
      }
     
      public async Task<ChamadoDTO> AbrirChamadoAsync(ChamadoDTO dto)
      {
          var chamado = new Chamado();
          await CopiarDtoPraEntidadeInsert(chamado, dto);

            chamado = await _ChamadoRepo.AdicionarAsync(chamado);
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
     
      public async Task<bool> RemoverAsync(Guid id)
      {
          if (!await _ChamadoRepo.IdExisteAsync(id))
              throw new ResourceNotFoundException("Recurso não encontrado");
     
          return await _ChamadoRepo.RemoverAsync(id);
      }
     
      private async Task CopiarDtoPraEntidadeUpdateUsuario(Chamado chamado, ChamadoDTO dto)
      {

          
            chamado.Categoria = await _CategoriaRepo.ObterPorIdAsync(dto.Autor.Id);
            chamado.Status = dto.Status;
            chamado.Prioridade = dto.Prioridade;

            //chamado.Autor = await _UsuarioRepo.ObterPorIdAsync(dto.Autor.Id);
            //chamado.Tecnico = await _UsuarioRepo.ObterPorIdAsync(dto.Tecnico.Id);
            //chamado.Titulo = dto.Titulo;
            //chamado.SugestaoGemini = dto.SugestaoGemini;
            //chamado.Descricao = dto.Descricao;
            //chamado.DataAbertura = dto.DataAbertura;
            //chamado.DataFechamento = dto.DataFechamento;
            //chamado.SlaVenceEm =  dto.SlaVenceEm;
            //chamado.SugestaoResolveu = dto.SugestaoResolveu;
            //foreach(Mensagem menDto in chamado.Mensagens)
            //{
            //    //Mensagem mensagem = await _MensagensRepo.ob
            //   // chamado.Mensagens.Add(mensagem);
            //}



        }
        private async Task CopiarDtoPraEntidadeInsert(Chamado chamado, ChamadoDTO dto)
        {
            if (dto.Tecnico != null && !string.IsNullOrEmpty(dto.Tecnico.Nome))
            {
                chamado.Tecnico = await _UsuarioRepo.ObterPorNomeAsync(dto.Tecnico.Nome);
                if (chamado == null) throw new ResourceNotFoundException("Recurso não encontrado");
            }
            chamado.Categoria = await _CategoriaRepo.ObterPorNomeAsync(dto.Categoria.Nome);
            chamado.Autor = await _UsuarioRepo.ObterPorNomeAsync(dto.Autor.Nome);


            chamado.Status = dto.Status;
            chamado.Prioridade = dto.Prioridade;
            chamado.Titulo = dto.Titulo;
            chamado.SugestaoGemini = dto.SugestaoGemini;
            chamado.Descricao = dto.Descricao;
            chamado.DataAbertura = dto.DataAbertura;
            chamado.DataFechamento = dto.DataFechamento;
            chamado.SlaVenceEm =  dto.SlaVenceEm;
            chamado.SugestaoResolveu = dto.SugestaoResolveu;
            foreach(Mensagem menDto in chamado.Mensagens)
            {
                Mensagem mensagem = await _MensagensRepo.ObterPorIdAsync(menDto.Id);
                chamado.Mensagens.Add(mensagem);
            }

        }
    }
}
