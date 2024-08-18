using FilmCritique.DAL.Repository.Abstract;
using FilmCritique.Entities.Model.Abstract;
using FilmCritique.Entities.Model.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FilmCritique.BL.Managers.Abstract
{
    public interface IMovieManager : IManager<Movie>
    {

        Task<Movie> GetMovieByIdAsync(int id);
        Task<List<UserReview>> GetCommentsByMovieIdAsync(int movieId);

    }
}
