using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TrabajadoresPrueba.Models;

namespace TrabajadoresPrueba.Controllers
{
    public class HomeController : Controller
    {
        private readonly TrabajadoresPruebaContext _DBcontext;

        public HomeController(TrabajadoresPruebaContext context)
        {
            _DBcontext = context;
        }

        public IActionResult Index()
        {
            return View();
        }

    }
}
