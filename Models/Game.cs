using System.ComponentModel.DataAnnotations;

namespace GameRating.Models
{
    public class Game
    {
        [Key]
        public int GameId { get; set; }
        [Required]
        public string? Title { get; set; }
        [Required]
        public string? Detail { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.UtcNow;

         [Display(Name = "Banner")]
        [DataType(DataType.Upload)]
        public string? Banner { get; set; }

        public List<GameComment>? GameComments { get; set; }

    }
}