using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FilmCritique.Model.ViewModels
{
    public class MovieViewModel
    {
        [Required]
        public string MovieName { get; set; }

        [Required]
        public DateTime ReleaseDate { get; set; }

        public string? TeaserUrl { get; set; }

        [Required]
        public IFormFile? PhotoUrl { get; set; } 

        [Required]
        public int Duration { get; set; }

        [Required]
        public string Description { get; set; }
        public double AdminRating { get; set; }

        public List<int> SelectedActorIds { get; set; } = new List<int>();
        public List<int> SelectedDirectorIds { get; set; } = new List<int>();
        public List<int> SelectedCategoryIds { get; set; } = new List<int>();
    }
}