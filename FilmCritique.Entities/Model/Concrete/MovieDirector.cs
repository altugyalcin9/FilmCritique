using FilmCritique.Entities.Model.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmCritique.Entities.Model.Concrete
{
    public class MovieDirector:BaseEntity
    {
        public int DirectorId { get; set; }
        public Director Director { get; set; }

        public int MovieId { get; set; }
        public Movie Movie { get; set; }
    }
}
