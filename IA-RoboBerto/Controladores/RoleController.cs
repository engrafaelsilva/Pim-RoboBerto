using IA_RoboBerto.Modelos;
using IA_RoboBerto.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace IA_RoboBerto.Controladores
{
    [ApiController]
    [Route("[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly RoleRepositorio _repo;

        public RoleController(RoleRepositorio repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IEnumerable<Role> Get()
        {
            return _repo.listarTodos();
        }
    }
}
