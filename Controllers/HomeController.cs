using Microsoft.AspNetCore.Mvc;
using SpotifyApp.Models;

namespace SpotifyApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password))
            {
                ViewBag.Error = "Por favor, ingresa correo y contraseña.";
                return View(model);
            }

            // Credenciales maestras fijas para el Administrador
            if (model.Email.ToLower() == "admin@yoss.com" && model.Password == "Admin123*")
            {
                HttpContext.Session.SetString("UserRole", "Admin");
                HttpContext.Session.SetString("UserEmail", model.Email);
                return RedirectToAction("Index", "Songs");
            }
            else
            {
                // Cualquier otro correo entra como Usuario predeterminado
                HttpContext.Session.SetString("UserRole", "User");
                HttpContext.Session.SetString("UserEmail", model.Email);
                return RedirectToAction("Index", "Songs");
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}