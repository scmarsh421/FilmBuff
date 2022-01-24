using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmBuff.Models.Comment
{
    public class CommentEdit
    {
        public int CommentId { get; set; }
        [Required]
        [MinLength(2, ErrorMessage = "Please enter at least 2 characters.")]
        public string Content { get; set; }
        public int ReviewId { get; set; }
    }
}
