namespace Front.DTOs
{
    public class PagedListDTO<T>
    {
        public List<T> Items { get; set; }
        public int PaginaAtual { get; set; }
        public int TotalPaginas { get; set; } 
        public int PaginaTamanho { get; set; }
        public int TotalCount { get; set; }
    }
}
