using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmBuff.Models.Movie
{
    public class MovieCreate
    {
        [Required]
        public string Title { get; set; }
        [Required]
        [MaxLength(4, ErrorMessage = "Too many characters in this field.")]
        public string Year { get; set; }
        [Required]
        [Display(Name = "Directed by")]
        public string DirectedBy { get; set; }
    }
}
