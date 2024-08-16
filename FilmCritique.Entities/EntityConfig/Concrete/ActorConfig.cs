using FilmCritique.Entities.EntityConfig.Abstract;
using FilmCritique.Entities.Model.Concrete;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmCritique.Entities.EntityConfig.Concrete
{
    public class ActorConfig : BaseConfig<Actor>
    {
        public override void Configure(EntityTypeBuilder<Actor> builder)
        {
            base.Configure(builder);

            builder.HasData(
            new Actor { Id = 1, FullName = "Robert Downey Jr." },
            new Actor { Id = 2, FullName = "Chris Evans" },
            new Actor { Id = 3, FullName = "Scarlett Johansson" },
            new Actor { Id = 4, FullName = "Mark Ruffalo" },
            new Actor { Id = 5, FullName = "Chris Hemsworth" }
        );
        }
        
    }
}
