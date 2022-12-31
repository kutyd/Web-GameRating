using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace GameRating.Models
{
    public class EditGameViewModel
    {
        [Required]
        public string? Title { get; set; }
        [Required]
        public string? Detail { get; set; }

        [DataType(DataType.Upload)]
        public string? Banner { get; set; }

    }
}