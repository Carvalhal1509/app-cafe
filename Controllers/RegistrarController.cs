using app_cadastro.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using app_cadastro.Repositorio;
using app_cadastro.Helper;
using System.Collections.Generic;
using app_cadastro.Util;

namespace app_cadastro.Controllers
{
    public class RegistrarController : Controller

    {
        private readonly IContatoRepositorio _contatoRepositorio;
        private readonly ISessao _sessao;
        public RegistrarController(IContatoRepositorio contatoRepositorio, ISessao sessao)
        {
            _contatoRepositorio = contatoRepositorio;
            _sessao = sessao;
        }

        public IActionResult PaginaAdm()
        {
            var usuario = _sessao.BuscarSessaoDoUsuario();

            if (usuario != null)
            {
                @ViewBag.Nome = usuario.Nome;
                @ViewBag.Perfil = usuario.Perfil;
                List<Usuarios> contato = _contatoRepositorio.BuscarTodos();
                return View(contato);
            }
            else
            {
                throw new Exception("Usuário ainda não está logado, efetue o login.");
                _ = RedirectToAction("Index", "Login");
            }
        }

        public IActionResult Criar()
        {
            var usuario = _sessao.BuscarSessaoDoUsuario();
            @ViewBag.Nome = usuario.Nome;
            @ViewBag.Perfil = usuario.Perfil;

            return View();
        }

        [HttpPost]
        public IActionResult Criar(Usuarios contato)
        {
            var usuario = _sessao.BuscarSessaoDoUsuario();
            @ViewBag.Nome = usuario.Nome;
            @ViewBag.Perfil = usuario.Perfil;

            try
            {
                if (contato.Nome == null || contato.Email == null || contato.Senha == null || contato.Celular == null)
                {
                    TempData["MensagemErro"] = "Campos com (*) são obrigatórios, preencha e tente novamente.";
                }
                else
                {
                    contato.Senha = Hash.SHA512(contato.Senha);
                    _contatoRepositorio.Adicionar(contato);
                    TempData["MensagemSucesso"] = "Usuário cadastrado com sucesso!";
                    return RedirectToAction("PaginaAdm" ,"Registrar");
                }
                return View(contato);

            }
            catch (SystemException erro)
            {
                TempData["MensagemErro"] = $"Ops, Não foi possivel cadastrar o seu usuário,tente novamente!, detalhe do erro:{ erro.Message}";
                return RedirectToAction("Index", "Login");
            }
        }
    }
}
