using FilmBuff.Models.Review;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmBuff.Models.Comment
{
    public class CommentListItem
    {
        public int CommentId { get; set; }
        [ForeignKey("Review")]
        public int ReviewId { get; set; }
        public virtual ReviewListItem Review { get; set; }
        public string Content { get; set; }
        [Display(Name = "Created")]
        public DateTimeOffset CreatedUtc { get; set; }
    }
}
