using System.Web.Mvc;
using Aultcare.DAL;
using Aultcare.Models;
using System.Linq;

namespace Aultcare.Controllers
{
    public class HomeController : Controller
    {

        private MovieContext db = new MovieContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GenreStatistics()
        {
            IQueryable<GenreNameGroup> data = from GenresMovies in db.GenreMovies
                                              group GenresMovies.MovieID by GenresMovies.Genre.GenreName into genreGroup
                                              select new GenreNameGroup()
                                              {
                                                  GenreName = genreGroup.Key,
                                                  GenreCount = genreGroup.Count()
                                              };

            return View(data.ToList());

        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}