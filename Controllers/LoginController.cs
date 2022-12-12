using app_cadastro.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using app_cadastro.Repositorio;
using app_cadastro.Helper;
using System.Collections.Generic;
using app_cadastro.Util;

namespace app_cadastro.Controllers
{
    public class LoginController : Controller
    {
       
        private readonly IContatoRepositorio _contatoRepositorio;
        private readonly ISessao _sessao;
        private readonly IEmail _email;

        public LoginController(IContatoRepositorio contatoRepositorio, ISessao sessao, IEmail email)
        {
            _contatoRepositorio = contatoRepositorio;
            _sessao = sessao;
            _email = email;
        }
        
        public IActionResult Index()

        {//se o usuario estiver logado,redirecionar para a home
            if (_sessao.BuscarSessaoDoUsuario() != null) return RedirectToAction("Index", "Home");
            return View();
        }

        public IActionResult RedefinirSenha()
        {
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
                Usuarios contato = _contatoRepositorio.BuscarPorLogin(loginModel.Email);

                if (ModelState.IsValid)
                {
                    if (!loginModel.Email.EndsWith("@detran.ba.gov.br"))
                    {
                        TempData["MensagemErro"] = "Email não pertence ao DETRAN, por favor, entre com seu email pessoal do DETRAN";
                    }
                    else if (contato.StatusExc == false)
                    {
                        loginModel.Senha = Hash.SHA512(loginModel.Senha);

                        if (contato.SenhaValida(loginModel.Senha))
                        {
                            _sessao.CriarSessaoDoUsuario(contato);
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            TempData["MensagemErro"] = $"Email ou Senha inválidos. Por favor,tente novamente!.";
                        }
                    }
                    else
                    {
                        TempData["MensagemErro"] = $"Este usuário não tem mais acesso à esse sistema, Por favor, contate algum Administrador!!";
                    }
                    
                }
                return View("index");
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, Não foi possivel realizar o Login,tente novamente!.";
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult EnviarLinkParaRedefinirSenha(RedefinirSenhaModel redefinirSenhaModel)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    string novaSenha = Guid.NewGuid().ToString().Substring(0, 8);
                    string novaSenhacriptografada = Util.Hash.SHA512(novaSenha);

                    Usuarios contato = _contatoRepositorio.BuscarPorEmailAlterarSenha(redefinirSenhaModel.Email, novaSenhacriptografada);
                    if (contato != null)
                    {
                        string mensagem = $"<strong>Olá,</strong><br>" +
                            $"<br>" +
                            $"A senha associada ao seu email no Detran - Café & Eventos foi alterada.<br>" +
                            $"<br>" +
                            $"Sua nova senha é:<br>" +
                            $"<br>" +
                            $"{novaSenha}<br>" +
                            $"<br>" +
                            $"<p>Obrigado por usar o DETRAN - Café & Eventos";
                        bool emailEnviado = _email.Enviar(contato.Email,"Detran Web Café e Eventos - Nova Senha",mensagem);
                        if (emailEnviado)
                        {
                            TempData["MensagemSucesso"] = $"Enviamos para o seu email cadastrado uma nova senha.";
                            return RedirectToAction("Index", "Login");
                        }
                        else
                        {

                            TempData["MensagemErro"] = $"Não conseguimos enviar para o seu email cadastrado uma nova senha.Porfavor,tente novamente.";
                            return RedirectToAction("Index", "Login");
                        }
                    }
                    TempData["MensagemErro"] = $"Não conseguimos redefinir sua senha.Por favor,verifique o email informado.";
                }
                return View("index");
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, Não foi possivel redefinir sua senha! Tente novamente.";
                return RedirectToAction("Index");
            }
        }
    }
}





    
