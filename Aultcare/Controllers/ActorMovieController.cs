using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Aultcare.DAL;
using Aultcare.Models;

namespace Aultcare.Controllers
{
    public class ActorMovieController : Controller
    {
        private MovieContext db = new MovieContext();

        // GET: ActorMovie/Create
        public ActionResult Create()
        {
            ViewBag.ActorID = new SelectList(db.Actors, "ActorID", "ActorName");
            ViewBag.MovieID = new SelectList(db.Movies, "MovieID", "MovieName");
            return View();
        }

        // POST: ActorMovie/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MovieID,ActorID,CharacterName,CharacterType")] ActorMovie actorMovie)
        {

            try
            {

                if (ModelState.IsValid)
                {

                    db.ActorMovies.Add(actorMovie);
                    db.SaveChanges();
                    return RedirectToAction("AdminIndex", "Movie");

                }

            }

            catch (DataException)
            {

                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");

            }

            ViewBag.ActorID = new SelectList(db.Actors, "ActorID", "ActorName", actorMovie.ActorID);
            ViewBag.MovieID = new SelectList(db.Movies, "MovieID", "MovieName", actorMovie.MovieID);
            return View(actorMovie);

        }

        // GET: ActorMovie/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActorMovie actorMovie = db.ActorMovies.Find(id);
            if (actorMovie == null)
            {
                return HttpNotFound();
            }
            ViewBag.ActorID = new SelectList(db.Actors, "ActorID", "ActorName", actorMovie.ActorID);
            ViewBag.MovieID = new SelectList(db.Movies, "MovieID", "MovieName", actorMovie.MovieID);
            return View(actorMovie);
        }

        // POST: ActorMovie/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        {

            if (id == null)
            {

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }

            var actorMovieToUpdate = db.ActorMovies.Find(id);

            if (TryUpdateModel(actorMovieToUpdate, "", new string[] { "MovieID", "ActorID", "CharacterName", "CharacterType" }))
            {

                try
                {

                    if (ModelState.IsValid)
                    {

                        db.SaveChanges();

                        return RedirectToAction("AdminIndex", "Movie");

                    }

                }

                catch (DataException)
                {

                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");

                }

            }
            ViewBag.ActorID = new SelectList(db.Actors, "ActorID", "ActorName", actorMovieToUpdate.ActorID);
            ViewBag.MovieID = new SelectList(db.Movies, "MovieID", "MovieName", actorMovieToUpdate.MovieID);
            return View(actorMovieToUpdate);
        }

        // GET: ActorMovie/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActorMovie actorMovie = db.ActorMovies.Find(id);
            if (actorMovie == null)
            {
                return HttpNotFound();
            }
            return View(actorMovie);
        }

        // POST: ActorMovie/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {

            try
            {

                ActorMovie actorMovie = db.ActorMovies.Find(id);
                db.ActorMovies.Remove(actorMovie);
                db.SaveChanges();

            }

            catch (DataException)
            {

                return RedirectToAction("Delete", new { id = id, saveChangesError = true });

            }

            return RedirectToAction("AdminIndex", "Movie");

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
