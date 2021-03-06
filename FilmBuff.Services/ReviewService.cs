using FilmBuff.Data;
using FilmBuff.Models;
using FilmBuff.Models.Movie;
using FilmBuff.Models.Review;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmBuff.Services
{
    public class ReviewService
    {
        private readonly Guid _userId;
        public ReviewService(Guid userId)
        {
            _userId = userId;
        }
        public bool CreateReview(ReviewCreate model)
        {
            var entity =
                new Review()
                {
                    OwnerId = _userId,
                    Content = model.Content,
                    MovieId = model.MovieId,
                    CreatedUtc = DateTimeOffset.Now
                };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Reviews.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }
        public IEnumerable<ReviewListItem> GetReviews()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Reviews
                        .Where(e => e.OwnerId == _userId)
                        .Select(
                            e =>
                                new ReviewListItem
                                {
                                    ReviewId = e.ReviewId,
                                    MovieId = e.MovieId,
                                    Content = e.Content,
                                    CreatedUtc = e.CreatedUtc,
                                    Movie = new MovieListItem
                                    {
                                       Title = e.Movie.Title
                                    }
                                }
                        );

                return query.ToArray();
            }
        }
        public ReviewDetail GetReviewById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Reviews
                    .Single(e => e.ReviewId == id && e.OwnerId == _userId);
                var movieEntity =
                    ctx
                    .Movies
                    .Single(e => e.MovieId == entity.MovieId);
                MovieDetail movieDetail = new MovieDetail();
                movieDetail.MovieId = movieEntity.MovieId;
                movieDetail.Title = movieEntity.Title;
                return
                    new ReviewDetail
                    {
                        ReviewId = entity.ReviewId,
                        MovieId = entity.MovieId,
                        Content = entity.Content,
                        CreatedUtc = entity.CreatedUtc,
                        ModifiedUtc = entity.ModifiedUtc,
                        Movie = movieDetail
                    };
            }
        }
        public bool UpdateReview(ReviewEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Reviews
                        .Single(e => e.ReviewId == model.ReviewId && e.OwnerId == _userId);

                entity.Content = model.Content;
                entity.ModifiedUtc = DateTimeOffset.UtcNow;

                return ctx.SaveChanges() == 1;
            }
        }
        public bool DeleteReview(int reviewId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Reviews
                        .Single(e => e.ReviewId == reviewId && e.OwnerId == _userId);

                ctx.Reviews.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}

