using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace GameRating.Models
{
    public class EditUserViewModel
    {
        public string Username { get; set; }
        
        [Required]
        public String Role { get; set; } = "User";

    }
}