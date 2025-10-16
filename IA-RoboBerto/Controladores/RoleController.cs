using IA_RoboBerto.Contratos.ContratosServicos;
using IA_RoboBerto.DTOs;
using IA_RoboBerto.Modelos;
using IA_RoboBerto.Repositorio;
using IA_RoboBerto.Servico;
using Microsoft.AspNetCore.Mvc;

namespace IA_RoboBerto.Controladores
{
    [ApiController]
    [Route("[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleServico _servico;

        public RoleController(IRoleServico servico)
        {
            _servico = servico;
        }

        [HttpGet]
        public IEnumerable<RoleDTO> Get()
        {
            return _servico.ListarTodos();
        }
    }
}
