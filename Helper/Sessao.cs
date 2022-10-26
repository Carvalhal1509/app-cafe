using app_cadastro.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace app_cadastro.Helper
{
    public class Sessao : ISessao
    {
        private readonly IHttpContextAccessor _httpContext;
        public Sessao(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }
        public Usuarios BuscarSessaoDoUsuario()
        {
            string sessaoUsuario = _httpContext.HttpContext.Session.GetString("sessaoUsuarioLogado");
            if (string.IsNullOrEmpty(sessaoUsuario)) return null;
            return JsonConvert.DeserializeObject<Usuarios>(sessaoUsuario);
        }

        public void CriarSessaoDoUsuario(Usuarios contato)
        {
            string valor = JsonConvert.SerializeObject(contato);
            _httpContext.HttpContext.Session.SetString("sessaoUsuarioLogado",valor);
        }

        public void RemoveSessaoDoUsuario()
        {
            _httpContext.HttpContext.Session.Remove("sessaoUsuarioLogado");
        }
    }
}
