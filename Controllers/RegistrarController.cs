using app_cadastro.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using app_cadastro.Repositorio;
using app_cadastro.Helper;
using System.Collections.Generic;
using app_cadastro.Util;
using System.Linq;
using app_cadastro.Filters;

namespace app_cadastro.Controllers
{
    [PaginaRestritaSomenteAdmin]
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
                @ViewBag.Email = usuario.Email;
                @ViewBag.Celular = usuario.Celular;
                @ViewBag.Aniversario = usuario.Aniversario;
                List<Usuarios> contato = _contatoRepositorio.BuscarTodos();
                return View(contato);
            }
            else
            {
                throw new Exception("Usuário ainda não está logado, efetue o login.");
                _ = RedirectToAction("Index", "Login");
            }
        }

        [HttpPost]
        public virtual IActionResult UsuariosInativosPagination(string sEcho, int iDisplayStart, int iColumns, int iDisplayLength, string sSearch,
                                                                string email, string nome)
        {
            IEnumerable<Usuarios> query = _contatoRepositorio.ListarTodos().Where(x => x.StatusExc == true);

            if (!string.IsNullOrEmpty(email)) query = query.Where(x => x.Email == email);
            if (!string.IsNullOrEmpty(nome)) query = query.Where(x => x.Nome == nome);

            if (!string.IsNullOrEmpty(sSearch)) query = query.Where(x => x.Nome.ToLower()
                .Contains(Utilities.RemoveSpecialCharacters(sSearch).ToLower())).AsQueryable();



            int recordsTotal = query.Count();

            List<Usuarios> aList = query.OrderBy(x => x.Nome).Skip(iDisplayStart).Take(iDisplayLength).ToList();

            var data = aList.Select(x => new
            {
                ide_usuario = x.Id,
                nome = x.Nome,
                email = x.Email,
                celular = x.Celular,
                dtc_nascimento = x.Aniversario.ToString("dd/MM/yyyy"),
                perfil = x.Perfil == Enums.PerfilEnum.AdminAniversariantes ? "Admin. Aniversariantes" : x.Perfil == Enums.PerfilEnum.AdminCafe ? "Admin. Café" : x.Perfil == Enums.PerfilEnum.Dev ? "Desenvolvedor" : "Padrão",
                reativar = $"<a href='#' type='button' class='btn btn-success' onclick='modalReativar({x.Id})'>Reativar</a>",
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

        public IActionResult UsuariosInativos()
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

        public IActionResult Criar()
        {
            var usuario = _sessao.BuscarSessaoDoUsuario();
            @ViewBag.Nome = usuario.Nome;
            @ViewBag.Perfil = usuario.Perfil;
            @ViewBag.Email = usuario.Email;
            @ViewBag.Celular = usuario.Celular;
            @ViewBag.Aniversario = usuario.Aniversario;

            return View();
        }

        [HttpPost]
        public IActionResult Criar(Usuarios contato)
        {
            var usuario = _sessao.BuscarSessaoDoUsuario();
            @ViewBag.Nome = usuario.Nome;
            @ViewBag.Perfil = usuario.Perfil;
            @ViewBag.Email = usuario.Email;
            @ViewBag.Celular = usuario.Celular;
            @ViewBag.Aniversario = usuario.Aniversario;

            try
            {
                if (contato.Nome == null || contato.Email == null || contato.Celular == null)
                {
                    TempData["MensagemErro"] = "Campos com (*) são obrigatórios, preencha e tente novamente.";
                }
                //else if (!contato.Email.EndsWith("@detran.ba.gov.br"))
                //{
                //    TempData["MensagemErro"] = "Email não pertence ao Detran, por favor, cadastre-se com email @detran.ba.gov.br";
                //}
                else
                {
                    string novaSenha = Guid.NewGuid().ToString().Substring(0, 8);
                    string senhaNaoEncrypitada = novaSenha;

                    contato.Senha = Hash.SHA512(novaSenha);
                    _contatoRepositorio.Adicionar(contato);
                    TempData["MensagemSucesso"] = $"Usuário cadastrado com sucesso! A senha provisória de {contato.Nome} é {senhaNaoEncrypitada}";
                    return RedirectToAction("PaginaAdm", "Registrar");
                }
                return View(contato);

            }
            catch (SystemException erro)
            {
                TempData["MensagemErro"] = $"Ops, Não foi possivel cadastrar o seu usuário,tente novamente!, detalhe do erro:{ erro.Message}";
                return RedirectToAction("Index", "Login");
            }
        }

        public IActionResult ReativarUsuario(int ide_usuario)
        {

            string error = string.Empty;
            bool is_action = false;

            try
            {
                _contatoRepositorio.ReativarUsuario(ide_usuario);
                is_action = true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }

            return Json(new { is_action, error });
        }
    }
}
