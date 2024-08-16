using FilmCritique.DAL.Repository.Abstract;
using FilmCritique.Entities.Model.Abstract;
using FilmCritique.Entities.Model.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmCritique.BL.Managers.Abstract
{
    public interface IMovieCategoryManager:IManager<MovieCategory>
    {
        Task UpdateMovieCategoriesAsync(int movieId, List<int> categoryIds);

    }
}
