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
using PagedList;

namespace Aultcare.Controllers
{
    public class GenreController : Controller
    {
        private MovieContext db = new MovieContext();

        // GET: Genre
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {

            //Method uses LINQ to Entities to specify the column to sort by.
            ViewBag.CurrentSort = sortOrder;

            ViewBag.GenreIDSortParm = sortOrder == "Genre_ID" ? "Genre_ID_Desc" : "Genre_ID";

            ViewBag.GenreNameSortParm = sortOrder == "Genre_Name" ? "Genre_Name_Desc" : "Genre_Name";

            //Determines if there is a filter n search box for paging
            if (searchString != null)
            {

                page = 1;

            }

            else
            {

                searchString = currentFilter;

            }

            ViewBag.CurrentFilter = searchString;

            var genres = from g in db.Genres select g;


            //For the search genres box.
            if (!String.IsNullOrEmpty(searchString))
            {

                genres = genres.Where(g => g.GenreID.ToString().Equals(searchString) || g.GenreName.Contains(searchString));

            }


            //This Switch is for Sorting
            switch (sortOrder)
            {

                case "Genre_ID_Desc":
                    genres = genres.OrderByDescending(g => g.GenreID);
                    break;

                case "Genre_Name":
                    genres = genres.OrderBy(g => g.GenreName);
                    break;

                case "Genre_Name_Desc":
                    genres = genres.OrderByDescending(g => g.GenreName);
                    break;

                default:
                    genres = genres.OrderBy(g => g.GenreID);
                    break;

            }

            //Determines the amount of records per paging size. 
            int pageSize = 3;

            int pageNumber = (page ?? 1);

            return View(genres.ToPagedList(pageNumber, pageSize));

        }

        // GET: Genre/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Genre genre = db.Genres.Find(id);
            if (genre == null)
            {
                return HttpNotFound();
            }
            return View(genre);
        }

        public ActionResult AdminIndex(string sortOrder, string currentFilter, string searchString, int? page)
        {

            //Method uses LINQ to Entities to specify the column to sort by.
            ViewBag.CurrentSort = sortOrder;

            ViewBag.GenreIDSortParm = sortOrder == "Genre_ID" ? "Genre_ID_Desc" : "Genre_ID";

            ViewBag.GenreNameSortParm = sortOrder == "Genre_Name" ? "Genre_Name_Desc" : "Genre_Name";

            //Determines if there is a filter n search box for paging
            if (searchString != null)
            {

                page = 1;

            }

            else
            {

                searchString = currentFilter;

            }

            ViewBag.CurrentFilter = searchString;

            var genres = from g in db.Genres select g;


            //For the search genres box.
            if (!String.IsNullOrEmpty(searchString))
            {

                genres = genres.Where(g => g.GenreID.ToString().Equals(searchString) || g.GenreName.Contains(searchString));

            }


            //This Switch is for Sorting
            switch (sortOrder)
            {

                case "Genre_ID_Desc":
                    genres = genres.OrderByDescending(g => g.GenreID);
                    break;

                case "Genre_Name":
                    genres = genres.OrderBy(g => g.GenreName);
                    break;

                case "Genre_Name_Desc":
                    genres = genres.OrderByDescending(g => g.GenreName);
                    break;

                default:
                    genres = genres.OrderBy(g => g.GenreID);
                    break;

            }

            //Determines the amount of records per paging size. 
            int pageSize = 3;

            int pageNumber = (page ?? 1);

            return View(genres.ToPagedList(pageNumber, pageSize));

        }

        public ActionResult AdminDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Genre genre = db.Genres.Find(id);
            if (genre == null)
            {
                return HttpNotFound();
            }
            return View(genre);
        }

        // GET: Genre/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Genre/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GenreName")] Genre genre)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    db.Genres.Add(genre);
                    db.SaveChanges();
                    return RedirectToAction("AdminIndex");
                }

            }

            catch (DataException)
            {

                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");

            }

            return View(genre);
        }

        // GET: Genre/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Genre genre = db.Genres.Find(id);
            if (genre == null)
            {
                return HttpNotFound();
            }
            return View(genre);
        }

        // POST: Genre/Edit/5
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

            var genreToUpdate = db.Genres.Find(id);

            if (TryUpdateModel(genreToUpdate, "", new string[] { "genreName"}))
            {

                try
                {

                    if (ModelState.IsValid)
                    {

                        db.SaveChanges();

                        return RedirectToAction("AdminIndex");

                    }

                }

                catch (DataException)
                {

                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");

                }

            }

            return View(genreToUpdate);

        }

        // GET: Genre/Delete/5
        public ActionResult Delete(int? id, bool? saveChangesError = false)
        {

            if (id == null)
            {

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }

            if (saveChangesError.GetValueOrDefault())
            {

                ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";

            }

            Genre genre = db.Genres.Find(id);

            if (genre == null)
            {

                return HttpNotFound();

            }

            return View(genre);

        }

        // POST: Genre/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {

            try
            {

                Genre genre = db.Genres.Find(id);

                db.Genres.Remove(genre);

                db.SaveChanges();

            }

            catch (DataException)
            {

                return RedirectToAction("Delete", new { id = id, saveChangesError = true });

            }

            return RedirectToAction("AdminIndex");

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
