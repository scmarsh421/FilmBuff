using FilmBuff.Models.Comment;
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
    public class CommentController : Controller
    {
        // GET: Comment Index
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new CommentService(userId);
            var model = service.GetComments();
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
        public ActionResult Create(CommentCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateCommentService();

            if (service.CreateComment(model))
            {
                TempData["SaveResult"] = "Your comment was created.";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Comment could not be created");
            return View(model);
        }

        //GET: Details
        public ActionResult Details(int id)
        {
            var svc = CreateCommentService();
            var model = svc.GetCommentById(id);
            return View(model);
        }

        // GET: Edit
        public ActionResult Edit(int id)
        {
            var service = CreateCommentService();
            var detail = service.GetCommentById(id);
            var model =
                new CommentEdit
                {
                    CommentId = detail.CommentId,
                    Content = detail.Content,
                };
            return View(model);
        }

        //POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CommentEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.CommentId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }
            var service = CreateCommentService();

            if (service.UpdateComment(model))
            {
                TempData["SaveResult"] = "Your comment was updated.";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Comment could not be updated");
            return View(model);
        }

        //GET: Delete
        public ActionResult Delete(int id)
        {
            var svc = CreateCommentService();
            var model = svc.GetCommentById(id);
            return View(model);
        }

        //POST: Delete
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteComment(int id)
        {
            var service = CreateCommentService();

            service.DeleteComment(id);

            TempData["SaveResult"] = "Your comment was deleted";

            return RedirectToAction("Index");
        }

        private CommentService CreateCommentService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new CommentService(userId);
            return service;
        }


    }
}