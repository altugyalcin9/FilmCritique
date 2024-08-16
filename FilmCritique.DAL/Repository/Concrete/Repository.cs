using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FilmCritique.DAL.Repository.Abstract;
using FilmCritique.Entities.DbContexts;
using FilmCritique.Entities.Model.Abstract;
using Microsoft.EntityFrameworkCore;

namespace FilmCritique.DAL.Repository.Concrete
{
    public class Repository<T>: IRepository<T> where T : BaseEntity
    {
        readonly AppDbContext _dbContext;
        readonly DbSet<T> _dbSet;
        public Repository()
        {
            _dbContext = new AppDbContext();
            _dbSet = _dbContext.Set<T>();
        }

        public virtual async Task<int> InsertAsync(T input)
        {
            /*
             * Burada ki Set<T> metodu 
             * dbcontext uzerindeki tanimli olan DbSet<T> propertiy'sine 
             * konumlanir
             * 
             */
            await _dbContext.Set<T>().AddAsync(input);
            // _dbContext.Urunler.Add(input);
            return _dbContext.SaveChanges();
        }

        public virtual async Task<int> UpdateAsync(T input)
        {
            _dbSet.Update(input);
            return await _dbContext.SaveChangesAsync();
        }
        public virtual async Task<int> DeleteAsync(T input)
        {
            _dbSet.Remove(input);
            return await _dbContext.SaveChangesAsync();
        }

        public virtual async Task<int> DeleteByIdAsync(int id)
        {
            var silinecek = _dbSet.Find(id);
            _dbSet.Remove(silinecek);
            return await _dbContext.SaveChangesAsync();
        }

        public virtual async Task<T?> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().Where(predicate).FirstOrDefaultAsync();

        }

        public virtual async Task<List<T>?> GetAllAsync(Expression<Func<T, bool>> predicate = null)
        {
            if (predicate != null)
                return await _dbContext.Set<T>().Where(predicate).ToListAsync();
            else
                return await _dbContext.Set<T>().ToListAsync();
        }

        public virtual async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }
        public IQueryable<T> GetAllInclude(params Expression<Func<T, object>>[] includes)
        { // 0505 806 1986 Bilge Pınar Oğuz kariyer eğitmen
            IQueryable<T> query = _dbSet;
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            return query;
        }

        public List<T>? GetAll(Expression<Func<T, bool>> predicate = null)
        {
            if (predicate != null)
                return _dbContext.Set<T>().Where(predicate).ToList();
            else
                return _dbContext.Set<T>().ToList();
        }


    }
}
