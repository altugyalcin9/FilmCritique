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
    public class CategoryConfig : BaseConfig<Category>
    {
        public override void Configure(EntityTypeBuilder<Category> builder)
        {
            base.Configure(builder);
            builder.HasData(
     new Category { Id = 1, Name = "Action" },
     new Category { Id = 2, Name = "Drama" },
     new Category { Id = 3, Name = "Comedy" },
     new Category { Id = 4, Name = "Horror" },
     new Category { Id = 5, Name = "Sci-Fi" },
     new Category { Id = 6, Name = "Romance" },
     new Category { Id = 7, Name = "Thriller" },
     new Category { Id = 8, Name = "Adventure" },
     new Category { Id = 9, Name = "Mystery" },
     new Category { Id = 10, Name = "Fantasy" },
     new Category { Id = 11, Name = "Animation" },
     new Category { Id = 12, Name = "Documentary" },
     new Category { Id = 13, Name = "Historical" },
     new Category { Id = 14, Name = "Musical" },
     new Category { Id = 15, Name = "Family" },
     new Category { Id = 16, Name = "Crime" }
          );
        }
    }
}

