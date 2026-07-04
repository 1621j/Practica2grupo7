using Microsoft.AspNetCore.Mvc;
using Practica2Grupo7.BLL.Dtos;
using Practica2Grupo7.BLL.Services;

namespace Practica2Grupo7.UI.Controllers
{
    public class CategoriasController : Controller
    {
        private readonly CategoriaService _categoriaService;

        public CategoriasController()
        {
            _categoriaService = new CategoriaService();
        }

        public async Task<IActionResult> Index()
        {
            var categorias = await _categoriaService.ObtenerTodasAsync();
            return View(categorias);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoriaDto categoriaDto)
        {
            if (!ModelState.IsValid)
                return View(categoriaDto);

            await _categoriaService.CrearAsync(categoriaDto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var categoria = await _categoriaService.ObtenerPorIdAsync(id);

            if (categoria == null)
                return NotFound();

            return View(categoria);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CategoriaDto categoriaDto)
        {
            if (!ModelState.IsValid)
                return View(categoriaDto);

            await _categoriaService.EditarAsync(categoriaDto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var categoria = await _categoriaService.ObtenerPorIdAsync(id);

            if (categoria == null)
                return NotFound();

            return View(categoria);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _categoriaService.EliminarAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}