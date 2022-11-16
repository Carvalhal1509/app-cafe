using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app_cadastro.Models;
using app_cadastro.Repositorio;
using app_cadastro.Filters;
using app_cadastro.Helper;
using app_cadastro.Data;

namespace app_cadastro.Controllers
{
    [PaginaParaUsuarioLogado]
    public class EventosController : Controller
    {
        private readonly IEventoRepositorio _eventoRepositorio;
        private readonly ISessao _sessao;
        private BancoContext _context;
        public EventosController(IEventoRepositorio eventoRepositorio, BancoContext context, ISessao sessao)
        {
            _eventoRepositorio = eventoRepositorio;
            _context = context;
            _sessao = sessao;

        }
        public IActionResult EventoEditar(int id)
        {

            EventoModel evento1 = _eventoRepositorio.ListarPorId(id);
            return View(evento1);
        }




        public IActionResult Index()
        {
            {
                var usuario = _sessao.BuscarSessaoDoUsuario();

                if (usuario != null)
                {
                    @ViewBag.Nome = usuario.Nome;
                    @ViewBag.Perfil = usuario.Perfil;
                    @ViewBag.Email = usuario.Email;
                    @ViewBag.Celular = usuario.Celular;
                    @ViewBag.Aniversario = usuario.Aniversario;

                    List<EventoModel> evento = _eventoRepositorio.BuscarTodos();
                    return View(evento);

                }
                else
                {
                    throw new Exception("Usuário ainda não está logado, efetue o login.");
                    _ = RedirectToAction("Index", "Login");
                }
            }

        }
        public IActionResult CriarEvento()
        {
            return View();
        }

        public IActionResult Apagar(int id)
        {
            try
            {
                _eventoRepositorio.Apagar(id);
                TempData["MensagemSucesso"] = "Evento deletado com sucesso!";
                return RedirectToAction("Index", "Eventos");
            }
            catch
            {
                TempData["MensagemErro"] = $"Ops, Não foi possivel deletar o seu evento, tente novamente!.";
                return RedirectToAction("Index", "Eventos");

            }
        }
        public IActionResult ApagarConfirmacao(int id)
        {
        
            EventoModel evento = _eventoRepositorio.ListarPorId(id);
            return View(evento);
        }
        [HttpPost]
        public IActionResult Alterar(EventoModel evento)
       
        {
            var query = _context.Eventos.Where(x => x.Id == evento.Id).FirstOrDefault();

            try
            {
                if (query != null)
                {
                    evento.Organizador = query.Organizador;
                    _eventoRepositorio.Atualizar(evento);
                    TempData["MensagemSucesso"] = "Evento atualizado com sucesso!";
                    return RedirectToAction("Index", "Eventos");
                }
                else
                {
                    TempData["MensagemErro"] = $"Ops, Não foi possivel alterar esse evento, tente novamente!";
                    return View("EventoEditar", evento);
                }
            }
            catch (SystemException erro)
            {
                TempData["MensagemErro"] = $"Ops, Não foi possivel alterar o seu evento,tente novamente!, detalhe do erro:{ erro.Message}";
                return RedirectToAction("EventoEditar", "Eventos");
            }

        }



        [HttpPost]
        public IActionResult CriarEvento(EventoModel evento)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    evento = _eventoRepositorio.Adicionar(evento);

                    TempData["MensagemSucesso"] = "Evento criado com sucesso";
                    return RedirectToAction("index", "Eventos");
                }
                return View(evento);
            }
            catch(Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos criar o evento.";
                return RedirectToAction("Index", "Eventos");
            }
        }

    }
}

