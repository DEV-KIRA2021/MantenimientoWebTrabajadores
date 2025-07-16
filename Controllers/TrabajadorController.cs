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
        public async Task<IActionResult> Index(string sexo)
        {
            //  Cargar departamentos desde la base de datos
            ViewBag.Departamentos = await _context.Departamentos.ToListAsync();

            // Guardar el filtro aplicado (si hay)
            ViewBag.SexoSeleccionado = sexo;

            //  Ejecutar el procedimiento almacenado para listar trabajadores
            var trabajadores = await _context.ListadoTrabajadores
                .FromSqlRaw("EXEC sp_ListarTrabajadores")
                .ToListAsync();

            //  Filtrar si se seleccionó un sexo
            if (!string.IsNullOrEmpty(sexo))
            {
                trabajadores = trabajadores.Where(t => t.Sexo == sexo).ToList();
            }

            return View(trabajadores);
        }

        [HttpGet]
        // Acción GET que devuelve las provincias de un departamento en formato JSON (para llenar selects dinámicos)
        public async Task<JsonResult> GetProvincias(int departamentoId)
        {
            var provincias = await _context.Provincia
                .Where(p => p.IdDepartamento == departamentoId)
                .ToListAsync();

            return Json(provincias);
        }

        [HttpGet]
        public async Task<JsonResult> GetDistritos(int provinciaId)
        {
            var distritos = await _context.Distritos
                .Where(d => d.IdProvincia == provinciaId)
                .ToListAsync();

            return Json(distritos);
        }

        [HttpPost]
        // Acción POST que crea un nuevo trabajador o actualiza uno existente, dependiendo si el Id es 0 o no
        public async Task<IActionResult> Create(Trabajadores trabajador)
        {
            if (ModelState.IsValid)
            {
                if (trabajador.Id == 0)
                {
                    _context.Add(trabajador); // Nuevo
                }
                else
                {
                    _context.Update(trabajador); // Editar
                }

                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(trabajador);
        }

        [HttpGet]
        // Acción GET para obtener un trabajador específico por Id, devuelve JSON para usar en edición dinámica
        public async Task<JsonResult> GetTrabajador(int id)
        {
            var trabajador = await _context.Trabajadores.FindAsync(id);
            return Json(trabajador);
        }

        [HttpPost]
        // Acción POST para eliminar un trabajador por Id
        public async Task<IActionResult> Delete(int Id)
        {
            var trabajador = await _context.Trabajadores.FindAsync(Id);

            if (trabajador != null)
            {
                _context.Trabajadores.Remove(trabajador);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }
    }
}
