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
    public class DepartamentoServico : IDepartamentoServico
    {
        private readonly IDepartamentoRepositorio _repo;

        public DepartamentoServico(IDepartamentoRepositorio repo)
        {
            _repo = repo;
        }

        public async Task<PagedList<DepartamentoDTO>> ListarTodosDepComUsuariosAsync(int paginaAtual, int tamanho)
        {
            var resultado = await _repo.ListarTodosDepComUsuariosAsync(paginaAtual, tamanho);

            var resultadoDTO = resultado.Select(x => new DepartamentoDTO(x)).ToList();

            return new PagedList<DepartamentoDTO>(
                resultadoDTO,
                resultado.PaginaAtual,
                resultado.PaginaTamanho,
                resultado.TotalCount
            );
        }

        public async Task<DepartamentoDTO> AdicionarAsync(DepartamentoDTO dto)
        {
            var dep = new Departamento();
            CopiarDtoPraEntidade(dep, dto);

            dep = await _repo.AdicionarAsync(dep);
            return new DepartamentoDTO(dep);
        }

        public async Task<DepartamentoDTO?> AtualizarAsync(Guid id, DepartamentoDTO dto)
        {
            var dep = await _repo.ObterPorIdAsync(id);
            if (dep == null)
            {
                throw new ResourceNotFoundException("Recurso não encontrado");
            }

            CopiarDtoPraEntidade(dep, dto);
            dep = await _repo.AtualizarAsync(dep);
            return new DepartamentoDTO(dep);
        }

        public async Task<DepartamentoDTO?> ObterPorNomeAsync(string nome)
        {
            var dep = await _repo.ObterPorNomeAsync(nome);
            if (dep == null)
            {
                throw new ResourceNotFoundException("Recurso não encontrado");
            }

            return new DepartamentoDTO(dep);
        }

        public async Task<DepartamentoDTO?> ObterPorIdAsync(Guid id)
        {
            var dep = await _repo.ObterPorIdAsync(id);
            if (dep == null)
            {
                throw new ResourceNotFoundException("Recurso não encontrado");
            }

            return new DepartamentoDTO(dep);
        }

        public async Task<bool> RemoverAsync(Guid id)
        {
            if (!await _repo.IdExisteAsync(id))
            {
                return false;
                throw new ResourceNotFoundException("Recurso não encontrado");
            }

            try
            {
                await _repo.RemoverAsync(id);
            }
            catch (DbUpdateException ex) when (ex.InnerException is PostgresException pgEx && pgEx.SqlState == "23503")
            {
                return false;
                throw new DataBaseException("Falha de integridade referencial");
            }

            return true;
        }

        private void CopiarDtoPraEntidade(Departamento departamento, DepartamentoDTO dto)
        {
            departamento.Id = dto.Id;
            departamento.Nome = dto.Nome;

        }
    }
}
