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
    public class DirectorConfig : BaseConfig<Director>
    {
        public override void Configure(EntityTypeBuilder<Director> builder)
        {
            base.Configure(builder);
            builder.HasData(
            new Director { Id = 1, FullName = "Jon Favreau" },       // Director of Iron Man
            new Director { Id = 2, FullName = "Joe Johnston" },      // Director of Captain America: The First Avenger
            new Director { Id = 3, FullName = "Joss Whedon" },       // Director of The Avengers
            new Director { Id = 4, FullName = "Kenneth Branagh" },   // Director of Thor
            new Director { Id = 5, FullName = "Ang Lee" }            // Director of Hulk
            );
        }
    }
}
