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

            CafeModel cafe = _cafeRepositorio.ListarPorId(id);
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

                    List<CafeModel> cafe = _cafeRepositorio.BuscarTodos();
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
                TempData["MensagemSucesso"] = "Cafe deletado com sucesso!";
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

            CafeModel cafe = _cafeRepositorio.ListarPorId(id);
            return View(cafe);
        }
        [HttpPost]
        public IActionResult Alterar(CafeModel cafee)

        {
            var query = _context.Cafe.Where(x => x.Id == cafee.Id).FirstOrDefault();

            try
            {
                if (query != null)
                {
                    cafee.Organizador = query.Organizador;
                    _cafeRepositorio.Atualizar(cafee);
                    TempData["MensagemSucesso"] = "Cafe atualizado com sucesso!";
                    return RedirectToAction("Index", "Cafe");
                }
                else
                {
                    TempData["MensagemErro"] = $"Ops, Não foi possivel alterar esse cafe, tente novamente!";
                    return View("CafeEditar", cafee);
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
            return View();
        }
        [HttpPost]
        public IActionResult CafeCriar(CafeModel cafee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    cafee = _cafeRepositorio.Adicionar(cafee);

                    TempData["MensagemSucesso"] = "Cafe criado com sucesso";
                    return RedirectToAction("index", "Cafe");
                }
                return View(cafee);
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos criar o cafe.";
                return RedirectToAction("Index", "Cafe");
            }
        }

    }
}