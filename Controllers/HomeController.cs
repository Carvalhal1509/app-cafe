using app_cadastro.Filters;
using app_cadastro.Helper;
using app_cadastro.Models;
using ControleDeContatos.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace app_cadastro.Controllers
{
    [PaginaParaUsuarioLogado]
    public class HomeController : Controller
    {
        private BancoContext _context;
        private readonly ISessao _sessao;

        public HomeController(BancoContext context, ISessao sessao)
        {
            _context = context;
            _sessao = sessao;
        }

        public IActionResult Index()
        {
            var usuario = _sessao.BuscarSessaoDoUsuario();

            if(usuario != null)
            {
                @ViewBag.Nome = usuario.Nome;
                @ViewBag.Perfil = usuario.Perfil;
                return View();
            }
            else
            {
                throw new Exception("Usuário ainda não está logado, efetue o login.");
                _ = RedirectToAction("Index", "Login");
            }

           
        }

     
        public IActionResult Criar()
        {
            return View();
        }

        public IActionResult Editar()
        {
            return View();
        }

        public IActionResult ApagarConfirmacao()
        {
            return View();
        }

        public IActionResult Apagar()
        {
            return View();
        }
    }
}


