using app_cadastro.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using app_cadastro.Repositorio;
using app_cadastro.Helper;
using System.Collections.Generic;

namespace app_cadastro.Controllers
{
    public class LoginController : Controller
    {
        private readonly IContatoRepositorio _contatoRepositorio;
        private readonly ISessao _sessao;

        public LoginController(IContatoRepositorio contatoRepositorio,ISessao sessao)
        {
            _contatoRepositorio = contatoRepositorio;
            _sessao = sessao;
        }
        public IActionResult Index()

        {//se o usuario estiver logado,redirecionar para a home
            if (_sessao.BuscarSessaoDoUsuario() != null) return RedirectToAction("Index", "Home");
            return View();
        }
        
        public IActionResult Sair()
        {
            _sessao.RemoveSessaoDoUsuario();
            return RedirectToAction("Index", "Login");
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
                            _sessao.CriarSessaoDoUsuario(contato);
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


    
