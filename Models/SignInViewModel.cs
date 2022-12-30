using System.ComponentModel.DataAnnotations;

namespace GameRating.Models
{
    public class SignInViewModel
    {

        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "The password is required")]
        [DataType(DataType.Password)]
        [MinLength(3, ErrorMessage = "Password must be at least 3 characters long")]
        public string Password { get; set; }
        
       
    }

}