using Microsoft.AspNetCore.Mvc;

namespace DFT_MVC.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Welcome(string name, int liczba = 1)
        {
            ViewData["text"] = "Cześć " + name;
            ViewData["liczba"] = liczba;
            return View();
        }
    }
}
