using Practica2Grupo7.BLL.Dtos;
using Practica2Grupo7.DAL.Entidades;
using Practica2Grupo7.DAL.Repositorios;

namespace Practica2Grupo7.BLL.Services
{
    public class CategoriaService
    {
        private readonly Repository<Categoria> _repository;

        public CategoriaService()
        {
            _repository = new Repository<Categoria>();
        }

        public async Task<List<CategoriaDto>> ObtenerTodasAsync()
        {
            var categorias = await _repository.ObtenerTodosAsync();

            return categorias.Select(c => new CategoriaDto
            {
                CategoriaId = c.CategoriaId,
                Nombre = c.Nombre,
                Descripcion = c.Descripcion
            }).ToList();
        }

        public async Task CrearAsync(CategoriaDto dto)
        {
            var categoria = new Categoria
            {
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion
            };

            await _repository.AgregarAsync(categoria);
            await _repository.GuardarCambiosAsync();
        }

        public async Task<CategoriaDto?> ObtenerPorIdAsync(int id)
        {
            var categoria = await _repository.ObtenerPorIdAsync(id);

            if (categoria == null)
                return null;

            return new CategoriaDto
            {
                CategoriaId = categoria.CategoriaId,
                Nombre = categoria.Nombre,
                Descripcion = categoria.Descripcion
            };
        }

        public async Task EditarAsync(CategoriaDto dto)
        {
            var categoria = await _repository.ObtenerPorIdAsync(dto.CategoriaId);

            if (categoria == null)
                return;

            categoria.Nombre = dto.Nombre;
            categoria.Descripcion = dto.Descripcion;

            await _repository.ActualizarAsync(categoria);
            await _repository.GuardarCambiosAsync();
        }

        public async Task EliminarAsync(int id)
        {
            var categoria = await _repository.ObtenerPorIdAsync(id);

            if (categoria == null)
                return;

            await _repository.EliminarAsync(categoria);
            await _repository.GuardarCambiosAsync();
        }
    }
}