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
    public class CafeController : Controller
    {
        private readonly ICafeRepositorio _cafeRepositorio;
        private readonly ISessao _sessao;
        private BancoContext _context;
        public CafeController(ICafeRepositorio cafeRepositorio, BancoContext context, ISessao sessao)
        {
            _cafeRepositorio = cafeRepositorio;
            _context = context;
            _sessao = sessao;

        }
        public IActionResult CafeEditar(int id)
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

            VaquinhaCafeModel cafe = _cafeRepositorio.ListarPorId(id);
            return View(cafe);
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

                    List<VaquinhaCafeModel> cafe = _cafeRepositorio.BuscarTodos();
                    return View(cafe);
                }
                else
                {
                    throw new Exception("Usuário ainda não está logado, efetue o login.");
                    _ = RedirectToAction("Index", "Login");
                }
            }

        }

        public IActionResult Apagar(int id)
        {
            try
            {
                _cafeRepositorio.Apagar(id);
                TempData["MensagemSucesso"] = "Vaquinha do Café deletado com sucesso!";
                return RedirectToAction("Index", "Cafe");
            }
            catch
            {
                TempData["MensagemErro"] = $"Ops, Não foi possivel deletar o seu Cafe, tente novamente!.";
                return RedirectToAction("Index", "Cafe");

            }
        }
        public IActionResult ApagarConfirmacao(int id)
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

           VaquinhaCafeModel cafe = _cafeRepositorio.ListarPorId(id);
            return View(cafe);
        }
        [HttpPost]
        public IActionResult Alterar(VaquinhaCafeModel cafee)
        {
            var query = _context.Cafe.Where(x => x.Id_Vaquinha_Cafe == cafee.Id_Vaquinha_Cafe).FirstOrDefault();

            try
            {
                if (query != null && cafee.Nome != null)
                {
                    _cafeRepositorio.Atualizar(cafee);
                    TempData["MensagemSucesso"] = "Vaquinha do Café atualizado com sucesso!";
                    return RedirectToAction("Index", "Cafe");
                }
                else
                {
                    TempData["MensagemErro"] = $"Ops, Não foi possivel alterar essa vaquinha, preencha os dados e tente novamente!";
                    return RedirectToAction("Index", "Cafe");
                }
            }
            catch (SystemException erro)
            {
                TempData["MensagemErro"] = $"Ops, Não foi possivel alterar o seu cafe,tente novamente!, detalhe do erro:{ erro.Message}";
                return RedirectToAction("CafeEditar", "Cafe");
            }

        }


        public IActionResult CafeCriar()
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

            return View();
        }
        [HttpPost]
        public IActionResult CafeCriar(VaquinhaCafeModel cafee)
        {
            try
            {
                var usuario = _sessao.BuscarSessaoDoUsuario();

                if (cafee.Nome != null || cafee.Descricao != null || cafee.Chave_Pix != null || cafee.Prazo_Pagamento != null)
                {

                    cafee = _cafeRepositorio.Adicionar(cafee);

                    TempData["MensagemSucesso"] = "Vaquinha do Café criado com sucesso";
                    return RedirectToAction("index", "Cafe");
                }
                else
                {
                    TempData["MensagemErro"] = $"Ops, campos com * são obrigatórios, preencha e tente novamente.";
                    return RedirectToAction("CafeCriar", "Cafe");
                }
                
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos criar o cafe.";
                return RedirectToAction("Index", "Cafe");
            }
        }

        [HttpPost]
        public virtual IActionResult ParticipantesPagination(string sEcho, int iDisplayStart, int iColumns, int iDisplayLength, string sSearch, int id_cafe)
        {
            IEnumerable<Arquivos> query = _context.Arquivos.Where(x => x.Id_Cafe == id_cafe && x.StatusAprovado).ToList();

            if (!string.IsNullOrEmpty(sSearch)) query = query.Where(x => x.NomeUsuario.ToString().ToLower()
                .Contains(Utilities.RemoveSpecialCharacters(sSearch).ToLower())).AsQueryable();

            int recordsTotal = query.Count();

            List<Arquivos> aList = query.OrderBy(x => x.Data_Envio).Skip(iDisplayStart).ToList();

            var data = aList.Select(x => new
            {
                id = x.ID,
                nome_usuario = x.NomeUsuario,
                status = x.StatusAprovado == true ? "Comprovante aprovado pelo Admin." : "Aguardando",
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

    }
}