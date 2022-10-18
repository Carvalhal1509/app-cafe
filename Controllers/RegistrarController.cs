using app_cadastro.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using app_cadastro.Repositorio;
using app_cadastro.Helper;
using System.Collections.Generic;

namespace app_cadastro.Controllers
{
    public class RegistrarController : Controller

    {
        private readonly IContatoRepositorio _contatoRepositorio;
        public RegistrarController(IContatoRepositorio contatoRepositorio)
        {
            _contatoRepositorio = contatoRepositorio;
        }

        public IActionResult PaginaAdm()
        {
            List<ContatoModel> contato = _contatoRepositorio.BuscarTodos();
            return View(contato);
        }

        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Criar(ContatoModel contato)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _contatoRepositorio.Adicionar(contato);
                    TempData["MensagemSucesso"] = "Usuário cadastrado com sucesso!";
                    return RedirectToAction("Index" ,"Login");
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
