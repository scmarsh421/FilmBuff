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
    public class MovieController : Controller
    {
        // GET: Movie
        //[Authorize]
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new MovieService(userId);
            var model = service.GetMovies();
            return View(model);
        }
        //GET: Create View
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MovieCreate model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new MovieService(userId);

            service.CreateMovie(model);

            return RedirectToAction("Index");
        }
    }
}