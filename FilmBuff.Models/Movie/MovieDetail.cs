﻿using FilmBuff.Data; //check this again later
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmBuff.Models.Movie
{
    public class MovieDetail
    {
        public int MovieId { get; set; }
        public string Title { get; set; }
        public string Year { get; set; }
        [Display(Name = "Directed by")]
        public string DirectedBy { get; set; }
        public virtual List<Review> Reviews { get; set; } = new List<Review>(); //check
        [Display(Name = "Created")]
        public DateTimeOffset CreatedUtc { get; set; }
        [Display(Name = "Modified")]
        public DateTimeOffset? ModifiedUtc { get; set; }
    }
}
