using IA_RoboBerto.Contratos.ContratosRepositorio;
using IA_RoboBerto.Contratos.ContratosServicos;
using IA_RoboBerto.Exceções;
using IA_RoboBerto.Modelos;
using System.Security.Claims;

namespace IA_RoboBerto.Servico
{
    public class AuthServico : IAuthServico
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUsuarioRepositorio _usuarioRepo;

        public AuthServico(IHttpContextAccessor httpContextAccessor, IUsuarioRepositorio usuarioRepo)
        {
            _httpContextAccessor = httpContextAccessor;
            _usuarioRepo = usuarioRepo;
        }

        public async Task<Usuario?> ObterUsuarioLogadoAsync()
        {
            var user = _httpContextAccessor.HttpContext?.User;

            // Verifica se há usuário autenticado
            if (user == null || !user.Identity.IsAuthenticated)
                throw new ForbiddenException("Acesso negado");

            // Busca o ID da claim — use o nome exato do token ("nameid")
            var claimId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value
                       ?? user.FindFirst("nameid")?.Value;

            if (string.IsNullOrEmpty(claimId))
                throw new ForbiddenException("Acesso negado");

            if (!Guid.TryParse(claimId, out var idUsuario))
                throw new ForbiddenException("Acesso negado");

            // Busca o usuário no repositório
            var usuario = await _usuarioRepo.ObterPorIdAsync(idUsuario);

            if (usuario == null)
               throw new UserNotFoundException("Usuário não encontrado");

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
