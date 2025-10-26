using IA_RoboBerto.Contratos.ContratosRepositorio;

namespace IA_RoboBerto.Modelos.Paginação
{
    public class PagedList<T> : List<T>
    {

        public IEnumerable<T> Items { get; set; } = new List<T>();
        public int PaginaAtual { get; set; }
        public int TotalPaginas => (int)Math.Ceiling(TotalCount / (double)PaginaTamanho);
        public int PaginaTamanho { get; set;  }
        public int TotalCount { get; set; }

        public PagedList() { }
        public PagedList(IEnumerable<T> items, int paginaAtual, int paginaTamanho, int totalCount)
        {
            Items = Items = items?.ToList() ?? new List<T>();
            PaginaAtual = paginaAtual;
            PaginaTamanho = paginaTamanho;
            TotalCount = totalCount;
            this.AddRange(items);
        }
    }

}
