using IA_RoboBerto.Contratos.ContratosRepositorio;
using IA_RoboBerto.Modelos;
using IA_RoboBerto.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace IA_RoboBerto.Controladores
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IDepartamentoRepositorio _repo;

        public UsuarioController(IDepartamentoRepositorio repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IEnumerable<Departamento> Get()
        {
            return _repo.ListarTodos();
        }
    }
}
