using DesafioTJRJ.Business.Interfaces.Repository;
using DesafioTJRJ.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DesafioTJRJ.Data.BaseRepository.Base
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly LibraryContext _context;
        private readonly DbSet<T> _dbSet;

        public BaseRepository(LibraryContext context)
        {
            this._context = context;
            this._dbSet = _context.Set<T>();
        }

        public IQueryable<T> GetAll(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return query;
        }

        public async Task<IQueryable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
        {
            var query = this.GetAll(includes);
            return await Task.FromResult(query);
        }

        public async Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes)
        {
            var query = await GetAllAsync(includes);

            var keyName = _context.Model.FindEntityType(typeof(T)).FindPrimaryKey().Properties.First().Name;
            var parameter = Expression.Parameter(typeof(T), "e");
            var property = Expression.Property(parameter, keyName);
            var equals = Expression.Equal(property, Expression.Constant(id));
            var lambda = Expression.Lambda<Func<T, bool>>(equals, parameter);

            return await query.FirstOrDefaultAsync(lambda);
        }

        public async Task AddAsync(T entity)
        {
            await this._dbSet.AddAsync(entity);
            await this._context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            this._context.Entry(entity).State = EntityState.Modified;
            await this._context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await this._dbSet.FindAsync(id);
            if (entity != null)
            {
                this._dbSet.Remove(entity);
                await this._context.SaveChangesAsync();
            }
        }
    }
}
