using DevOC.App.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text.Json;

namespace DevOC.App.Filters
{
    public class PaginaUsuarioLogado : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            string sessaoUsuario = context.HttpContext.Session.GetString("sessaoUsuarioLogado");

            if (string.IsNullOrEmpty(sessaoUsuario))
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "login" }, { "action", "Index" } });
            }
            else
            {
                UsuarioViewModel usuario = JsonSerializer.Deserialize<UsuarioViewModel>(sessaoUsuario);
                if (usuario == null)
                {
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "login" }, { "action", "Index" } });
                }
            }

            base.OnActionExecuted(context);
        }
    }
}
