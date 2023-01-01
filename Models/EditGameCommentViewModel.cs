using System.ComponentModel.DataAnnotations;

namespace GameRating.Models
{
    public class EditGameCommentViewModel
    {
       
        [Required]
        public string? Content { get; set; }
        [Range(1, 5)]
        public int Rating { get; set; }
    }
}