using FilmCritique.Entities.Model.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FilmCritique.DAL.Repository.Abstract
{

    public interface IRepository<T> where T : BaseEntity
    {

        Task<int> InsertAsync(T input);
        Task<int> UpdateAsync(T input);
        Task<int> DeleteAsync(T input);
        Task<int> DeleteByIdAsync(int id);
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null);
        public List<T>? GetAll(Expression<Func<T, bool>> predicate = null);
        Task<T> GetByIdAsync(int id);
        Task<T> GetAsync(Expression<Func<T, bool>> predicate = null);
        IQueryable<T> GetAllInclude(params Expression<Func<T, object>>[] includes);
    }
}
