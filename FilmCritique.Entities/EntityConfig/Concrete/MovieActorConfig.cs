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
    public class MovieActorConfig : BaseConfig<MovieActor>
    {
        public override void Configure(EntityTypeBuilder<MovieActor> builder)
        {
            base.Configure(builder);
            builder.HasData(
            new MovieActor { Id = 1, ActorId = 1, MovieId = 1 }, // Robert Downey Jr. in Iron Man
            new MovieActor { Id = 2, ActorId = 2, MovieId = 2 }, // Chris Evans in Captain America: The First Avenger
            new MovieActor { Id = 3, ActorId = 1, MovieId = 3 }, // Robert Downey Jr. in The Avengers
            new MovieActor { Id = 4, ActorId = 2, MovieId = 3 }, // Chris Evans in The Avengers
            new MovieActor { Id = 5, ActorId = 3, MovieId = 3 }, // Scarlett Johansson in The Avengers
            new MovieActor { Id = 6, ActorId = 4, MovieId = 3 }, // Mark Ruffalo in The Avengers
            new MovieActor { Id = 7, ActorId = 5, MovieId = 3 }, // Chris Hemsworth in The Avengers
            new MovieActor { Id = 8, ActorId = 4, MovieId = 5 }, // Mark Ruffalo in Hulk
            new MovieActor { Id = 9, ActorId = 5, MovieId = 4 }  // Chris Hemsworth in Thor
        );
        }
    }
}
