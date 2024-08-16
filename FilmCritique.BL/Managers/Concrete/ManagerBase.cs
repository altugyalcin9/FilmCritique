using FilmCritique.BL.Managers.Abstract;
using FilmCritique.DAL.Repository.Concrete;
using FilmCritique.Entities.DbContexts;
using FilmCritique.Entities.Model.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmCritique.BL.Managers.Concrete
{
    public class ManagerBase<T>:Repository<T>, IManager<T> where T : BaseEntity
    {
        private AppDbContext context;
        public ManagerBase(AppDbContext context)
        {
            this.context = context;
        }
    }
}
