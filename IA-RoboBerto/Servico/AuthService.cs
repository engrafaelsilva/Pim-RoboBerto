using IA_RoboBerto.Contratos.ContratosRepositorio;
using IA_RoboBerto.Contratos.ContratosServicos;
using IA_RoboBerto.Exceções;
using IA_RoboBerto.Modelos;
using System.Security.Claims;

namespace IA_RoboBerto.Servico
{
    public class AuthService : IAuthService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUsuarioRepositorio _usuarioRepo;

        public AuthService(IHttpContextAccessor httpContextAccessor, IUsuarioRepositorio usuarioRepo)
        {
            _httpContextAccessor = httpContextAccessor;
            _usuarioRepo = usuarioRepo;
        }

        public async Task<Usuario?> ObterUsuarioLogadoAsync()
        {
            var idUsuarioFromClaim = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var id = Guid.Parse(idUsuarioFromClaim);
            if(id == null) throw new ForbiddenException("Acesso negado");

            var usuario = await _usuarioRepo.ObterPorIdAsync(id);
            if (usuario == null) throw new UserNotFoundException("Usuário não encontrado");

            return usuario;
        }
        public async Task ValidateSelfOrAdminAsync(Guid id)
        {
            var usuario = await ObterUsuarioLogadoAsync();

            bool isAdmin = usuario.Roles.Any(r => r.Nome == "ADM");
            if (!isAdmin && usuario.Id != id)
            {
                throw new ForbiddenException("Access denied");
            }
        }

    }
}
