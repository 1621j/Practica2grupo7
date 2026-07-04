using Practica2Grupo7.BLL.Dtos;
using Practica2Grupo7.DAL.Entidades;
using Practica2Grupo7.DAL.Repositorios;

namespace Practica2Grupo7.BLL.Services
{
    public class ProductoService
    {
        private readonly Repository<Producto> _repository;

        public ProductoService()
        {
            _repository = new Repository<Producto>();
        }

        public async Task<List<ProductoDto>> ObtenerTodosAsync()
        {
            var productos = await _repository.ObtenerTodosAsync();

            return productos.Select(p => new ProductoDto
            {
                ProductoId = p.ProductoId,
                Nombre = p.Nombre,
                Descripcion = p.Descripcion,
                Precio = p.Precio,
                Stock = p.Stock,
                CategoriaId = p.CategoriaId,
                NombreCategoria = p.Categoria?.Nombre ?? string.Empty
            }).ToList();
        }

        public async Task<ProductoDto?> ObtenerPorIdAsync(int id)
        {
            var producto = await _repository.ObtenerPorIdAsync(id);

            if (producto == null)
                return null;

            return new ProductoDto
            {
                ProductoId = producto.ProductoId,
                Nombre = producto.Nombre,
                Descripcion = producto.Descripcion,
                Precio = producto.Precio,
                Stock = producto.Stock,
                CategoriaId = producto.CategoriaId,
                NombreCategoria = producto.Categoria?.Nombre ?? string.Empty
            };
        }

        public async Task CrearAsync(ProductoDto dto)
        {
            var producto = new Producto
            {
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion,
                Precio = dto.Precio,
                Stock = dto.Stock,
                CategoriaId = dto.CategoriaId
            };

            await _repository.AgregarAsync(producto);
            await _repository.GuardarCambiosAsync();
        }

        public async Task EditarAsync(ProductoDto dto)
        {
            var producto = await _repository.ObtenerPorIdAsync(dto.ProductoId);

            if (producto == null)
                return;

            producto.Nombre = dto.Nombre;
            producto.Descripcion = dto.Descripcion;
            producto.Precio = dto.Precio;
            producto.Stock = dto.Stock;
            producto.CategoriaId = dto.CategoriaId;

            await _repository.ActualizarAsync(producto);
            await _repository.GuardarCambiosAsync();
        }

        public async Task EliminarAsync(int id)
        {
            var producto = await _repository.ObtenerPorIdAsync(id);

            if (producto == null)
                return;

            await _repository.EliminarAsync(producto);
            await _repository.GuardarCambiosAsync();
        }
    }
}