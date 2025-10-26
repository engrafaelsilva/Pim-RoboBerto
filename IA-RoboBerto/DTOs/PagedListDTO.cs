using IA_RoboBerto.Modelos.Paginação;

namespace IA_RoboBerto.DTOs
{

        public class PagedListDTO<T>
        {
            public List<T> Items { get; set; }
            public int PaginaAtual { get; set; }
            public int TotalPaginas { get; set; }
            public int PaginaTamanho { get; set; }
            public int TotalCount { get; set; }

            public PagedListDTO(PagedList<T> pagedList)
            {
                Items = pagedList.Items.ToList();
                PaginaAtual = pagedList.PaginaAtual;
                TotalPaginas = pagedList.TotalPaginas;
                PaginaTamanho = pagedList.PaginaTamanho;
                TotalCount = pagedList.TotalCount;
            }
        }
}
