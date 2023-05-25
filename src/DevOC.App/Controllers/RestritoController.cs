using DevOC.App.Filters;
using Microsoft.AspNetCore.Mvc;

namespace DevOC.App.Controllers
{
    [PaginaUsuarioLogado]
    public class RestritoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
