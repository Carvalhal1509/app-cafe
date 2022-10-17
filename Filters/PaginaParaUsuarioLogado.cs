using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using app_cadastro.Models;
using Newtonsoft.Json;

namespace app_cadastro.Filters
{
    public class PaginaParaUsuarioLogado : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            string sessaoUsuario = context.HttpContext.Session.GetString("sessaoUsuarioLogado");
            if (string.IsNullOrEmpty(sessaoUsuario))
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary { {"controller","Login" },{ "action","Index"} });
            }
            else
            {
                ContatoModel contato = JsonConvert.DeserializeObject<ContatoModel>(sessaoUsuario);
                if(contato == null)
                {
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary { {"controller","Login" },{ "action","Index"} });
                }
            }
                
            base.OnActionExecuted(context);
        }
    }
}
