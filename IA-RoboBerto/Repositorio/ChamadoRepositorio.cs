using IA_RoboBerto.Modelos.Paginação;
using IA_RoboBerto.Modelos;
using IA_RoboBerto.Repositorio.Context;
using Microsoft.EntityFrameworkCore;
using IA_RoboBerto.Contratos.ContratosRepositorio;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using IA_RoboBerto.Models.Enums;

namespace IA_RoboBerto.Repositorio
{
    public class ChamadoRepositorio : IChamadoRepositorio
    {

        private readonly RoboBertoContext _context;


        public ChamadoRepositorio(RoboBertoContext context)
        {
            _context = context;
        }

        public async Task<PagedList<Chamado>> ListarTodosAsync(int paginaAtual, int tamanho)
        {
            var resultado = await _context.Chamado
                .AsNoTracking()
                .Include(c => c.Autor)
                .Include(c => c.Tecnico)
                .Include(c => c.Categoria)
                .Include(c => c.Mensagens)
                    .ThenInclude(m => m.Autor)
                .Skip(tamanho * paginaAtual)
                .Take(tamanho)
                .ToListAsync();
            var totalRegistros = await _context.Departamento.CountAsync();
            var resultadoPaginado = new PagedList<Chamado>(resultado, paginaAtual, tamanho, totalRegistros);
            return resultadoPaginado;
        }


        public async Task<PagedList<Chamado>> ListarMeusChamadosAsync(Guid id, int paginaAtual, int tamanho)
        {
            var resultado = _context.Chamado
                .AsNoTracking()
                .Include(c => c.Autor)
                .Include(c => c.Tecnico)
                .Include(c => c.Categoria)
                .Include(c => c.Mensagens)
                .Where(c => c.Autor.Id == id);

            var totalRegistros = await resultado.CountAsync();

            var itens = await resultado
                .OrderByDescending(c => c.DataAbertura)
                .Skip(paginaAtual * tamanho)
                .Take(tamanho)
                .ToListAsync();
            var resultadoPaginado = new PagedList<Chamado>(itens, paginaAtual, tamanho, totalRegistros);
            return resultadoPaginado;
        }

        public async Task<Chamado?> ObterPorIdAsync(Guid id)
        {
            return await _context.Chamado
                .Include(c => c.Autor)
                .Include(c => c.Tecnico)
                .Include(c => c.Categoria)
                .Include(c => c.Mensagens)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<Chamado> AdicionarAsync(Chamado chamado)
        {
            await _context.Chamado.AddAsync(chamado);
            await _context.SaveChangesAsync();

            return chamado;
        }

        public async Task<Chamado?> AtualizarUsuAsync(Chamado chamado)
        {
            var existente = await _context.Chamado.FindAsync(chamado.Id);
            if (existente == null) return null;

            existente.Categoria = chamado.Categoria;
            existente.Status = chamado.Status;
            existente.Prioridade = chamado.Prioridade;

            await _context.SaveChangesAsync();
            return existente;
        }

        public async Task<bool> RemoverAsync(Guid id)
        {
            var existente = await _context.Chamado.FindAsync(id);
            if (existente == null) return false;

            _context.Chamado.Remove(existente);
            await _context.SaveChangesAsync();
            return true;
        }


        public async Task<bool> IdExisteAsync(Guid id)
        {
            return await _context.Chamado
                .AnyAsync(u => u.Id == id);
        }


        public async Task<Chamado?> AlterarStatusParaCanceladoAsync(Guid id)
        {
            var existente = await ObterPorIdAsync(id);
            existente.Status = EStatusChamado.FECHADO;
            await _context.SaveChangesAsync();
            return existente;
        }
        public async Task<Chamado?> AlterarStatusParaFechadoAsync(Guid id)
        {
            var existente = await ObterPorIdAsync(id);
            existente.Status = EStatusChamado.CANCELADO;
            await _context.SaveChangesAsync();
            return existente;
        }

        public async Task<Chamado?> AtribuirTecnicoAsync(Chamado chamado, Usuario tecnico)
        {
            chamado.Tecnico = tecnico;
            await _context.SaveChangesAsync();
            return chamado;
        }

        public async Task<Chamado?> AlterarResolveuGeminiEStatusAsync(Chamado chamado, bool resolveuSugestao)
        {
            chamado.SugestaoResolveu = resolveuSugestao;
            if (!resolveuSugestao)
            {
                chamado.Status = EStatusChamado.ABERTO;
            }
            else
            {
                chamado.Status = EStatusChamado.FECHADO;
            }

            await _context.SaveChangesAsync();
            return chamado;
        }

        public async Task<Chamado?> AdicionarComentarioAsync(Chamado chamado, Mensagem mensagem)
        {
            var chamadoTracked = await _context.Chamado
                .Include(c => c.Mensagens)
                .FirstOrDefaultAsync(c => c.Id == chamado.Id);
            if (chamadoTracked == null) return null;
            chamadoTracked.Mensagens.Add(mensagem);
            await _context.SaveChangesAsync();
            return chamadoTracked;
        }

        public async Task<Chamado?> ReabrirChamadoAsync(Chamado chamado)
        {
            var existente = await _context.Chamado.FindAsync(chamado.Id);

            existente.Tecnico = null;
            existente.DataFechamento = null;
            existente.DataAbertura = DateTime.UtcNow;
            existente.Status = EStatusChamado.ABERTO;
            // existente.Categoria = chamado.Categoria;
            // existente.Prioridade = chamado.Prioridade;
            //  existente.SlaVenceEm = chamado.SlaVenceEm;
            // existente.Descricao = chamado.Descricao;
            //  existente.SugestaoGemini = chamado.SugestaoGemini;
            await _context.SaveChangesAsync();
            return existente;
        }

        public async Task<List<Chamado>> ListarChamadosAbertoExpiradosAsync()
        {
            return await _context.Chamado
                .Include(c => c.Autor)
                .Include(c => c.Categoria)
                .Include(c => c.Tecnico)
                .Where(c => c.Tecnico == null
                            && c.SlaVenceEm <= DateTime.UtcNow
                            && c.Status == EStatusChamado.ABERTO
                            && c.SugestaoResolveu == false)
                .ToListAsync();
        }
        public async Task<List<Chamado>> ListarChamadosAbertoNaoExpiradosAsync()
        {
            return await _context.Chamado
                .Include(c => c.Autor)
                .Include(c => c.Categoria)
                .Include(c => c.Tecnico)
                .Where(c => c.Tecnico == null
                            && c.Status == EStatusChamado.ABERTO
                            && c.SugestaoResolveu == false)
                .ToListAsync();
        }
    }
}
