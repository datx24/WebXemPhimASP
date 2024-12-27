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
        public ActionResult WatchMovie_64130299(string id, int? episodeNumber)
        {
            // Lấy tất cả các tập phim của MovieId
            var movieUrls = db.MovieUrls_64130299.Where(url => url.MovieId == id).OrderBy(url => url.EpisodeNumber).ToList();

            // Nếu không có tập phim nào, trả về lỗi
            if (!movieUrls.Any())
            {
                TempData["ErrorMessage"] = "Phim không tồn tại.";
                return HttpNotFound();
            }

            // Nếu chỉ có một tập phim, mặc định chọn tập đó
            if (movieUrls.Count == 1)
            {
                return View(movieUrls.First());
            }

            // Xác định tập phim hiện tại nếu có (dựa trên số tập nếu có)
            var currentEpisode = episodeNumber.HasValue
                ? movieUrls.FirstOrDefault(url => url.EpisodeNumber == episodeNumber)
                : movieUrls.FirstOrDefault(); // Nếu không có số tập, chọn tập đầu tiên

            // Nếu không tìm thấy tập phim hiện tại, trả về lỗi
            if (currentEpisode == null)
            {
                TempData["ErrorMessage"] = "Tập phim không tồn tại.";
                return HttpNotFound();
            }

            // Truyền dữ liệu vào View
            ViewBag.MovieUrls = movieUrls; // Danh sách các tập phim
            ViewBag.CurrentMovieTitle = db.Movie_64130299.FirstOrDefault(m => m.MovieId == id)?.Title ?? "Phim không xác định";

            // Trả về View với thông tin tập phim hiện tại
            return View(currentEpisode);
        }
    }
}
