using Microsoft.AspNetCore.Mvc;

namespace DevOC.App.Controllers
{
    public class ContatoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
