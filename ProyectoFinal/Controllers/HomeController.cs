using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Models;
using System.Diagnostics;
using ProyectoFinal.Models.ViewModel;
using Microsoft.EntityFrameworkCore;
using ProyectoFinal.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Web;

namespace ProyectoFinal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ProyectoFinalContext _context;
        public HomeController(ILogger<HomeController> logger, ProyectoFinalContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(ViewUsuarioLogin userLogin)
        {
            var usuario = _context.Usuario.
                FirstOrDefault(u => u.Password.Equals(userLogin.Password) 
                && u.Email.Equals(userLogin.Email));
            if (usuario != null)
            {
                // TempData["Usuario"] = usuario;

                ViewBag.Mensaje = "Bienvenido";
                return RedirectToAction("Index", "Usuarios");
            }
            ViewBag.Mensaje = "Usuario y/o contraseña incorrecta";
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}