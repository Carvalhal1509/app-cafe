using app_cadastro.Data;
using app_cadastro.Helper;
using app_cadastro.Models;
using app_cadastro.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace app_cadastro.Controllers
{
    public class ArquivosController : Controller
    {
        private BancoContext _context;
        private readonly ISessao _sessao;
        public ArquivosController(BancoContext bancoContext, ISessao sessao)
        {
            _context = bancoContext;
            _sessao = sessao;
        }
        public IActionResult Index()
        {
            var usuario = _sessao.BuscarSessaoDoUsuario();
            var query = _context.Cafe.Where(x => !x.StatusExc).ToList();
            var arquivos = _context.Arquivos.ToList();

            if (usuario != null)
            {
                @ViewBag.Nome = usuario.Nome;
                @ViewBag.Perfil = usuario.Perfil;
                @ViewBag.Email = usuario.Email;
                @ViewBag.Celular = usuario.Celular;
                @ViewBag.Aniversario = usuario.Aniversario;
                @ViewBag.ListaCafe = query;

            }
          
            return View(arquivos);
        }

        [HttpPost]
        public virtual IActionResult ComprovantesPixPagination(string sEcho, int iDisplayStart, int iColumns, int iDisplayLength, string sSearch, int id_cafe)
        {
            IEnumerable<Arquivos> query = _context.Arquivos.Where(x => x.Id_Cafe == id_cafe && !x.StatusAprovado).ToList();

            if (!string.IsNullOrEmpty(sSearch)) query = query.Where(x => x.NomeUsuario.ToString().ToLower()
                .Contains(Utilities.RemoveSpecialCharacters(sSearch).ToLower())).AsQueryable();

            int recordsTotal = query.Count();

            List<Arquivos> aList = query.OrderBy(x => x.Data_Envio).Skip(iDisplayStart).ToList();

            var data = aList.Select(x => new
            {
                id_comprovante = x.ID,
                descricao = x.Descricao,
                nome_usuario = x.NomeUsuario,
                data_envio = x.Data_Envio.ToString("dd/MM/yyy"),
                visualizar = x.Dados == null ? "": $"<a href='#' type='button' class='btn btn-primary' onclick='visualizar({x.ID})'>Visualizar</a>",
                aprovar = $"<a href='#' type='button' class='btn btn-success' onclick='aprovar({x.ID})'>Aprovar</a>"
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
        public IActionResult UploadImagem(IList<IFormFile> arquivos, int id_cafe)
        {
            var usuario = _sessao.BuscarSessaoDoUsuario();
            IFormFile imagemCarregada = arquivos.FirstOrDefault();

            if (imagemCarregada != null)
            {
                MemoryStream ms = new MemoryStream();
                imagemCarregada.OpenReadStream().CopyTo(ms);

                Arquivos arqui = new Arquivos()
                {
                    Descricao = imagemCarregada.FileName,
                    Dados = ms.ToArray(),
                    Id_Cafe = id_cafe,
                    Id_Usuario = usuario.Id,
                    NomeUsuario = usuario.Nome,
                    ContentType = imagemCarregada.ContentType
                };

                _context.Arquivos.Add(arqui);
                _context.SaveChanges();

                TempData["MensagemSucesso"] = "Comprovante enviado com sucesso! Aguarde aprovação do Administrador para entrar na lista de participantes.";

            }
            else
            {
                Arquivos arqui = new Arquivos()
                {
                    Id_Cafe = id_cafe,
                    Id_Usuario = usuario.Id,
                    NomeUsuario = usuario.Nome
                };

                _context.Arquivos.Add(arqui);
                _context.SaveChanges();

                TempData["MensagemSucesso"] = $"Participação efetuada com sucesso! Aguarde aprovação do Administrador para entrar na lista de participantes";
            }

            return RedirectToAction("Index", "Cafe");
        }

        public IActionResult Visualizar(int id)
        {
            var arquivosBanco = _context.Arquivos.FirstOrDefault(a => a.ID == id);

            return File(arquivosBanco.Dados, arquivosBanco.ContentType,arquivosBanco.Descricao);
        }

        public IActionResult BuscarDadosCafe(int id_cafe)
        {

            string error = string.Empty;
            bool is_action = false;

            var dados = _context.Cafe.Where(x => x.Id_Vaquinha_Cafe == id_cafe).FirstOrDefault();

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
        public IActionResult AprovarPix(int id_comprovante)
        {
            string error = string.Empty;
            bool is_action = false;

            var query = _context.Arquivos.Where(x => x.ID == id_comprovante).FirstOrDefault();

            try
            {
                
                if (query != null)
                {
                    query.StatusAprovado = true;
                    _context.Arquivos.Update(query);
                    _context.SaveChanges();

                    is_action = true;
                }
            }
            catch (Exception erro)
            {
                error = erro.Message;
            }

            return Json(new { is_action, error, query.Id_Cafe});
        }
    }
}
