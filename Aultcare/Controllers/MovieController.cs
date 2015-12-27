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
    public class MovieController : Controller
    {
        private MovieContext db = new MovieContext();

        // GET: Movie
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {

            //Method uses LINQ to Entities to specify the column to sort by.
            ViewBag.CurrentSort = sortOrder;

            ViewBag.MovieIDSortParm = sortOrder == "Movie_ID" ? "Movie_ID_Desc" : "Movie_ID";

            ViewBag.MovieNameSortParm = sortOrder == "Movie_Name" ? "Movie_Name_Desc" : "Movie_Name";

            ViewBag.DirectorSortParm = sortOrder == "Director" ? "Director_Desc" : "Director";

            ViewBag.ReleasedSortParm = sortOrder == "Released" ? "Released_Desc" : "Released";

            ViewBag.LengthSortParm = sortOrder == "Length" ? "Length_Desc" : "Length";


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

            var movies = from m in db.Movies select m;
            

            //For the search movies box.
            if (!String.IsNullOrEmpty(searchString))
            {

                movies = movies.Where(m => m.MovieID.ToString().Equals(searchString) || m.MovieName.Contains(searchString) ||  m.Director.Contains(searchString) || m.Released.ToString().Equals(searchString) || m.Released.Year.ToString().Equals(searchString) || m.Length.ToString().Equals(searchString));

            }


            //This Switch is for Sorting
            switch (sortOrder)
            {

                case "Movie_ID_Desc":
                    movies = movies.OrderByDescending(m => m.MovieID);
                    break;

                case "Movie_Name":
                    movies = movies.OrderBy(m => m.MovieName);
                    break;

                case "Movie_Name_Desc":
                    movies = movies.OrderByDescending(m => m.MovieName);
                    break;

                case "Director":
                    movies = movies.OrderBy(m => m.Director);
                    break;

                case "Director_Desc":
                    movies = movies.OrderByDescending(m => m.Director);
                    break;

                case "Released":
                    movies = movies.OrderBy(m => m.Released);
                    break;

                case "Released_Desc":
                    movies = movies.OrderByDescending(m => m.Released);
                    break;

                case "Length":
                    movies = movies.OrderBy(m => m.Length);
                    break;

                case "Length_Desc":
                    movies = movies.OrderByDescending(m => m.Length);
                    break;

                default:
                    movies = movies.OrderBy(m => m.MovieID);
                    break;

            }

            //Determines the amount of records per paging size. 
            int pageSize = 3;

            int pageNumber = (page ?? 1);

            return View(movies.ToPagedList(pageNumber, pageSize));

        }

        // GET: Movie/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // GET: AdminMovie
        public ActionResult AdminIndex(string sortOrder, string currentFilter, string searchString, int? page)
        {

            //Method uses LINQ to Entities to specify the column to sort by.
            ViewBag.CurrentSort = sortOrder;

            ViewBag.MovieIDSortParm = sortOrder == "Movie_ID" ? "Movie_ID_Desc" : "Movie_ID";

            ViewBag.MovieNameSortParm = sortOrder == "Movie_Name" ? "Movie_Name_Desc" : "Movie_Name";

            ViewBag.DirectorSortParm = sortOrder == "Director" ? "Director_Desc" : "Director";

            ViewBag.ReleasedSortParm = sortOrder == "Released" ? "Released_Desc" : "Released";

            ViewBag.LengthSortParm = sortOrder == "Length" ? "Length_Desc" : "Length";


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

            var movies = from m in db.Movies select m;


            //For the search movies box.
            if (!String.IsNullOrEmpty(searchString))
            {

                movies = movies.Where(m => m.MovieID.ToString().Equals(searchString) || m.MovieName.Contains(searchString) || m.Director.Contains(searchString) || m.Released.ToString().Equals(searchString) || m.Released.Year.ToString().Equals(searchString) || m.Length.ToString().Equals(searchString));

            }


            //This Switch is for Sorting
            switch (sortOrder)
            {

                case "Movie_ID_Desc":
                    movies = movies.OrderByDescending(m => m.MovieID);
                    break;

                case "Movie_Name":
                    movies = movies.OrderBy(m => m.MovieName);
                    break;

                case "Movie_Name_Desc":
                    movies = movies.OrderByDescending(m => m.MovieName);
                    break;

                case "Director":
                    movies = movies.OrderBy(m => m.Director);
                    break;

                case "Director_Desc":
                    movies = movies.OrderByDescending(m => m.Director);
                    break;

                case "Released":
                    movies = movies.OrderBy(m => m.Released);
                    break;

                case "Released_Desc":
                    movies = movies.OrderByDescending(m => m.Released);
                    break;

                case "Length":
                    movies = movies.OrderBy(m => m.Length);
                    break;

                case "Length_Desc":
                    movies = movies.OrderByDescending(m => m.Length);
                    break;

                default:
                    movies = movies.OrderBy(m => m.MovieID);
                    break;

            }

            //Determines the amount of records per paging size. 
            int pageSize = 3;

            int pageNumber = (page ?? 1);

            return View(movies.ToPagedList(pageNumber, pageSize));

        }

        // GET: Movie/Details/5
        public ActionResult AdminDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // GET: Movie/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Movie/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MovieName,Director,Released,Length")] Movie movie)
        {

            try
            {

                if (ModelState.IsValid)
                {

                    db.Movies.Add(movie);
                    db.SaveChanges();
                    return RedirectToAction("AdminIndex");

                }

            }

            catch (DataException)
            {
    
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");

            }

            return View(movie);

        }

        // GET: Movie/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // POST: Movie/Edit/5
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

            var movieToUpdate = db.Movies.Find(id);

            if (TryUpdateModel(movieToUpdate, "", new string[] { "MovieName", "Director", "Released", "Length" }))
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

            return View(movieToUpdate);

        }

        // GET: Movie/Delete/5
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

            Movie movie = db.Movies.Find(id);

            if (movie == null)
            {

                return HttpNotFound();

            }

            return View(movie);

        }

        // POST: Movie/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {

            try
            {

                Movie movie = db.Movies.Find(id);

                db.Movies.Remove(movie);

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
