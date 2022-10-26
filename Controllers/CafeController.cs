using app_cadastro.Helper;
using ControleDeContatos.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace app_cadastro.Controllers
{
    public class CafeController : Controller
    {
        private BancoContext _context;
        private readonly ISessao _sessao;

        public CafeController(BancoContext context, ISessao sessao)
        {
            _context = context;
            _sessao = sessao;
        }
        public IActionResult Index()
        {
            var usuario = _sessao.BuscarSessaoDoUsuario();
            @ViewBag.Nome = usuario.Nome;
            @ViewBag.Perfil = usuario.Perfil;

            return View();
        }
    }
}
