using System.ComponentModel.DataAnnotations;

namespace GameRating.Models
{
    public class SignUpViewModel
    {

        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "The password is required")]
        [DataType(DataType.Password)]
        [MinLength(3, ErrorMessage = "Password must be at least 3 characters long")]
        public string Password { get; set; }
        
        [DataType(DataType.Password)]
        [MinLength(3, ErrorMessage = "Re-Password must be at least 3 characters long")]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string RePassword { get; set; }

    }

}