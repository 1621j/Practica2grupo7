using System.Linq.Expressions;

namespace Practica2Grupo7.DAL.Repositorios
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> ObtenerTodosAsync();
        Task<T?> ObtenerPorIdAsync(int id);
        Task<List<T>> BuscarAsync(Expression<Func<T, bool>> filtro);
        Task AgregarAsync(T entidad);
        Task ActualizarAsync(T entidad);
        Task EliminarAsync(T entidad);
        Task GuardarCambiosAsync();
    }
}