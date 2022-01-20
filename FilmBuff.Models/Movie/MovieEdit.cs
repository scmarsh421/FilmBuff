using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmBuff.Models.Movie
{
    public class MovieEdit
    {
        public int MovieId { get; set; }
        public string Title { get; set; }
        public string Year { get; set; }
        public string DirectedBy { get; set; }

    }
}
