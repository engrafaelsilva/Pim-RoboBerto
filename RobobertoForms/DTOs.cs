using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobobertoForms
{
    public class AutorDTO
    {
        public string Nome { get; set; }
    }

    public class CategoriaDTO
    {
        public string Nome { get; set; }
    }

    public class ChamadoDTO
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public EStatusChamado Status { get; set; }
        public EPrioridade Prioridade { get; set; }
        public DateTime DataAbertura { get; set; }
        public string SugestaoGemini { get; set; }

        // Objetos aninhados
        public AutorDTO Autor { get; set; }
        public CategoriaDTO Categoria { get; set; }
    }

    public enum EPrioridade
    {
        NORMAL = 1,
        URGENTE,
        BAIXA
    }
    public enum EStatusChamado
    {
        ABERTO = 1,
        SUGESTAO_GERADA,
        FECHADO,
        CANCELADO
    }
}
