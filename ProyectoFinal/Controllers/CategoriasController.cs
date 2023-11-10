using Microsoft.AspNetCore.Mvc;

namespace ProyectoFinal.Controllers
{
    public class CategoriasController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
