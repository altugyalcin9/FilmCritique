using FilmCritique.DAL.Repository.Abstract;
using FilmCritique.Entities.Model.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmCritique.BL.Managers.Abstract
{
    public interface IManager<T>:IRepository<T> where T : BaseEntity
    {
    }
}
