using DevOC.App.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace DevOC.App.ViewComponents
{
    public class Menu : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            string sessaoUsuario = HttpContext.Session.GetString("sessaoUsuarioLogado");

            if (string.IsNullOrEmpty(sessaoUsuario)) return null;

            UsuarioViewModel usuario = JsonSerializer.Deserialize<UsuarioViewModel>(sessaoUsuario);

            return View(usuario);
        }
    }
}
