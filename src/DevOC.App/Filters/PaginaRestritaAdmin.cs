using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text.Json;
using System;
using DevOC.App.ViewModels;

namespace DevOC.App.Filters
{
    public class PaginaRestritaAdmin : ActionFilterAttribute
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

                if (usuario.Perfil != DevOC.Business.Enums.TiposUsuarios.Admin)
                {
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Restrito" }, { "action", "Index" } });
                }
            }

            base.OnActionExecuted(context);
        }
    }
}
