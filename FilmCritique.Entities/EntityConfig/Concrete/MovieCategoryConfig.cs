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
    public class MovieCategoryConfig : BaseConfig<MovieCategory>
    {
        public override void Configure(EntityTypeBuilder<MovieCategory> builder)
        {
            base.Configure(builder);
            builder.HasData(
            new MovieCategory { Id = 1, CategoryId = 1, MovieId = 1 }, // Example data
            new MovieCategory { Id = 2, CategoryId = 2, MovieId = 2 }, // Example data
            new MovieCategory { Id = 3, CategoryId = 3, MovieId = 3 }, // Example data
            new MovieCategory { Id = 4, CategoryId = 4, MovieId = 4 }, // Example data
            new MovieCategory { Id = 5, CategoryId = 5, MovieId = 5 }  // Example data
        );

        }
    }
}
