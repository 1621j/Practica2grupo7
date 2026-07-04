using Microsoft.AspNetCore.Mvc;
using Practica2Grupo7.BLL.Dtos;
using Practica2Grupo7.BLL.Services;

namespace Practica2Grupo7.UI.Controllers
{
    public class ProductosController : Controller
    {
        private readonly ProductoService _productoService;

        public ProductosController()
        {
            _productoService = new ProductoService();
        }

        public async Task<IActionResult> Index()
        {
            var productos = await _productoService.ObtenerTodosAsync();
            return View(productos);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductoDto productoDto)
        {
            if (!ModelState.IsValid)
                return View(productoDto);

            await _productoService.CrearAsync(productoDto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var producto = await _productoService.ObtenerPorIdAsync(id);

            if (producto == null)
                return NotFound();

            return View(producto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductoDto productoDto)
        {
            if (!ModelState.IsValid)
                return View(productoDto);

            await _productoService.EditarAsync(productoDto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var producto = await _productoService.ObtenerPorIdAsync(id);

            if (producto == null)
                return NotFound();

            return View(producto);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _productoService.EliminarAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}