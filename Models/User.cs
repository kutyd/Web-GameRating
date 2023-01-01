using System.ComponentModel.DataAnnotations;

namespace GameRating.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }  

        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public DateTime? CreatedDatetime { get; set; } = DateTime.UtcNow;

        [Required]
        public String Role { get; set; } = "User";


    }
}