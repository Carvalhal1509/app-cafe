using app_cadastro.Models;
using app_cadastro.Repositorio;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace app_cadastro.Controllers
{
    public class ContatoController : Controller
    {
        private readonly IContatoRepositorio _contatoRepositorio;
        public ContatoController(IContatoRepositorio contatoRepositorio)
        {
            _contatoRepositorio = contatoRepositorio;
        }
       

        public IActionResult Eventos()
        {
            List<ContatoModel> contato = _contatoRepositorio.BuscarTodos();
            return View(contato);
        }

        
        public IActionResult Editar(int id)
        {
            ContatoModel contato = _contatoRepositorio.ListarPorId(id);
            return View(contato);
        }

        public IActionResult ApagarConfirmacao(int id)
        {
            ContatoModel contato = _contatoRepositorio.ListarPorId(id);
            return View(contato);
        }
        public IActionResult Apagar(int id)
        {
            try
            {
                _contatoRepositorio.Apagar(id);
                TempData["MensagemSucesso"] = "Usuário deletado com sucesso!";
                return RedirectToAction("Index", "Login");
            }
            catch
            {
                TempData["MensagemErro"] = $"Ops, Não foi possivel deletar o seu usuário,tente novamente!.";
                return RedirectToAction("Index", "Login");

            }
        }

       
        [HttpPost]
        public IActionResult Alterar(ContatoModel contato)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _contatoRepositorio.Atualizar(contato);
                    TempData["MensagemSucesso"] = "Usuário atualizado com sucesso!";
                    return RedirectToAction("Index", "Login");
                }
                return View("Editar", contato);
            }
            catch (SystemException erro)
            {
                TempData["MensagemErro"] = $"Ops, Não foi possivel alterar o seu usuário,tente novamente!, detalhe do erro:{ erro.Message}";
                return RedirectToAction("Index", "Login");

            }
        }
    }
}
