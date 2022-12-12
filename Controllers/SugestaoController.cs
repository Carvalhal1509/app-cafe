using app_cadastro.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using app_cadastro.Repositorio;
using app_cadastro.Helper;
using System.Collections.Generic;

namespace app_cadastro.Controllers
{
    public class SugestaoController : Controller

    {
        private readonly ISugestaoRepositoriocs _sugestaoRepositoriocs;
        private readonly ISessao _sessao;
        public SugestaoController(ISugestaoRepositoriocs sugestaoRepositoriocs,ISessao sessao)
        {
            _sugestaoRepositoriocs = sugestaoRepositoriocs;
            _sessao = sessao;
        }
        public IActionResult Index()
        {
            var usuario = _sessao.BuscarSessaoDoUsuario();

            @ViewBag.Nome = usuario.Nome;
            @ViewBag.Perfil = usuario.Perfil;
            @ViewBag.Email = usuario.Email;
            @ViewBag.Celular = usuario.Celular;
            @ViewBag.Aniversario = usuario.Aniversario.ToString("dd/MM/yyyy");

            List<SugestaoModel> sugestao = _sugestaoRepositoriocs.BuscarTodos();
            return View(sugestao);
        }

        [HttpPost]
        public IActionResult Criar(string Descricao)
        {
            string error = string.Empty;
            bool is_action = false;

            var usuario = _sessao.BuscarSessaoDoUsuario();

            try
            {
                if (Descricao == null) throw new Exception("Sugestão não pode ser vazia, por favor digite a sugestão e tente novamente!");

                if (ModelState.IsValid)
                {
                    SugestaoModel sugestao = new SugestaoModel();
                    sugestao.Descricao = Descricao;
                    sugestao.DataSugestao = DateTime.Now;
                    sugestao.UsuarioSugestao = usuario.Nome;

                    _sugestaoRepositoriocs.Adicionar(sugestao);

                    is_action = true;
                }

            }
            catch (Exception ex)
            {
                error = ex.Message;
            }

            return Json(new { is_action, error });
        }
    }
}





