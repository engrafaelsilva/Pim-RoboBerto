using IA_RoboBerto.Models;
using IA_RoboBerto.Repository;
using Microsoft.AspNetCore.Mvc;

namespace IA_RoboBerto.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly UsuarioRepository _repository;

        public UsuarioController(UsuarioRepository repository)
        {
            _repository = repository;
        }

        // ============= LISTAGEM DE USUÁRIOS =========================

        // GET: /Usuario/Index
        public IActionResult Index()
        {
            var usuarios = _repository.ObterTodos();

            return View(usuarios);
        }

        // ============= CADASTRAR NOVO USUÁRIO =======================
        
        // GET: /Usuario/Cadastrar
        public IActionResult Cadastrar()
        {
            return View();
        }

        // POST: /Usuario/Cadastrar
        [HttpPost]
        public IActionResult Cadastrar(Usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                return View(usuario);
            }

            var existente = _repository.ObterPorEmail(usuario.Email);
            if (existente != null)
            {
                ModelState.AddModelError("Email", "Já existe um usuário com este e-mail.");
                return View(usuario);
            }

            _repository.Adicionar(usuario);

            TempData["MensagemSucesso"] = "Usuário cadastrado com sucesso!";

            return RedirectToAction("Login");
        }

        // ============= LOGIN DE USUÁRIO =============================
        
        // GET: /Usuario/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: /Usuario/Login
        [HttpPost]
        public IActionResult Login(string email, string senha)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(senha))
            {
                ModelState.AddModelError("", "Informe e-mail e senha.");
          
                return View();
            }

            bool valido = _repository.ValidarLogin(email, senha);

            if (!valido)
            {
                ModelState.AddModelError("", "E-mail ou senha incorretos.");
                return View();
            }

            TempData["MensagemSucesso"] = $"Bem-vindo(a), {email}!";

            return RedirectToAction("Index", "Chamados");
        }
    }
}
