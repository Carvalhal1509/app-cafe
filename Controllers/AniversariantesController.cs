using app_cadastro.Filters;
using app_cadastro.Helper;
using app_cadastro.Models;
using app_cadastro.Repositorio;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace app_cadastro.Controllers
{
    [PaginaParaUsuarioLogado]
    public class AniversariantesController : Controller
    {
        private readonly IAniversariantesRepositorio _aniversariantesRepositorio;
        private readonly ISessao _sessao;
        public AniversariantesController(IAniversariantesRepositorio aniversariantesRepositorio, ISessao sessao)
        {
            _aniversariantesRepositorio = aniversariantesRepositorio;
            _sessao = sessao;
        }
        public IActionResult Index()
        {
            var usuario = _sessao.BuscarSessaoDoUsuario();

            if (usuario != null)
            {

                @ViewBag.Nome = usuario.Nome;
                @ViewBag.Perfil = usuario.Perfil;
                @ViewBag.Email = usuario.Email;
                @ViewBag.Celular = usuario.Celular;
                @ViewBag.Aniversario = usuario.Aniversario;
                List<Usuarios> contato = _aniversariantesRepositorio.BuscarTodos();
                return View(contato);
            }
            else
            {
                throw new Exception("Usuário ainda não está logado, efetue o login.");
                _ = RedirectToAction("Index", "Login");
            }
        }
    }
}
