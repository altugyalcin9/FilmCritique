using FilmCritique.Entities.Model.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmCritique.Entities.Model.Concrete
{
    public class Director:BaseEntity
    {
        public string FullName { get; set; }

        public ICollection<MovieDirector> MovieDirectors { get; set; }

    }
}
