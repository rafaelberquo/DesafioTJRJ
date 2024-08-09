using DesafioTJRJ.Business.Interfaces.Repository;
using DesafioTJRJ.Business.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DesafioTJRJ.Business.Services.Base
{
    public class BaseService<T> : IBaseService<T> where T : class
    {
        private readonly IBaseRepository<T> _repository;

        protected BaseService(IBaseRepository<T> repository)
        {
            _repository = repository;
        }

        public async Task<IQueryable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
        {
            return await _repository.GetAllAsync(includes);
        }

        public async Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes)
        {
            return await _repository.GetByIdAsync(id, includes);
        }

        public async Task AddAsync(T entity)
        {
            await _repository.AddAsync(entity);
        }

        public async Task UpdateAsync(T entity)
        {
            await _repository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}