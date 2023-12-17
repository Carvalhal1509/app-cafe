using app_cadastro.Data;
using app_cadastro.Filters;
using app_cadastro.Helper;
using app_cadastro.Models;
using app_cadastro.Repositorio;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace app_cadastro.Controllers
{
    [PaginaParaUsuarioLogado]
    public class ContatoController : Controller
    {
        private readonly IContatoRepositorio _contatoRepositorio;
        private BancoContext _context;
        private readonly ISessao _sessao;
        public ContatoController(IContatoRepositorio contatoRepositorio, BancoContext context, ISessao sessao)
        {
            _contatoRepositorio = contatoRepositorio;
            _context = context;
            _sessao = sessao;
        }

        public IActionResult Eventos()
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
            //List<Usuarios> contato = _contatoRepositorio.BuscarTodos();
        }

        
        public IActionResult Editar(int id)
        {
            var usuario = _sessao.BuscarSessaoDoUsuario();
            @ViewBag.Nome = usuario.Nome;
            @ViewBag.Perfil = usuario.Perfil;
            @ViewBag.Email = usuario.Email;
            @ViewBag.Celular = usuario.Celular;
            @ViewBag.Aniversario = usuario.Aniversario;


            Usuarios contato = _contatoRepositorio.ListarPorId(id);
            return View(contato);
        }

        public IActionResult ApagarConfirmacao(int id)
        {
            var usuario = _sessao.BuscarSessaoDoUsuario();
            @ViewBag.Nome = usuario.Nome;
            @ViewBag.Perfil = usuario.Perfil;
            @ViewBag.Email = usuario.Email;
            @ViewBag.Celular = usuario.Celular;
            @ViewBag.Aniversario = usuario.Aniversario;

            Usuarios contato = _contatoRepositorio.ListarPorId(id);
            return View(contato);
        }

        public IActionResult Apagar(int id)
        {
            try
            {
                _contatoRepositorio.Apagar(id);
                TempData["MensagemSucesso"] = "Usuário deletado com sucesso!";
                return RedirectToAction("PaginaAdm", "Registrar");
            }
            catch
            {
                TempData["MensagemErro"] = $"Ops, Não foi possivel deletar o seu usuário, tente novamente!.";
                return RedirectToAction("PaginaAdm", "Registrar");

            }
        }

       
        [HttpPost]
        public IActionResult Alterar(Usuarios contato)
        {

            var query = _context.Usuarios.Where(x => x.Id == contato.Id).FirstOrDefault();
            var buscaPorEmail = _context.Usuarios.Where(x => x.Email == contato.Email).FirstOrDefault();

            try
            {
                //if (!contato.Email.EndsWith("@detran.ba.gov.br"))
                //{
                //    TempData["MensagemErro"] = "Email não pertence ao Detran, por favor, cadastre-se com email @detran.ba.gov.br";
                //    return RedirectToAction("PaginaAdm", "Registrar");
                //}
                //else 
                if (query != null && contato.Email == query.Email)
                {
                    contato.Senha = query.Senha;
                    _contatoRepositorio.Atualizar(contato);
                    TempData["MensagemSucesso"] = "Usuário atualizado com sucesso!";
                    return RedirectToAction("PaginaAdm", "Registrar");
                }
                else if (buscaPorEmail != null)
                {
                    TempData["MensagemErro"] = "Este email já está cadastrado no nosso sistema, troque o email e tente novamente";
                    return RedirectToAction("PaginaAdm", "Registrar");
                }              
                else
                {
                    TempData["MensagemErro"] = $"Ops, Não foi possivel alterar esse usuário, tente novamente!";
                    return View("Editar", contato);
                }
            }
            catch (SystemException erro)
            {
                TempData["MensagemErro"] = $"Ops, Não foi possivel alterar o seu usuário,tente novamente!, detalhe do erro:{ erro.Message}";
                return RedirectToAction("Editar", "Contato");
            }
            
        }
    }
}
