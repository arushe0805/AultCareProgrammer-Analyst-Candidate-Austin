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
    public class GenreMovieController : Controller
    {
        private MovieContext db = new MovieContext();

        // GET: GenreMovie/Create
        public ActionResult Create()
        {
            ViewBag.GenreID = new SelectList(db.Genres, "GenreID", "GenreName");
            ViewBag.MovieID = new SelectList(db.Movies, "MovieID", "MovieName");
            return View();
        }

        // POST: GenreMovie/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MovieID,GenreID,GenreRatingForMovie")] GenreMovie genreMovie)
        {

            try
            {

                if (ModelState.IsValid)
                {

                    db.GenreMovies.Add(genreMovie);
                    db.SaveChanges();
                    return RedirectToAction("AdminIndex", "Movie");

                }

            }

            catch (DataException)
            {

                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");

            }

            ViewBag.GenreID = new SelectList(db.Genres, "GenreID", "GenreName", genreMovie.GenreID);
            ViewBag.MovieID = new SelectList(db.Movies, "MovieID", "MovieName", genreMovie.MovieID);
            return View(genreMovie);

        }

        // GET: GenreMovie/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GenreMovie genreMovie = db.GenreMovies.Find(id);
            if (genreMovie == null)
            {
                return HttpNotFound();
            }
            ViewBag.GenreID = new SelectList(db.Genres, "GenreID", "GenreName", genreMovie.GenreID);
            ViewBag.MovieID = new SelectList(db.Movies, "MovieID", "MovieName", genreMovie.MovieID);
            return View(genreMovie);
        }

        // POST: GenreMovie/Edit/5
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

            var genreMovieToUpdate = db.GenreMovies.Find(id);

            if (TryUpdateModel(genreMovieToUpdate, "", new string[] { "MovieID", "GenreID", "GenreRatingForMovie" }))
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

            ViewBag.GenreID = new SelectList(db.Genres, "GenreID", "GenreName", genreMovieToUpdate.GenreID);
            ViewBag.MovieID = new SelectList(db.Movies, "MovieID", "MovieName", genreMovieToUpdate.MovieID);
            return View(genreMovieToUpdate);

        }

        // GET: GenreMovie/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GenreMovie genreMovie = db.GenreMovies.Find(id);
            if (genreMovie == null)
            {
                return HttpNotFound();
            }
            return View(genreMovie);
        }

        // POST: GenreMovie/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {

            try
            {

                GenreMovie genreMovie = db.GenreMovies.Find(id);
                db.GenreMovies.Remove(genreMovie);
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
