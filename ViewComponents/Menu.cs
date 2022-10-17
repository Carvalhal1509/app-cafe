using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using app_cadastro.Models;
using Newtonsoft.Json;

namespace app_cadastro.ViewComponents
{
    public class Menu : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            string sessaoUsuario = HttpContext.Session.GetString("sessaoUsuarioLogado");
            if (string.IsNullOrEmpty(sessaoUsuario)) return null;
            ContatoModel contato = JsonConvert.DeserializeObject<ContatoModel>(sessaoUsuario);
            return View(contato);
        }
    }
}
