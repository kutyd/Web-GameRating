
using System.ComponentModel.DataAnnotations;

namespace GameRating.Models
{
    public class GameComment
    {
        [Key]
        public int CommentId { get; set; }
        public string? Content { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; } = 1;

        public DateTime? CreatedDate { get; set; } = DateTime.UtcNow;

        public int GameId { get; set; }
        public Game? Game { get; set; }

        public Guid? UserId { get; set; }
        public User? User { get; set; }

    }
}
