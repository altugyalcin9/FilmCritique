using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace FilmCritique.Models.ViewModels
{
    public class MovieEditViewModel
    {
        #region eski  
        //public int Id { get; set; }  // Film kimliği

        //[Required]
        //public string MovieName { get; set; }

        //[Required]
        //public DateTime ReleaseDate { get; set; }

        //public string? TeaserUrl { get; set; }

        //[Required]
        //public IFormFile? PhotoUrl { get; set; }  // Yüklenen yeni fotoğraf dosyası

        //public string ExistingPhoto { get; set; }  // Mevcut fotoğrafın yolu

        //[Required]
        //public int Duration { get; set; }

        //[Required]
        //public string Description { get; set; }
        //public double AdminRating { get; set; }

        //// Seçilen aktörlerin, yönetmenlerin ve kategorilerin ID'lerini tutar
        //public List<int> SelectedActorIds { get; set; } = new List<int>();
        //public List<int> SelectedDirectorIds { get; set; } = new List<int>();
        //public List<int> SelectedCategoryIds { get; set; } = new List<int>();

        //// Görünümde gösterilecek tüm aktörleri, yönetmenleri ve kategorileri tutar
        //public IEnumerable<SelectListItem> Actors { get; set; }
        //public IEnumerable<SelectListItem> Directors { get; set; }
        //public IEnumerable<SelectListItem> Categories { get; set; } 
        #endregion



        public int Id { get; set; }

        [StringLength(100)]
        public string? MovieName { get; set; }

        public DateTime? ReleaseDate { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        public string? TeaserUrl { get; set; }

        public int? Duration { get; set; }

        [Range(0, 10)]
        public double? AdminRating { get; set; }

        public IFormFile? PhotoUrl { get; set; }

        public string? ExistingPhoto { get; set; }

        public List<int>? SelectedActorIds { get; set; }

        public List<int>? SelectedDirectorIds { get; set; }

        public List<int>? SelectedCategoryIds { get; set; }

        public IEnumerable<SelectListItem>? Categories { get; set; }

        public IEnumerable<SelectListItem>? Actors { get; set; }

        public IEnumerable<SelectListItem>? Directors { get; set; }


    }
}
