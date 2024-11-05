using MovieWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MovieWeb.Controllers
{
    public class Home_64130299Controller : Controller
    {
        private MovieDatabase_64130299Entities db = new MovieDatabase_64130299Entities();
        // GET: Home_64130299
        public ActionResult home64130299()
        {
            var movies = db.Movie_64130299.ToList();
            return View(movies); // Trả về danh sách phim cho view
        }
        // Phương thức hiển thị thông tin chi tiết của bộ phim
        public ActionResult Details_64130299(int id)
        {
            var movie = db.Movie_64130299.Find(id); // Replace with your logic to retrieve the movie
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie); // Return the movie details view
        }
    }
}