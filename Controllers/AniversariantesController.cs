using app_cadastro.Data;
using app_cadastro.Filters;
using app_cadastro.Helper;
using app_cadastro.Models;
using app_cadastro.Repositorio;
using app_cadastro.Util;
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
        private readonly IAniversariantesDoMesRepositorio _aniversariantesDoMesRepositorio;
        private readonly IContatoRepositorio _contatoRepositorio;
        private readonly ISessao _sessao;
        private BancoContext _context;
        public AniversariantesController(IAniversariantesRepositorio aniversariantesRepositorio, ISessao sessao, IAniversariantesDoMesRepositorio aniversariantesDoMesRepositorio,
            IContatoRepositorio contatoRepositorio, BancoContext context)
        {
            _aniversariantesRepositorio = aniversariantesRepositorio;
            _sessao = sessao;
            _aniversariantesDoMesRepositorio = aniversariantesDoMesRepositorio;
            _contatoRepositorio = contatoRepositorio;
            _context = context;
        }
        public IActionResult Index()
        {
            var usuario = _sessao.BuscarSessaoDoUsuario();
            var itens = _aniversariantesDoMesRepositorio.ListarTodosItens().Where(x => !x.StatusExc).OrderBy(x => x.Nome);

            if (usuario != null)
            {

                @ViewBag.Nome = usuario.Nome;
                @ViewBag.Perfil = usuario.Perfil;
                @ViewBag.Email = usuario.Email;
                @ViewBag.Celular = usuario.Celular;
                @ViewBag.Aniversario = usuario.Aniversario;
                @ViewBag.Itens = itens;
                List<Usuarios> contato = _aniversariantesRepositorio.BuscarTodos();
                return View(contato);
            }
            else
            {
                throw new Exception("Usuário ainda não está logado, efetue o login.");
                _ = RedirectToAction("Index", "Login");
            }
        }

        public IActionResult ItemAniversariantes()
        {
            var usuario = _sessao.BuscarSessaoDoUsuario();

            if (usuario != null)
            {

                @ViewBag.Nome = usuario.Nome;
                @ViewBag.Perfil = usuario.Perfil;
                @ViewBag.Email = usuario.Email;
                @ViewBag.Celular = usuario.Celular;
                @ViewBag.Aniversario = usuario.Aniversario;

                return View();
            }
            else
            {
                throw new Exception("Usuário ainda não está logado, efetue o login.");
                _ = RedirectToAction("Index", "Login");
            }
        }
        public IActionResult CriarItem()
        {
            var usuario = _sessao.BuscarSessaoDoUsuario();

            if (usuario != null)
            {

                @ViewBag.Nome = usuario.Nome;
                @ViewBag.Perfil = usuario.Perfil;
                @ViewBag.Email = usuario.Email;
                @ViewBag.Celular = usuario.Celular;
                @ViewBag.Aniversario = usuario.Aniversario;

                return View();
            }
            else
            {
                throw new Exception("Usuário ainda não está logado, efetue o login.");
                _ = RedirectToAction("Index", "Login");
            }
        }

        [HttpPost]
        public IActionResult EditarItem(int Id,string nomeItem)
        {

            string error = string.Empty;
            bool is_action = false;

            var query = _context.ItemAniversario.Where(x => x.Id == Id).FirstOrDefault();

            try
            {
                if(query != null)
                {
                    ItemAniversarioModel item = new ItemAniversarioModel();
                    query.Nome = nomeItem;

                    _context.SaveChanges();
                }


                is_action = true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }

            return Json(new { is_action, error });
        }


        [HttpPost]
        public IActionResult EditarEventoAniversario(string nome, string descricao, DateTime data, int Id)
        {

            string error = string.Empty;
            bool is_action = false;

            var query = _context.AniversariantesDoMes.Where(x => x.Id == Id).FirstOrDefault();

            try
            {
                if (query != null)
                {
                    AniversariantesModel item = new AniversariantesModel();

                        query.Nome = nome;
                        query.Descricao = descricao;
                        query.DataRealizacao = data;

                    _context.SaveChanges();
                }


                is_action = true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }

            return Json(new { is_action, error });
        }


        [HttpPost]
        public virtual IActionResult AniversariantesDoMesPagination(string sEcho, int iDisplayStart, int iColumns, int iDisplayLength, string sSearch)
        {
            var usuario = _sessao.BuscarSessaoDoUsuario();

            IEnumerable<AniversariantesModel> query = _aniversariantesDoMesRepositorio.ListarTodos().Where(x => !x.StatusExc);

            if (!string.IsNullOrEmpty(sSearch)) query = query.Where(x => x.Nome.ToLower()
                .Contains(Utilities.RemoveSpecialCharacters(sSearch).ToLower())).AsQueryable();

            int recordsTotal = query.Count();

            List<AniversariantesModel> aList = query.OrderBy(x => x.Nome).Skip(iDisplayStart).Take(iDisplayLength).ToList();

            var data = aList.Select(x => new
            {
                ide_aniversariantes = x.Id,
                nome = x.Nome,
                detalhes = $"<a href='#' type='button' class='btn btn-success' onclick='modalDetalhes({x.Id})'>Detalhes</a>",
                editar = usuario.Perfil == Enums.PerfilEnum.AdminAniversariantes || usuario.Perfil == Enums.PerfilEnum.Dev ? $"<a href='#' type='button' class='btn btn-warning' onclick='modalEditar({x.Id})'>Editar</a>" : "",
                excluir = usuario.Perfil == Enums.PerfilEnum.AdminAniversariantes || usuario.Perfil == Enums.PerfilEnum.Dev ? $"<a href='#' type='button' class='btn btn-danger' onclick='modalExclusao({x.Id})'>Excluir</a>" : ""
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
        public virtual IActionResult ItensPagination(string sEcho, int iDisplayStart, int iColumns, int iDisplayLength, string sSearch)
        {
            var usuario = _sessao.BuscarSessaoDoUsuario();

            IEnumerable<ItemAniversarioModel> query = _aniversariantesDoMesRepositorio.ListarTodosItens();

            if (!string.IsNullOrEmpty(sSearch)) query = query.Where(x => x.Nome.ToLower()
                .Contains(Utilities.RemoveSpecialCharacters(sSearch).ToLower())).AsQueryable();

            int recordsTotal = query.Count();

            List<ItemAniversarioModel> aList = query.OrderBy(x => x.Nome).Skip(iDisplayStart).Take(iDisplayLength).ToList();

            var data = aList.Select(x => new
            {
                ide_item = x.Id,
                nome = x.Nome,
                editar =  $"<a href='#' type='button' class='btn btn-warning' onclick='modalEditar({x.Id})'>Editar</a>",
                excluir = $"<a href='#' type='button' class='btn btn-danger' onclick='modalExclusao({x.Id})'>Excluir</a>"
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
        public virtual IActionResult EscolhaItensPagination(string sEcho, int iDisplayStart, int iColumns, int iDisplayLength, string sSearch, int ide_aniversario)
        {
            var usuario = _sessao.BuscarSessaoDoUsuario();

            IEnumerable<AniversariantesItemUsuarioModel> query = _aniversariantesDoMesRepositorio.ListarTodosVinculos(ide_aniversario).Where(x => !x.StatusExc);

            if (!string.IsNullOrEmpty(sSearch)) query = query.Where(x => x.Nome_Usuario.ToString().ToLower()
                .Contains(Utilities.RemoveSpecialCharacters(sSearch).ToLower())).AsQueryable();

            int recordsTotal = query.Count();

            List<AniversariantesItemUsuarioModel> aList = query.OrderBy(x => x.Nome_Usuario).Skip(iDisplayStart).Take(iDisplayLength).ToList();
            //var queryUsuario = _contatoRepositorio.ListarPorId();

            var data = aList.Select(x => new
            {
                nome_item = x.Nome_Item,
                nome_usuario = x.Nome_Usuario,
                excluir = usuario.Perfil == Enums.PerfilEnum.AdminAniversariantes || usuario.Perfil == Enums.PerfilEnum.Dev || usuario.Id == x.Id_Usuario ? $"<a href='#' type='button' class='btn btn-danger' onclick='modalApagar({x.Id})'>Remover</a>" : ""
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

        public IActionResult Criar()
        {
            var usuario = _sessao.BuscarSessaoDoUsuario();

            if (usuario != null)
            {

                @ViewBag.Nome = usuario.Nome;
                @ViewBag.Perfil = usuario.Perfil;
                @ViewBag.Email = usuario.Email;
                @ViewBag.Celular = usuario.Celular;
                @ViewBag.Aniversario = usuario.Aniversario;
                return View();
            }
            else
            {
                throw new Exception("Usuário ainda não está logado, efetue o login.");
                _ = RedirectToAction("Index", "Login");
            }
        }

        [HttpPost]
        public IActionResult CriarAniversariantes(AniversariantesModel aniversariantes)
        {
            try
            {
                var usuario = _sessao.BuscarSessaoDoUsuario();

                if (aniversariantes != null && usuario != null)
                {
                    aniversariantes = _aniversariantesDoMesRepositorio.Adicionar(aniversariantes);

                    TempData["MensagemSucesso"] = "Aniversariantes do mês criado com sucesso";
                    return RedirectToAction("Index", "Aniversariantes");
                }
                else
                {
                    TempData["MensagemErro"] = $"Ops, verifique os dados não preenchidos e tente novamente.";
                    return View(aniversariantes);
                }
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos criar o evento.";
                return RedirectToAction("Index", "Eventos");
            }
        }

        [HttpPost]
        public IActionResult SalvarEscolha(int ide_aniversariantes, int id_item)
        {
            string error = string.Empty;
            bool is_action = false;

            try
            {
                var usuario = _sessao.BuscarSessaoDoUsuario();
                var queryItem = _aniversariantesDoMesRepositorio.ListarItemPorId(id_item);

                if (queryItem.StatusExc == true) throw new Exception("Item já escolhido, recarregue a página e escolha outro item");

                if (usuario != null)
                {
                    AniversariantesItemUsuarioModel escolha = new AniversariantesItemUsuarioModel();
                    escolha.Id_Usuario = usuario.Id;
                    escolha.Nome_Usuario = usuario.Nome;
                    escolha.Id_Aniversariantes = ide_aniversariantes;
                    escolha.Id_Item = id_item;
                    escolha.Nome_Item = queryItem.Nome;
                    escolha = _aniversariantesDoMesRepositorio.AdicionarEscolha(escolha);

                    is_action = true;
                }
            }
            catch (Exception erro)
            {
                error = erro.Message;
            }

            return Json(new { is_action, error, ide_aniversariantes});
        }

        public IActionResult ExcluirAniversariantes(int ide_aniversariantes)
        {

            string error = string.Empty;
            bool is_action = false;

            try
            {
                _aniversariantesDoMesRepositorio.Apagar(ide_aniversariantes);
                is_action = true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }

            return Json(new { is_action, error });
        }

        [HttpPost]
        public IActionResult CriarItem(ItemAniversarioModel item)
        {
            try
            {
                var usuario = _sessao.BuscarSessaoDoUsuario();

                if (item != null && usuario != null)
                {
                    item = _aniversariantesDoMesRepositorio.AdicionarItem(item);

                    TempData["MensagemSucesso"] = "Item criado com sucesso";
                    return RedirectToAction("ItemAniversariantes", "Aniversariantes");
                }
                else
                {
                    TempData["MensagemErro"] = $"Ops, verifique os dados não preenchidos e tente novamente.";
                    return View(item);
                }
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos criar o evento.";
                return RedirectToAction("Index", "Eventos");
            }
        }

        public IActionResult ExcluirItem(int ide_item)
        {

            string error = string.Empty;
            bool is_action = false;

            try
            {
                _aniversariantesDoMesRepositorio.ApagarItem(ide_item);
                is_action = true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }

            return Json(new { is_action, error });
        }

        public IActionResult BuscarDadosAniversariantes(int ide_aniversariantes)
        {

            string error = string.Empty;
            bool is_action = false;

            var dados = _aniversariantesDoMesRepositorio.ListarPorId(ide_aniversariantes);

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

        public IActionResult ApagarParticipacao(int id)
        {
            var query = _context.AniversariantesItemUsuario.Where(x => x.Id == id).FirstOrDefault();

            try
            {
                if (query != null)
                {
                    query.StatusExc = true;

                    _context.AniversariantesItemUsuario.Update(query);
                    _context.SaveChanges();

                    TempData["MensagemSucesso"] = "Participacao deletada com sucesso!";
                }
                return RedirectToAction("Index", "Aniversariantes");
            }
            catch
            {
                TempData["MensagemErro"] = $"Ops, ocorreu um erro ao tentar deletar ssua participacao!";
                return RedirectToAction("Index", "Aniversariantes");

            }
        }
    }
}
