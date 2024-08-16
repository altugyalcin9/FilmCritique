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
    public class MovieConfig : BaseConfig<Movie>
    {
        public override void Configure(EntityTypeBuilder<Movie> builder)
        {
            base.Configure(builder);
            builder.HasData(
                        new Movie
                        {
                            Id = 1,
                            MovieName = "Iron Man",
                            ReleaseDate = new DateTime(2008, 5, 2),
                            TeaserUrl = "https://www.youtube.com/watch?v=8ugaeA-nMTc",
                            PhotoUrl = "ironman.jpg",
                            Duration = 126,
                            Description = "After being held captive in an Afghan cave, billionaire engineer Tony Stark creates a unique weaponized suit of armor to fight evil.",
                            AdminRating = 7.9
                        },
                        new Movie
                        {
                            Id = 2,
                            MovieName = "Captain America: The First Avenger",
                            ReleaseDate = new DateTime(2011, 7, 22),
                            TeaserUrl = "https://www.youtube.com/watch?v=JerVrbLldXw",
                            PhotoUrl = "captanAmericaFirstAvanger.jpg",
                            Duration = 124,
                            Description = "Steve Rogers, a rejected military soldier transforms into Captain America after taking a dose of a 'Super-Soldier serum'.",
                            AdminRating = 6.9
                        },
                        new Movie
                        {
                            Id = 3,
                            MovieName = "The Avengers",
                            ReleaseDate = new DateTime(2012, 5, 4),
                            TeaserUrl = "https://www.youtube.com/watch?v=eOrNdBpGMv8",
                            PhotoUrl = "theAvangers.jpg",
                            Duration = 143,
                            Description = "Earth's mightiest heroes must come together and learn to fight as a team if they are going to stop the mischievous Loki and his alien army from enslaving humanity.",
                            AdminRating = 8.0
                        },
                        new Movie
                        {
                            Id = 4,
                            MovieName = "Thor",
                            ReleaseDate = new DateTime(2011, 5, 6),
                            TeaserUrl = "https://www.youtube.com/watch?v=v7MGUNV8MxU",
                            PhotoUrl = "thor.jpg",
                            Duration = 115,
                            Description = "The powerful but arrogant god Thor is cast out of Asgard to live amongst humans in Midgard (Earth), where he soon becomes one of their finest defenders.",
                            AdminRating = 7.0
                        },
                        new Movie
                        {
                            Id = 5,
                            MovieName = "Hulk",
                            ReleaseDate = new DateTime(2003, 6, 20),
                            TeaserUrl = "https://www.youtube.com/watch?v=xbqNb2PFKKA",
                            PhotoUrl = "hulk.jpg",
                            Duration = 138,
                            Description = "Bruce Banner, a genetics researcher with a tragic past, suffers an accident that causes him to transform into a raging green monster when he gets angry.",
                            AdminRating = 6.7
                        }
                    );
        }
    }
}
