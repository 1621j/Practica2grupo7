using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Practica2Grupo7.BLL.Dtos;
using Practica2Grupo7.BLL.Services;

namespace Practica2Grupo7.UI.Controllers
{
    public class ProductosController : Controller
    {
        private readonly ProductoService _productoService;
        private readonly CategoriaService _categoriaService;

        public ProductosController()
        {
            _productoService = new ProductoService();
            _categoriaService = new CategoriaService();
        }

        public async Task<IActionResult> Index()
        {
            await CargarCategorias();
            var productos = await _productoService.ObtenerTodosAsync();
            return View(productos);
        }

        [HttpGet]
        public async Task<JsonResult> ObtenerTodos()
        {
            var productos = await _productoService.ObtenerTodosAsync();

            return Json(new
            {
                dato = productos
            });
        }

        [HttpGet]
        public async Task<JsonResult> ObtenerPorId(int id)
        {
            var producto = await _productoService.ObtenerPorIdAsync(id);

            return Json(new
            {
                esCorrecto = producto != null,
                dato = producto
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductoDto productoDto)
        {
            if (!ModelState.IsValid)
            {
                return Json(new
                {
                    esCorrecto = false,
                    mensaje = "Datos inválidos"
                });
            }

            await _productoService.CrearAsync(productoDto);

            return Json(new
            {
                esCorrecto = true,
                mensaje = "Producto creado correctamente"
            });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductoDto productoDto)
        {
            if (!ModelState.IsValid)
            {
                return Json(new
                {
                    esCorrecto = false,
                    mensaje = "Datos inválidos"
                });
            }

            await _productoService.EditarAsync(productoDto);

            return Json(new
            {
                esCorrecto = true,
                mensaje = "Producto actualizado correctamente"
            });
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _productoService.EliminarAsync(id);

            return Json(new
            {
                esCorrecto = true,
                mensaje = "Producto eliminado correctamente"
            });
        }

        private async Task CargarCategorias()
        {
            var categorias = await _categoriaService.ObtenerTodasAsync();

            ViewBag.Categorias = new SelectList(
                categorias,
                "CategoriaId",
                "Nombre"
            );
        }
    }
}