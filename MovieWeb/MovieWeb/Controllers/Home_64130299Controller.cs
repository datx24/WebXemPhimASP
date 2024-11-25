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
        public ActionResult Home_64130299()
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
            // Get UserId from Session
            var userId = Session["UserId"] as string;

            if (string.IsNullOrEmpty(userId))
            {
                // If UserId is not in session, redirect to login page
                return RedirectToAction("Login_64130299", "User_64130299");
            }

            // Check the user's subscription status (assuming we have a MemberSubscription table)
            var subscription = db.MemberSubscription_64130299
                                 .FirstOrDefault(s => s.UserId == userId && s.AccessLevel == "Premium" && s.Status == "Active");

            if (subscription == null)
            {
                // If the user does not have an active Premium subscription, show a message and redirect
                TempData["ErrorMessage"] = "Bạn cần đăng ký gói Premium để xem phim này.";
                return RedirectToAction("Create_64130299", "Subscription_64130299");
            }

            // Retrieve the MovieUrl entry based on the MovieId (foreign key)
            var movieUrl = db.MovieUrls_64130299.FirstOrDefault(url => url.MovieId == id);
            if (movieUrl == null) return HttpNotFound();

            return View(movieUrl); // Pass the MovieUrl object to the view
        }
    }
}
