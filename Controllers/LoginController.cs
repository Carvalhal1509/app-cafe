using app_cadastro.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app_cadastro.Repositorio;
using app_cadastro.Models;

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
                   ContatoModel contato = _contatoRepositorio.BuscarPorLogin(loginModel.Login);
                   
                    if (contato != null)
                    {
                        if(contato.SenhaValida(loginModel.Senha))
                        { 
                            return RedirectToAction("Index", "Home");
                        }

                        TempData["MensagemErro"] = $"Senha invalida.Por favor,tente novamente!.";
                    }
                    TempData["MensagemErro"] = $"Usuario e/ou Senha invalida.Por favor,tente novamente!.";
                }
                return View("index");
            }
            catch (Exception erro)
            { 
            TempData["MensagemErro"] = $"Ops, Não foi possivel realizar o Login,tente novamente!.";
        }
   return RedirectToAction("Index");
    }
   }
}


    
