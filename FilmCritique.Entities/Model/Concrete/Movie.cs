using FilmCritique.Entities.Model.Abstract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmCritique.Entities.Model.Concrete
{
    public class Movie : BaseEntity
    {
        public string MovieName { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string? TeaserUrl { get; set; }
        public string PhotoUrl { get; set; }
        public int Duration { get; set; }
        public string Description { get; set; }
        public double AdminRating { get; set; }

        public ICollection<MovieActor> MovieActors { get; set; }

        public ICollection<MovieDirector> MovieDirectors { get; set; }

        public ICollection<MovieCategory> MovieCategories { get; set; }

        public ICollection<UserReview>? UserReviews { get; set; }

        public double AverageRating
        {
            get
            {
                return UserReviews.Any() ? UserReviews.Average(r => r.Rating) : 0;
            }


        }
    }
}
