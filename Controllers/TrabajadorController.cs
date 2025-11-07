using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrabajadoresPrueba.Models;

namespace TrabajadoresPrueba.Controllers
{
    public class TrabajadorController : Controller
    {
        private readonly TrabajadoresPruebaContext _context;
        public TrabajadorController(TrabajadoresPruebaContext context)
        {
            _context = context;
        }
        // Mostrar lista de trabajadores con filtro por sexo (opcional)
        public async Task<IActionResult> Index(string sexo)
        {
            // Ejecutar el procedimiento almacenado
            var trabajadores = await _context.Trabajadores
                .FromSqlRaw("EXEC sp_ListarTrabajadores")
                .ToListAsync();

            // Guardar filtro actual
            ViewBag.SexoSeleccionado = sexo;

            // Contar hombres y mujeres
            ViewBag.CantidadHombres = trabajadores.Count(t => t.Sexo == "M");
            ViewBag.CantidadMujeres = trabajadores.Count(t => t.Sexo == "F");

            // Aplicar filtro si corresponde
            if (!string.IsNullOrEmpty(sexo))
            {
                trabajadores = trabajadores.Where(t => t.Sexo == sexo).ToList();
            }

            return View(trabajadores);
        }

        // Crear o editar trabajador
        [HttpPost]
        public async Task<IActionResult> Create(Trabajadores trabajador, IFormFile? FotoFile)
        {
            try
            {
                if (ModelState.IsValid)
                {
            bool existeDocumento = await _context.Trabajadores
                .AnyAsync(t => t.NumeroDocumento == trabajador.NumeroDocumento && t.Id != trabajador.Id);

            if (existeDocumento)
            {
                ModelState.AddModelError("NumeroDocumento", "El número de documento ya está registrado.");
                return View("Index", trabajador); // Muestra el error en la vista
            }
                    if (FotoFile != null && FotoFile.Length > 0)
                    {
                        var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                        if (!Directory.Exists(uploadsFolder))
                            Directory.CreateDirectory(uploadsFolder);

                        var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(FotoFile.FileName);
                        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await FotoFile.CopyToAsync(stream);
                        }

                        trabajador.Foto = "/uploads/" + uniqueFileName;
                    }

                    if (trabajador.Id == 0)
                        _context.Add(trabajador);
                    else
                        _context.Update(trabajador);

                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                // Te mostrará el error detallado
                ModelState.AddModelError("", $"Error al guardar: {ex.Message}");
            }

            return View("Index");
        }



        // Obtener datos de un trabajador específico (para edición)
        [HttpGet]
        public async Task<JsonResult> GetTrabajador(int id)
        {
            var trabajador = await _context.Trabajadores.FindAsync(id);
            return Json(trabajador);
        }

        // Eliminar trabajador
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var trabajador = await _context.Trabajadores.FindAsync(id);

            if (trabajador != null)
            {
                _context.Trabajadores.Remove(trabajador);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }
    }
}
