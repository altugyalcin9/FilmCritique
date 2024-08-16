using FilmCritique.Entities.EntityConfig.Abstract;
using FilmCritique.Entities.Model.Concrete;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmCritique.Entities.EntityConfig.Concrete
{
    public class MovieDirectorConfig : BaseConfig<MovieDirector>
    {
        public override void Configure(EntityTypeBuilder<MovieDirector> builder)
        {
            base.Configure(builder);
            builder.HasData(
            new MovieDirector { Id = 1, DirectorId = 1, MovieId = 1 }, // Example data
            new MovieDirector { Id = 2, DirectorId = 2, MovieId = 2 }, // Example data
            new MovieDirector { Id = 3, DirectorId = 3, MovieId = 3 }, // Example data
            new MovieDirector { Id = 4, DirectorId = 4, MovieId = 4 }, // Example data
            new MovieDirector { Id = 5, DirectorId = 5, MovieId = 5 }  // Example data
        );
        }
    }
}
