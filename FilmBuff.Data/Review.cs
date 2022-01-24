using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmBuff.Data
{
    public class Review
    {
        [Key]
        public int ReviewId { get; set; }
        [Required]
        public Guid OwnerId { get; set; }
        [Required]
        [ForeignKey("Movie")]
        public int MovieId { get; set; }
        //[Display(Name = "Movie")]
        public virtual Movie Movie { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public DateTimeOffset CreatedUtc { get; set; }
        public DateTimeOffset? ModifiedUtc { get; set; }
    }
}
