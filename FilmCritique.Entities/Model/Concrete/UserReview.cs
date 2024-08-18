using FilmCritique.Entities.Model.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmCritique.Entities.Model.Concrete
{
    public class UserReview : BaseEntity
    {
        public string Comment { get; set; }
        public double Rating { get; set; }


        public int MovieId { get; set; }
        public Movie Movie { get; set; }
        public string UserId { get; set; }
        //public User User { get; set; }


    }
}