using app_cadastro.Data;
using app_cadastro.Filters;
using app_cadastro.Helper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using app_cadastro.Util;
using app_cadastro.Repositorio;
using app_cadastro.Models;

namespace app_cadastro.Controllers
{
    [PaginaParaUsuarioLogado]
    public class HomeController : Controller
    {
        private readonly IContatoRepositorio _contatoRepositorio;
        private BancoContext _context;
        private readonly ISessao _sessao;

        public HomeController(BancoContext context, ISessao sessao, IContatoRepositorio contatoRepositorio)
        {
            _context = context;
            _sessao = sessao;
            _contatoRepositorio = contatoRepositorio;
        }

        public IActionResult Index()
        {
            var usuario = _sessao.BuscarSessaoDoUsuario();

            if(usuario != null)
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
        public IActionResult AlterarSenha(string SenhaAtual, string NovaSenha, string ConfirmarSenha)
        {
            var usuario = _sessao.BuscarSessaoDoUsuario();
            @ViewBag.Nome = usuario.Nome;
            @ViewBag.Perfil = usuario.Perfil;
            @ViewBag.Email = usuario.Email;
            @ViewBag.Celular = usuario.Celular;
            @ViewBag.Aniversario = usuario.Aniversario;

            try
            {
                if (usuario.Senha == Hash.SHA512(SenhaAtual))
                {
                    if (NovaSenha == ConfirmarSenha)
                    {
                        NovaSenha = Hash.SHA512(NovaSenha);
                        
                        usuario.Senha = NovaSenha;
                        _contatoRepositorio.Atualizar(usuario);
                        return Ok(new Response<string>("", "Senha alterada com sucesso!", true));
                    }
                    else
                    {
                        return BadRequest(new Response<string>("", "Nova senha e confirmar nova senha estão diferentes,favor verificar!", false));
                    }
                }
                else
                {
                    return BadRequest(new Response<string>("", "Senha atual invalida, tente novamente!", false));
                }
            }
            catch (SystemException erro)
            {
                return BadRequest(new Response<string>("", "Ops, Não foi possivel mdificar a sua senha ,tente novamente!", false));
            }
        }


        public IActionResult teste()
        {
            return View();
        }


        public IActionResult Criar()
        {
            return View();
        }

        public IActionResult Editar()
        {
            return View();
        }

        public IActionResult ApagarConfirmacao()
        {
            return View();
        }

        public IActionResult Apagar()
        {
            return View();
        }
    }
}


