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
    public class ActorController : Controller
    {
        private MovieContext db = new MovieContext();

        // GET: Actor
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {

            //Method uses LINQ to Entities to specify the column to sort by.
            ViewBag.CurrentSort = sortOrder;

            ViewBag.ActorIDSortParm = sortOrder == "Actor_ID" ? "Actor_ID_Desc" : "Actor_ID";

            ViewBag.ActorNameSortParm = sortOrder == "Actor_Name" ? "Actor_Name_Desc" : "Actor_Name";

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

            var actors = from a in db.Actors select a;
            

            //For the search actors box.
            if (!String.IsNullOrEmpty(searchString))
            {

                actors = actors.Where(a => a.ActorID.ToString().Equals(searchString) || a.ActorName.Contains(searchString));

            }


            //This Switch is for Sorting
            switch (sortOrder)
            {

                case "Actor_ID_Desc":
                    actors = actors.OrderByDescending(a => a.ActorID);
                    break;

                case "Actor_Name":
                    actors = actors.OrderBy(a => a.ActorName);
                    break;

                case "Actor_Name_Desc":
                    actors = actors.OrderByDescending(a => a.ActorName);
                    break;

                default:
                    actors = actors.OrderBy(a => a.ActorID);
                    break;

            }

            //Determines the amount of records per paging size. 
            int pageSize = 3;

            int pageNumber = (page ?? 1);

            return View(actors.ToPagedList(pageNumber, pageSize));

        }

        // GET: Actor/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Actor actor = db.Actors.Find(id);
            if (actor == null)
            {
                return HttpNotFound();
            }
            return View(actor);
        }

        public ActionResult AdminIndex(string sortOrder, string currentFilter, string searchString, int? page)
        {

            //Method uses LINQ to Entities to specify the column to sort by.
            ViewBag.CurrentSort = sortOrder;

            ViewBag.ActorIDSortParm = sortOrder == "Actor_ID" ? "Actor_ID_Desc" : "Actor_ID";

            ViewBag.ActorNameSortParm = sortOrder == "Actor_Name" ? "Actor_Name_Desc" : "Actor_Name";

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

            var actors = from a in db.Actors select a;


            //For the search actors box.
            if (!String.IsNullOrEmpty(searchString))
            {

                actors = actors.Where(a => a.ActorID.ToString().Equals(searchString) || a.ActorName.Contains(searchString));

            }


            //This Switch is for Sorting
            switch (sortOrder)
            {

                case "Actor_ID_Desc":
                    actors = actors.OrderByDescending(a => a.ActorID);
                    break;

                case "Actor_Name":
                    actors = actors.OrderBy(a => a.ActorName);
                    break;

                case "Actor_Name_Desc":
                    actors = actors.OrderByDescending(a => a.ActorName);
                    break;

                default:
                    actors = actors.OrderBy(a => a.ActorID);
                    break;

            }

            //Determines the amount of records per paging size. 
            int pageSize = 3;

            int pageNumber = (page ?? 1);

            return View(actors.ToPagedList(pageNumber, pageSize));

        }

        public ActionResult AdminDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Actor actor = db.Actors.Find(id);
            if (actor == null)
            {
                return HttpNotFound();
            }
            return View(actor);
        }

        // GET: Actor/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Actor/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ActorName")] Actor actor)
        {

            try
            {

                if (ModelState.IsValid)
                {

                    db.Actors.Add(actor);
                    db.SaveChanges();
                    return RedirectToAction("AdminIndex");

                }

            }

            catch (DataException)
            {

                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");

            }

            return View(actor);

        }

        // GET: Actor/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Actor actor = db.Actors.Find(id);
            if (actor == null)
            {
                return HttpNotFound();
            }
            return View(actor);
        }

        // POST: Actor/Edit/5
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

            var actorToUpdate = db.Actors.Find(id);

            if (TryUpdateModel(actorToUpdate, "", new string[] { "ActorName" }))
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

            return View(actorToUpdate);

        }

        // GET: Actor/Delete/5
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

            Actor actor = db.Actors.Find(id);

            if (actor == null)
            {

                return HttpNotFound();

            }

            return View(actor);

        }

        // POST: Movie/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {

            try
            {

                Actor actor = db.Actors.Find(id);

                db.Actors.Remove(actor);

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
