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
using app_cadastro.Util;

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
            var usuario = _sessao.BuscarSessaoDoUsuario();
            if (usuario != null)
            {
                @ViewBag.Nome = usuario.Nome;
                @ViewBag.Perfil = usuario.Perfil;
                @ViewBag.Email = usuario.Email;
                @ViewBag.Celular = usuario.Celular;
                @ViewBag.Aniversario = usuario.Aniversario;

                EventoModel evento1 = _eventoRepositorio.ListarPorId(id);
                return View(evento1);
            }
            else
            {
                throw new Exception("Usuário ainda não está logado, efetue o login.");
                _ = RedirectToAction("Index", "Login");
            }
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
            }

            List<EventoModel> evento = _eventoRepositorio.BuscarTodos();
                
            return View(evento);

        }

        public IActionResult MeusEventos()
        {
            var usuario = _sessao.BuscarSessaoDoUsuario();

            if (usuario != null)
            {
                @ViewBag.Nome = usuario.Nome;
                @ViewBag.Perfil = usuario.Perfil;
                @ViewBag.Email = usuario.Email;
                @ViewBag.Celular = usuario.Celular;
                @ViewBag.Aniversario = usuario.Aniversario;
            }

            List<EventoModel> evento = _eventoRepositorio.BuscarTodos().Where(x=> x.UsuarioId == usuario.Id && !x.StatusExc).ToList();

            return View(evento);

        }

        public IActionResult CriarEvento()
        {
            var usuario = _sessao.BuscarSessaoDoUsuario();
            @ViewBag.Nome = usuario.Nome;
            @ViewBag.Perfil = usuario.Perfil;
            @ViewBag.Email = usuario.Email;
            @ViewBag.Celular = usuario.Celular;
            @ViewBag.Aniversario = usuario.Aniversario;

            return View();
        }

        public IActionResult Apagar(int id)
        {

            try
            {
                var usuario = _sessao.BuscarSessaoDoUsuario();

                _eventoRepositorio.Apagar(id, usuario.Id);
                TempData["MensagemSucesso"] = "Evento deletado com sucesso!";
                return RedirectToAction("MeusEventos", "Eventos");
            }
            catch
            {
                TempData["MensagemErro"] = $"Ops, ocorreu um erro ao tentar deletar seu evento!";
                return RedirectToAction("MeusEventos", "Eventos");

            }
        }
        public IActionResult ApagarConfirmacao(int id)
        {

            var usuario = _sessao.BuscarSessaoDoUsuario();
            @ViewBag.Nome = usuario.Nome;
            @ViewBag.Perfil = usuario.Perfil;
            @ViewBag.Email = usuario.Email;
            @ViewBag.Celular = usuario.Celular;
            @ViewBag.Aniversario = usuario.Aniversario;

            EventoModel evento = _eventoRepositorio.ListarPorId(id);
            return View(evento);
        }

        [HttpPost]
        public IActionResult Alterar(EventoModel evento)   
        {
            var query = _context.Eventos.Where(x => x.Id == evento.Id).FirstOrDefault();

            try
            {
                if (query != null && evento.NomeEvento != null && evento.Descricao != null)
                {
                    evento.Organizador = query.Organizador;
                    Usuarios usuario = _sessao.BuscarSessaoDoUsuario();
                    evento.UsuarioId = usuario.Id;
                    _eventoRepositorio.Atualizar(evento);
                    TempData["MensagemSucesso"] = "Evento atualizado com sucesso!";
                    return RedirectToAction("MeusEventos", "Eventos");
                }
                else
                {
                    TempData["MensagemErro"] = $"Ops, Não foi possivel alterar esse evento, verifique os campos obrigatórios(*) e tente novamente!";
                    return RedirectToAction("MeusEventos", "Eventos");
                }
            }
            catch (SystemException erro)
            {
                TempData["MensagemErro"] = $"Ops, Não foi possivel alterar o seu evento,tente novamente!, detalhe do erro:{ erro.Message}";
                return RedirectToAction("MeusEventos", "Eventos");
            }

        }



        [HttpPost]
        public IActionResult CriarEvento(EventoModel evento)
        {
            try
            {
                var usuario = _sessao.BuscarSessaoDoUsuario();

                if (evento.NomeEvento != null || evento.Descricao != null)
                {
                    evento.UsuarioId = usuario.Id;
                    evento.Organizador = usuario.Nome;
                    evento = _eventoRepositorio.Adicionar(evento);

                    TempData["MensagemSucesso"] = "Evento criado com sucesso";
                    return RedirectToAction("MeusEventos", "Eventos");
                }
                else
                {
                    TempData["MensagemErro"] = $"Ops, verifique os campos obrigatórios(*) e tente novamente.";
                    return View(evento);
                }
            }
            catch(Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos criar o evento.";
                return RedirectToAction("MeusEventos", "Eventos");
            }
        }

        public IActionResult BuscarDadosEvento(int id_evento)
        {

            string error = string.Empty;
            bool is_action = false;

            var dados = _context.Eventos.Where(x => x.Id == id_evento && !x.StatusExc).FirstOrDefault();

            try
            {
                if (dados != null)
                {
                    is_action = true;
                }

            }
            catch (Exception ex)
            {
                error = ex.Message;
            }

            return Json(new { is_action, error, dados });
        }

        [HttpPost]
        public IActionResult AdicionarItem(int id_evento, string nome)
        {
            string error = string.Empty;
            bool is_action = false;

            try
            {
                if (nome == null) throw new Exception("Preencha o nome do item e tente novamente");

                var usuario = _sessao.BuscarSessaoDoUsuario();

                if (usuario != null)
                {
                    ItemEventoModel item = new ItemEventoModel();
                    item.Id_Evento = id_evento;
                    item.Nome = nome;

                    _context.ItemEvento.Add(item);
                    _context.SaveChanges();

                    is_action = true;
                }
            }
            catch (Exception erro)
            {
                error = erro.Message;
            }

            return Json(new { is_action, error, id_evento });
        }


        public List<ItemEventoModel> BuscarItens(int id_evento)
        {
            var query = _context.ItemEvento.Where(x => x.Id_Evento == id_evento && !x.StatusExc).OrderBy(x => x.Nome).ToList();
            @ViewBag.Itens_Evento = query;
            return query;
        }

        [HttpPost]
        public IActionResult Buscar(int id_evento)
        {
            string error = string.Empty;
            bool is_action = false;

            List<ItemEventoModel> itens = null;

            try
            {
                itens = _context.ItemEvento.Where(x => x.Id_Evento == id_evento).OrderBy(x => x.Nome).ToList();
                is_action = true;
            }
            catch (Exception erro)
            {
                error = erro.Message;
            }

            return Json(new { is_action, error, itens});
        }

        [HttpPost]
        public virtual IActionResult EscolhaItensPagination(string sEcho, int iDisplayStart, int iColumns, int iDisplayLength, string sSearch, int id_evento)
        {
            var usuario = _sessao.BuscarSessaoDoUsuario();

            IEnumerable<EventoItemUsuarioModel> query = _context.EventoItemUsuario.Where(x => x.Id_Evento == id_evento && !x.StatusExc).ToList();

            if (!string.IsNullOrEmpty(sSearch)) query = query.Where(x => x.Nome_Usuario.ToString().ToLower()
                .Contains(Utilities.RemoveSpecialCharacters(sSearch).ToLower())).AsQueryable();

            int recordsTotal = query.Count();

            List<EventoItemUsuarioModel> aList = query.OrderBy(x => x.Nome_Usuario).Skip(iDisplayStart).ToList();

            var data = aList.Select(x => new
            {
                nome_item = x.Nome_Item,
                nome_usuario = x.Nome_Usuario,
                excluir = usuario.Perfil == Enums.PerfilEnum.Dev || usuario.Id == x.Id_Usuario ? $"<a href='#' type='button' class='btn btn-danger' onclick='modalApagar({x.Id})'>Remover</a>" : ""
            }).ToArray();

            return Json(new
            {
                iDraw = 1,
                sEcho,
                iTotalRecords = recordsTotal,
                iTotalDisplayRecords = recordsTotal,
                aaData = data
            });

        }

        [HttpPost]
        public IActionResult SalvarEscolha(int id_evento, int id_item)
        {
            string error = string.Empty;
            bool is_action = false;

            try
            {
                var usuario = _sessao.BuscarSessaoDoUsuario();
                var queryItem = _context.ItemEvento.Where(x => x.Id == id_item && x.Id_Evento == id_evento).FirstOrDefault();

                if (usuario != null)
                {
                    EventoItemUsuarioModel escolha = new EventoItemUsuarioModel();
                    escolha.Id_Usuario = usuario.Id;
                    escolha.Nome_Usuario = usuario.Nome;
                    escolha.Id_Evento = id_evento;
                    escolha.Id_Item_Evento = id_item;
                    escolha.Nome_Item = queryItem.Nome;

                    _context.EventoItemUsuario.Add(escolha);
                    _context.SaveChanges();

                    is_action = true;
                }
            }
            catch (Exception erro)
            {
                error = erro.Message;
            }

            return Json(new { is_action, error, id_evento });
        }

        public IActionResult ApagarParticipacao(int id)
        {
            var query = _context.EventoItemUsuario.Where(x => x.Id == id).FirstOrDefault();

            try
            {
                if(query != null)
                {
                    query.StatusExc = true;

                    _context.EventoItemUsuario.Update(query);
                    _context.SaveChanges();

                    TempData["MensagemSucesso"] = "Participacao deletada com sucesso!";
                }
                return RedirectToAction("Index", "Eventos");
            }
            catch
            {
                TempData["MensagemErro"] = $"Ops, ocorreu um erro ao tentar deletar ssua participacao!";
                return RedirectToAction("MeusEventos", "Eventos");

            }
        }


    }
}

