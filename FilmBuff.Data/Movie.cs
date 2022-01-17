using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmBuff.Data
{
    public class Movie
    {
        [Key]
        public int MovieId { get; set; }
        [Required]
        public Guid OwnerId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        [MaxLength(4, ErrorMessage ="Too many characters in this field.")]
        public int Year { get; set; }
        [Required]
        public string DirectedBy { get; set; }
        public virtual List<Review> Reviews { get; set; } = new List<Review>();
        [Required]
        public DateTimeOffset CreatedUtc { get; set; }
        public DateTimeOffset? ModifiedUtc { get; set; }

    }
}
