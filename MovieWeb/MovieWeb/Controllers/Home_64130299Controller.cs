using MovieWeb.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace MovieWeb.Controllers
{
    public class Home_64130299Controller : Controller
    {
        private MovieDatabase_64130299Entities db = new MovieDatabase_64130299Entities();

        // GET: Home_64130299 - displays a list of movies
        public ActionResult Home64130299()
        {
            var movies = db.Movie_64130299.ToList();
            return View(movies); // Returns the list of movies to the view
        }

        // Display detailed information of a movie
        public ActionResult Details_64130299(string id)
        {
            var movie = db.Movie_64130299.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }

            // Retrieve associated URL from MovieUrls_64130299 table
            var movieUrl = db.MovieUrls_64130299.FirstOrDefault(url => url.MovieId == id);
            ViewBag.MovieUrl = movieUrl?.Url;

            return View(movie); // Returns the movie details view
        }

        // Display the trailer of a movie
        public ActionResult Trailer_64130299(string id)
        {
            var movie = db.Movie_64130299.Find(id);
            if (movie == null) return HttpNotFound();

            return View(movie); // Returns the trailer view
        }

        // Display the full movie using URL from MovieUrls_64130299 table
        public ActionResult WatchMovie_64130299(string id)
        {
            // Retrieve the MovieUrl entry based on the MovieId (foreign key)
            var movieUrl = db.MovieUrls_64130299.FirstOrDefault(url => url.MovieId == id);
            if (movieUrl == null) return HttpNotFound();

            return View(movieUrl); // Pass the MovieUrl object to the view
        }
    }
}
