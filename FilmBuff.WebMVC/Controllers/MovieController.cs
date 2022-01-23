using FilmBuff.Models.Movie;
using FilmBuff.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FilmBuff.WebMVC.Controllers
{  
    [Authorize]
    public class MovieController : Controller
    {
        // GET: Movie Index
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new MovieService(userId);
            var model = service.GetMovies();
            return View(model);
        }

        //GET: Create
        public ActionResult Create()
        {
            return View();
        }

        //POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MovieCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateMovieService();

            if (service.CreateMovie(model))
            {
                TempData["SaveResult"] = "Your movie was created.";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Movie could not be created");
            return View(model);
        }
        //GET: Details
        public ActionResult Details(int id)
        {
            var svc = CreateMovieService();
            var model = svc.GetMovieById(id);
            return View(model);
        }

        // GET: Edit
        public ActionResult Edit(int id)
        {
            var service = CreateMovieService();
            var detail = service.GetMovieById(id);
            var model =
                new MovieEdit
                {
                    MovieId = detail.MovieId,
                    Title = detail.Title,
                    Year = detail.Year,
                    DirectedBy = detail.DirectedBy,
                };
            return View(model);
        }
        //POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, MovieEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.MovieId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }
            var service = CreateMovieService();

            if (service.UpdateMovie(model))
            {
                TempData["SaveResult"] = "Your movie was updated.";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Movie could not be updated");
            return View(model);
        }

        //GET: Deelte
        public ActionResult Delete(int id)
        {
            var svc = CreateMovieService();
            var model = svc.GetMovieById(id);
            return View(model);
        }
        //POST: Delete
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteMovie(int id)
        {
            var service = CreateMovieService();

            service.DeleteMovie(id);

            TempData["SaveResult"] = "Your movie was deleted";

            return RedirectToAction("Index");
        }
        private MovieService CreateMovieService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new MovieService(userId);
            return service;
        }
    }
}