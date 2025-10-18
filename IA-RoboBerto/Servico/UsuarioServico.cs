using IA_RoboBerto.Contratos.ContratosRepositorio;
using IA_RoboBerto.Contratos.ContratosServicos;
using IA_RoboBerto.DTOs;
using IA_RoboBerto.Exceções;
using IA_RoboBerto.Modelos;
using IA_RoboBerto.Modelos.Paginação;
using IA_RoboBerto.Repositorio;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace IA_RoboBerto.Servico
{
    public class UsuarioServico : IUsuarioServico
    {
        private readonly IUsuarioRepositorio _UsuarioRepo;
        private readonly IDepartamentoRepositorio _DepartamentoRepo;
        private readonly IRoleRepositorio _RoleRepo;
        public UsuarioServico(IUsuarioRepositorio usuRepo, IDepartamentoRepositorio departamentoRepo, IRoleRepositorio roleRepo)
        {
            _UsuarioRepo = usuRepo;
            _DepartamentoRepo = departamentoRepo;
            _RoleRepo = roleRepo;
        }

      public async Task<PagedList<UsuarioMaxDTO>> ListarTodosAsync(int paginaAtual, int tamanho)
        {
            var resultado = await _UsuarioRepo.ListarTodosAsync(paginaAtual, tamanho);

            var resultadoDTO = resultado.Select(x => new UsuarioMaxDTO(x)).ToList();
            return new PagedList<UsuarioMaxDTO>(
                resultadoDTO,
                resultado.PaginaAtual,
                resultado.PaginaTamanho,
                resultado.TotalCount
            );
        }
        public async Task<UsuarioMaxDTO?> ObterPorIdAsync(Guid id)
        {
            var usuario = await _UsuarioRepo.ObterPorIdAsync(id);
            if (usuario == null) throw new ResourceNotFoundException("Recurso não encontrado");
            return new UsuarioMaxDTO(usuario);
        }

        public async Task<UsuarioInsertDTO> AdicionarAsync(UsuarioInsertDTO dto)
        {
            var usuario = new Usuario();
           await CopiarDtoPraEntidadeInsert(usuario, dto);

            usuario = await _UsuarioRepo.AdicionarAsync(usuario);
            return new UsuarioInsertDTO(usuario);
        }

        public async Task<UsuarioMaxDTO?> AtualizarAsync(Guid id, UsuarioMaxDTO dto)
        {
            var usuario = await _UsuarioRepo.ObterPorIdAsync(id);
            if (usuario == null) throw new ResourceNotFoundException("Recurso não encontrado");

            await CopiarDtoPraEntidadeUpdate(usuario, dto);

            var atualizado = await _UsuarioRepo.AtualizarAsync(usuario);

            return new UsuarioMaxDTO(atualizado);
        }
        public async Task<bool> RemoverAsync(Guid id)
        {
            if (!await _UsuarioRepo.IdExisteAsync(id))
                throw new ResourceNotFoundException("Recurso não encontrado");

            try
            {

                await _UsuarioRepo.RemoverAsync(id);
            }
            catch (DbUpdateException ex) when (ex.InnerException is PostgresException pgEx && pgEx.SqlState == "23503")
            {
                throw new DataBaseException("Falha de integridade referencial");
            }
            return true;
        }

        private async Task CopiarDtoPraEntidadeUpdate(Usuario usuario, UsuarioMaxDTO dto)
        {
            var departamento = await _DepartamentoRepo.ObterPorNomeAsync(dto.Departamento.Nome);

            usuario.Nome = dto.Nome;
            usuario.Email = dto.Email;
            usuario.Telefone = dto.Telefone;
            usuario.Departamento = departamento;

        }
        private async Task CopiarDtoPraEntidadeInsert(Usuario usuario, UsuarioInsertDTO dto)
        {
            usuario.Nome = dto.Nome;
            usuario.Email = dto.Email;
            usuario.Telefone = dto.Telefone;
            usuario.DataCriacao = DateTime.UtcNow;
            usuario.SenhaHash = dto.SenhaHash;

            usuario.Departamento = await _DepartamentoRepo.ObterPorNomeAsync(dto.Departamento.Nome);


                foreach (var roleDto in dto.Roles)
                {
                    var role = await _RoleRepo.ObterPorNomeAsync(roleDto.Nome);
                        usuario.Roles.Add(role);
            }
        }
    }
}
