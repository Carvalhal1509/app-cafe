using app_cadastro.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app_cadastro.Repositorio;

namespace app_cadastro.Controllers
{
    public class LoginController : Controller
    {
        private readonly IContatoRepositorio _contatoRepositorio;
        public LoginController(IContatoRepositorio contatoRepositorio)
        {
            _contatoRepositorio = contatoRepositorio;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Entrar(LoginModel loginModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return RedirectToAction("Index", "Home");
                }
                TempData["MensagemErro"] = $"Usuario e/ou senha invalido(s).Por favor,tente novamente!.";

                return View("index");
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, Não foi possivel realizar o Login,tente novamente!.";
                return RedirectToAction("Index");
            }
        }

    }
}