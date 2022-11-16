using app_cadastro.Data;
using app_cadastro.Models;
using app_cadastro.Util;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace app_cadastro.Controllers
{
    public class UsuarioController : Controller
    {
        private BancoContext _context;

        public UsuarioController(BancoContext context)
        {
            _context = context;
        }

        public IActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registrar(Usuarios Usuario)
        {
            return View();
        }

        [HttpPost]
        public IActionResult SalvarUsuario(Usuarios Usuario, string informacaoEmail2, string informacaoSenha2)
        {

            try
            {
                if (Usuario.Nome == null || Usuario.Email == null || Usuario.Senha == null || Usuario.Celular == null || informacaoEmail2 == null || informacaoSenha2 == null)
                {
                    return BadRequest(new Response<string>("", "Campos com (*) são obrigatórios, preencha e tente novamente.", false));
                }

                if (Usuario.Senha != informacaoSenha2 || Usuario.Email != informacaoEmail2)
                {
                    return BadRequest(new Response<string>("", "Email ou senha diferentes!", false));
                }

                Usuario.Senha = Hash.SHA512(Usuario.Senha);

                var query = _context.Usuarios.Where(x => x.Email == Usuario.Email).FirstOrDefault();

                if (query != null)
                {
                    return BadRequest(new Response<string>("", "Email já cadastrado!", false));
                }

                Usuario.Perfil = Enums.PerfilEnum.Padrao;
                _context.Usuarios.Add(Usuario);
                _context.SaveChanges();

                return Ok(new Response<string>("", "Salvo com sucesso", true));

            }
            catch (Exception erro)
            {
                return BadRequest(new Response<string>("", "Email já cadastrado!", false));
            }
        }
    }
}