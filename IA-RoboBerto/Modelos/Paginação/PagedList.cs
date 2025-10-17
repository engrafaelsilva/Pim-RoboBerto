using IA_RoboBerto.Contratos.ContratosRepositorio;

namespace IA_RoboBerto.Modelos.Paginação
{
    public class PagedList<T> : List<T>
    {

        public IEnumerable<T> Items { get; }
        public int PaginaAtual { get; }
        public int TotalPaginas => (int)Math.Ceiling(TotalCount / (double)PaginaTamanho);
        public int PaginaTamanho { get; }
        public int TotalCount { get; }

        public PagedList(IEnumerable<T> items, int paginaAtual, int paginaTamanho, int totalCount)
        {
            Items = items;
            PaginaAtual = paginaAtual;
            PaginaTamanho = paginaTamanho;
            TotalCount = totalCount;
            this.AddRange(items);
        }
    }

}
