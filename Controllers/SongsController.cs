using Microsoft.AspNetCore.Mvc;
using SpotifyApp.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace SpotifyApp.Controllers
{
    public class SongsController : Controller
    {
        private static SongArray songArray = new SongArray(50);

        public IActionResult Index()
        {
            ViewBag.Capacity = songArray.Capacity;
            ViewBag.Count = songArray.Count;
            ViewBag.Role = HttpContext.Session.GetString("UserRole") ?? "User";
            ViewBag.Email = HttpContext.Session.GetString("UserEmail") ?? "Invitado";

            return View(songArray.GetAll());
        }

        [HttpPost]
        public async Task<IActionResult> Add(Song song, IFormFile? audioFile)
        {
            string role = HttpContext.Session.GetString("UserRole") ?? "User";
            if (role != "Admin")
            {
                TempData["Error"] = "Solo el Administrador puede agregar canciones.";
                return RedirectToAction("Index");
            }

            // Si se subió un archivo MP3 real
            if (audioFile != null && audioFile.Length > 0)
            {
                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(audioFile.FileName);
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await audioFile.CopyToAsync(fileStream);
                }

                song.FilePath = "/uploads/" + uniqueFileName;
            }

            if (ModelState.IsValid)
            {
                songArray.Add(song);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            string role = HttpContext.Session.GetString("UserRole") ?? "User";
            if (role != "Admin")
            {
                TempData["Error"] = "Solo el Administrador puede eliminar canciones.";
                return RedirectToAction("Index");
            }

            songArray.Delete(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Sort()
        {
            string role = HttpContext.Session.GetString("UserRole") ?? "User";
            if (role != "Admin")
            {
                TempData["Error"] = "Solo el Administrador puede ordenar el arreglo.";
                return RedirectToAction("Index");
            }

            songArray.SortByTitle();
            return RedirectToAction("Index");
        }
    }
}