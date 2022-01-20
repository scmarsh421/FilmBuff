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
                    DirectedBy = model.DirectedBy,
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
        public MovieDetail GetMovieById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Movies
                    .Single(e => e.MovieId == id && e.OwnerId == _userId);
                return
                    new MovieDetail
                    {
                        MovieId = entity.MovieId,
                        Title = entity.Title,
                        Year = entity.Year,
                        DirectedBy = entity.DirectedBy,
                        Reviews = entity.Reviews,
                        CreatedUtc = entity.CreatedUtc,
                        ModifiedUtc = entity.ModifiedUtc
                    };
            }
        }
        public bool UpdateMovie(MovieEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Movies
                        .Single(e => e.MovieId == model.MovieId && e.OwnerId == _userId);

                entity.Title = model.Title;
                entity.Year = model.Year;
                entity.DirectedBy = model.DirectedBy;
                entity.ModifiedUtc = DateTimeOffset.UtcNow;

                return ctx.SaveChanges() == 1;
            }
        }
        public bool DeleteMovie(int movieId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Movies
                        .Single(e => e.MovieId == movieId && e.OwnerId == _userId);

                ctx.Movies.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
