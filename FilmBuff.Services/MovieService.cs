using FilmBuff.Data;
using FilmBuff.Models.Movie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmBuff.Services
{
    public class MovieService
    {
        private readonly Guid _userId;
        public MovieService(Guid userId)
        {
            _userId = userId;
        }
        public bool CreateMovie(MovieCreate model)
        {
            var entity =
                new Movie()
                {
                    OwnerId = _userId,
                    Title = model.Title,
                    Year = model.Year,
                    CreatedUtc = DateTimeOffset.Now
                };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Movies.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }
        public IEnumerable<MovieListItem> GetMovies()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Movies
                        .Where(e => e.OwnerId == _userId)
                        .Select(
                            e =>
                                new MovieListItem
                                {
                                    MovieId = e.MovieId,
                                    Title = e.Title,
                                    Year = e.Year,
                                    DirectedBy = e.DirectedBy,
                                    CreatedUtc = e.CreatedUtc
                                }
                        );

                return query.ToArray();
            }
        }
    }
}
