using Microsoft.EntityFrameworkCore;
using Practica2Grupo7.DAL.Data;
using System.Linq.Expressions;

namespace Practica2Grupo7.DAL.Repositorios
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository()
        {
            _context = new AppDbContext();
            _dbSet = _context.Set<T>();
        }

        public async Task<List<T>> ObtenerTodosAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T?> ObtenerPorIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<List<T>> BuscarAsync(Expression<Func<T, bool>> filtro)
        {
            return await _dbSet.Where(filtro).ToListAsync();
        }

        public async Task AgregarAsync(T entidad)
        {
            await _dbSet.AddAsync(entidad);
        }

        public Task ActualizarAsync(T entidad)
        {
            _dbSet.Update(entidad);
            return Task.CompletedTask;
        }

        public Task EliminarAsync(T entidad)
        {
            _dbSet.Remove(entidad);
            return Task.CompletedTask;
        }

        public async Task GuardarCambiosAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}