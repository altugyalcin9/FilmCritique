using System.ComponentModel.DataAnnotations;

namespace FilmCritique.ViewModels
{
    public class CreateReviewViewModel
    {
        [Required]
        public string Comment { get; set; }

        [Required]
        [Range(1, 10, ErrorMessage = "Rating must be between 1 and 10.")]
        public double Rating { get; set; }
    }
}
