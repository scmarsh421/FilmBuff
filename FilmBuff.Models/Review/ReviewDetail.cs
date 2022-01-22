using FilmBuff.Models.Movie;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmBuff.Models.Review
{
    public class ReviewDetail
    {
        public int ReviewId { get; set; }
        [ForeignKey("Movie")]
        public int MovieId { get; set; }
        public virtual MovieListItem Movie { get; set; }
        public string Content { get; set; }
        [Display(Name = "Created")]
        public DateTimeOffset CreatedUtc { get; set; }
        [Display(Name = "Modified")]
        public DateTimeOffset? ModifiedUtc { get; set; }
    }
}
