using IA_RoboBerto.Contratos.ContratosRepositorio;
using IA_RoboBerto.Contratos.ContratosServicos;
using IA_RoboBerto.DTOs;
using IA_RoboBerto.Exceções;
using IA_RoboBerto.Modelos;
using IA_RoboBerto.Modelos.Paginação;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace IA_RoboBerto.Servico
{
    public class CategoriaServico : ICategoriaServico
    {
        private readonly ICategoriaRepositorio _repo;
        public CategoriaServico(ICategoriaRepositorio repo)
        {
            _repo = repo;
        }

        public async Task<CategoriaMinDTO> AdicionarAsync(CategoriaMinDTO dto)
        {
            var cat = new Categoria();
            CopiarDtoPraEntidade(cat,dto);
            cat = await _repo.AdicionarAsync(cat);
            return new CategoriaMinDTO(cat);
        }


        public async Task<CategoriaMinDTO?> AtualizarAsync(Guid id, CategoriaMinDTO dto)
        {
            Categoria cat = await _repo.ObterPorIdAsync(id);

            if (cat == null)
            {
                throw new ResourceNotFoundException("Recurso não encontrado");
            }
            CopiarDtoPraEntidade(cat, dto);
            cat = await _repo.AtualizarAsync(cat);
            return new CategoriaMinDTO(cat);
        }

        public async Task<PagedList<CategoriaMinDTO>> ListarTodosAsync(int paginaAtual, int tamanho)
        {
            var resultado = await _repo.ListarTodosAsync(paginaAtual, tamanho);

            var resultadoDTO = resultado.Select(x => new CategoriaMinDTO(x)).ToList();

            return new PagedList<CategoriaMinDTO>(
                resultadoDTO,
                resultado.PaginaAtual,
                resultado.PaginaTamanho,
                resultado.TotalCount
            );
        }
        public async Task<CategoriaMinDTO?> ObterPorNomeAsync(string nome)
        {
            Categoria cat = await _repo.ObterPorNomeAsync(nome);
            if (cat == null)
            {
                throw new ResourceNotFoundException("Recurso não encontrado");
            }

            return new CategoriaMinDTO(cat);
        }

        public async Task<CategoriaMinDTO?> ObterPorIdAsync(Guid id)
        {
            Categoria cat = await _repo.ObterPorIdAsync(id);

            if (cat == null)
            {
                throw new ResourceNotFoundException("Recurso não encontrado");
            }

            return new CategoriaMinDTO(cat);
        }

        public async Task<bool> RemoverAsync(Guid id)
        {
            if(!await _repo.IdExiste(id))
            {
                return false;
                throw new ResourceNotFoundException("Recurso não encontrado");
            }
            try
            {
                await _repo.RemoverAsync(id);
            } catch(DbUpdateException ex) when(ex.InnerException is PostgresException pgEx && pgEx.SqlState == "23503")
            {
                return false;
                throw new DataBaseException("Falha de integridade referencial");
            }

            return true;
        }


        private void CopiarDtoPraEntidade(Categoria categoria, CategoriaMinDTO dto)
        {
            categoria.Id = dto.Id;
            categoria.Nome = dto.Nome;
        }
    }
}
