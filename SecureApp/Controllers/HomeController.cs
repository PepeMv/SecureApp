using Microsoft.AspNetCore.Mvc;
using Negocio.Interfaces;
using System.Diagnostics;
using SecureApp.Models;

namespace SecureApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUsuarioRepo _usuarioRepo;

        public HomeController(ILogger<HomeController> logger, IUsuarioRepo usuarioRepo)
        {
            _logger = logger;
            _usuarioRepo = usuarioRepo;
        }

        public IActionResult Index()
        {
            var usuario = _usuarioRepo.DameUsuarioPorId(1).GetAwaiter().GetResult();

            return View(usuario);
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
