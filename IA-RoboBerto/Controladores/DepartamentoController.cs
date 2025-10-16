using IA_RoboBerto.Contratos.ContratosServicos;
using IA_RoboBerto.DTOs;
using IA_RoboBerto.Modelos;
using IA_RoboBerto.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace IA_RoboBerto.Controladores
{
        [ApiController]
        [Route("[controller]")]
        public class DepartamentoController : ControllerBase
        {
            private readonly IDepartamentoServico _servico;

            public DepartamentoController(IDepartamentoServico servico)
            {
                _servico = servico;
            }

            [HttpGet]
            public IEnumerable<DepartamentoDTO> Get()
            {
                return _servico.ListarTodos();
            }
        }
}
