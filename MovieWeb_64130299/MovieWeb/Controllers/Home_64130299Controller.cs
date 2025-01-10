using MovieWeb.Models;
using System;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;

namespace MovieWeb.Controllers
{
    public class Home_64130299Controller : Controller
    {
        private MovieDatabase_64130299Entities db = new MovieDatabase_64130299Entities();

        [HttpPost]
        public ActionResult RemoveFromFavorites(string favoriteId) // Đảm bảo truyền vào FavoriteId kiểu string
        {
            try
            {
                var userId = Session["UserId"] as string;

                if (string.IsNullOrEmpty(userId))
                {
                    // Nếu người dùng chưa đăng nhập, chuyển hướng đến trang đăng nhập
                    return RedirectToAction("Login_64130299", "User_64130299");
                }

                // Lấy bản ghi Favorite từ cơ sở dữ liệu cho người dùng và FavoriteId
                var favorite = db.Favorite_64130299
                                 .FirstOrDefault(f => f.FavoriteId == favoriteId && f.UserId == userId);

                if (favorite == null)
                {
                    // Nếu không tìm thấy bản ghi, thông báo lỗi
                    TempData["ErrorMessage"] = "Phim không tồn tại trong danh sách yêu thích.";
                    return RedirectToAction("Favorites_64130299");
                }

                // Xóa phim khỏi danh sách yêu thích
                db.Favorite_64130299.Remove(favorite);
                db.SaveChanges();

                // Thông báo thành công
                TempData["SuccessMessage"] = "Xóa phim khỏi danh sách yêu thích thành công!";
                return RedirectToAction("Favorites_64130299");
            }
            catch (Exception ex)
            {
                // Xử lý lỗi và thông báo cho người dùng
                TempData["ErrorMessage"] = "Có lỗi xảy ra khi xóa phim: " + ex.Message;
                return RedirectToAction("Favorites_64130299");
            }
        }


        public ActionResult AddToFavorites(string movieId)
        {
            // Lấy userId từ session
            var userId = Session["UserId"] as string;

            // Kiểm tra nếu không có userId trong session, chuyển hướng đến trang đăng nhập
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login_64130299", "User_64130299");
            }

            // Kiểm tra nếu bộ phim đã tồn tại trong danh sách yêu thích của người dùng
            var existingFavorite = db.Favorite_64130299
                                     .SingleOrDefault(f => f.UserId == userId && f.MovieId == movieId);

            if (existingFavorite == null)
            {
                // Tạo đối tượng mới cho yêu thích
                var favorite = new Favorite_64130299
                {
                    FavoriteId = Guid.NewGuid().ToString(),
                    UserId = userId,
                    MovieId = movieId,
                    CreatedAt = DateTime.Now // Lưu thời gian tạo
                };

                // Thêm vào bảng Favorite_64130299
                db.Favorite_64130299.Add(favorite);
                db.SaveChanges();
            }
            else
            {
                // Nếu đã tồn tại trong yêu thích, có thể thông báo hoặc bỏ qua
                ViewBag.Message = "Phim đã có trong danh sách yêu thích.";
            }

            // Trả về trang trước đó hoặc danh sách yêu thích
            return RedirectToAction("Favorites_64130299");
        }
        public ActionResult Favorites_64130299(int page = 1)
        {
            var userId = Session["UserId"] as string;

            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login_64130299", "User_64130299");
            }

            // Giới hạn số lượng phim hiển thị mỗi trang (20 phim)
            int pageSize = 20;
            int skipCount = (page - 1) * pageSize;

            // Lấy danh sách yêu thích cùng thông tin phim, phân trang
            var favoriteMovies = db.Favorite_64130299
                                   .Where(f => f.UserId == userId)
                                   .Include(f => f.Movie_64130299) // Eager loading thông tin phim
                                   .OrderBy(f => f.Movie_64130299.Title) // Có thể thay đổi cách sắp xếp
                                   .Skip(skipCount)
                                   .Take(pageSize)
                                   .ToList();

            return View(favoriteMovies);
        }

        // GET: Home_64130299 - hiển thị danh sách các bộ phim
        public ActionResult Home_64130299()
        {
            // Lấy danh sách 10 phim mới nhất dựa vào UpdatedAt
            var latestMovies = db.Movie_64130299
                                .OrderByDescending(m => m.UpdatedAt)
                                .Take(10)
                                .ToList();

            return View(latestMovies); // Truyền danh sách phim vào view
        }
        public ActionResult Details_64130299(string id)
        {
            // Lấy thông tin UserId từ session
            string userId = Session["UserId"]?.ToString();

            // Tìm phim theo ID
            var movie = db.Movie_64130299.Find(id);
            if (movie == null)
            {
                return HttpNotFound(); // Trả về lỗi nếu không tìm thấy phim
            }

            // Kiểm tra quyền truy cập phim
            bool canWatchMovie = false;
            string message = string.Empty;

            // Nếu phim là phim miễn phí, khách vãng lai có thể xem được
            if (movie.AccessLevel == "Free")
            {
                canWatchMovie = true;
            }
            else if (!string.IsNullOrEmpty(userId))
            {
                // Nếu người dùng đã đăng nhập, kiểm tra thẻ thành viên
                var userSubscription = db.MemberSubscription_64130299
                    .FirstOrDefault(ms => ms.UserId == userId && ms.Status == "Kích hoạt" && ms.ExpiryDate > DateTime.Now);

                if (userSubscription != null && userSubscription.AccessLevel == "Premium")
                {
                    // Nếu người dùng có thẻ Premium, cho phép xem phim
                    canWatchMovie = true;
                }
                else
                {
                    // Nếu không có thẻ Premium, không cho phép xem
                    message = "Bạn cần đăng kí thẻ Premium để xem phim này.";
                }
            }
            else
            {
                // Nếu không đăng nhập và phim yêu cầu thẻ Premium
                message = "Bạn cần đăng kí thẻ Premium để xem phim này.";
            }

            ViewBag.CanWatchMovie = canWatchMovie;
            ViewBag.Message = message;

            // Lấy URL liên quan từ bảng MovieUrls_64130299
            var movieUrl = db.MovieUrls_64130299.FirstOrDefault(url => url.MovieId == id);
            ViewBag.MovieUrl = movieUrl?.Url; // Gửi URL này đến View thông qua ViewBag

            // Lấy danh sách đánh giá của bộ phim
            var ratings = db.Rating_64130299.Where(r => r.MovieId == id).ToList();
            var averageRating = ratings.Any() ? ratings.Average(r => r.Rating) : 0;
            ViewBag.AverageRating = averageRating; // Gửi điểm trung bình đến View

            // Truy xuất đánh giá của người dùng (nếu có)
            var userRating = db.Rating_64130299.FirstOrDefault(r => r.MovieId == id && r.UserId == userId);
            ViewBag.UserRating = userRating?.Rating ?? 0; // Nếu người dùng chưa đánh giá, giá trị mặc định là 0

            return View(movie); // Trả về View với thông tin chi tiết phim
        }

        public ActionResult RateMovie_64130299(string id, int rating)
        {
            // Lấy ID người dùng từ session
            string userId = Session["UserId"]?.ToString();

            // Kiểm tra nếu người dùng chưa đăng nhập
            if (string.IsNullOrEmpty(userId))
            {
                TempData["ErrorMessage"] = "Bạn cần đăng nhập để đánh giá.";
                return RedirectToAction("Login_64130299", "User_64130299");
            }

            // Tìm đánh giá của người dùng trong cơ sở dữ liệu
            var existingRating = db.Rating_64130299.FirstOrDefault(r => r.MovieId == id && r.UserId == userId);
            if (existingRating != null)
            {
                // Cập nhật đánh giá nếu người dùng đã đánh giá trước đó
                existingRating.Rating = rating;
            }
            else
            {
                // Tạo mới đánh giá nếu người dùng chưa đánh giá
                db.Rating_64130299.Add(new Rating_64130299
                {
                    RatingId = Guid.NewGuid().ToString(), // Tạo ID duy nhất
                    MovieId = id,
                    UserId = userId,
                    Rating = rating
                });
            }

            // Lưu thay đổi vào cơ sở dữ liệu
            db.SaveChanges();

            // Chuyển hướng đến trang chi tiết phim
            return RedirectToAction("Details_64130299", new { id = id });
        }


        // Hiển thị trailer của một bộ phim
        public ActionResult Trailer_64130299(string id)
        {
            // Tìm phim theo ID
            var movie = db.Movie_64130299.Find(id);
            if (movie == null) return HttpNotFound(); // Nếu không tìm thấy, trả về lỗi

            return View(movie); // Trả về View với thông tin trailer
        }

        // Hiển thị bộ phim hoàn chỉnh dựa vào URL từ bảng MovieUrls_64130299
        public ActionResult WatchMovie_64130299(string id, int? episodeNumber)
        {
            // Lấy tất cả các tập phim của MovieId
            var movieUrls = db.MovieUrls_64130299
                .Where(url => url.MovieId == id)
                .OrderBy(url => url.EpisodeNumber)
                .ToList();

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

            // Xác định tập phim hiện tại (nếu số tập được cung cấp)
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
            ViewBag.CurrentMovieTitle = db.Movie_64130299
                .FirstOrDefault(m => m.MovieId == id)?.Title ?? "Phim không xác định";

            // Trả về View với thông tin tập phim hiện tại
            return View(currentEpisode);
        }

        // Action lọc danh sách phim theo các tiêu chí
        public ActionResult FilterMovies_64130299(string GenreId, string GenreName, string Country, int? ReleaseYear, string Title = "", int page = 1)
        {
            int pageSize = 20; // Số lượng phim hiển thị trên một trang
            var query = db.Movie_64130299.AsQueryable();

            // Lọc theo tên phim gần đúng
            if (!string.IsNullOrEmpty(Title))
            {
                query = query.Where(m => m.Title.ToLower().Contains(Title.ToLower())); // Tìm kiếm gần đúng theo tên phim, không phân biệt chữ hoa chữ thường
            }

            // Lọc theo định dạng phim (phim lẻ hoặc phim bộ)
            if (!string.IsNullOrEmpty(GenreId))
            {
                bool isSingleMovie = GenreId.Equals("True", StringComparison.OrdinalIgnoreCase);
                query = query.Where(m => m.GenreId == isSingleMovie);
            }

            // Lọc theo thể loại (GenreName)
            if (!string.IsNullOrEmpty(GenreName))
            {
                query = query.Where(m => m.GenreName.ToLower().Contains(GenreName.ToLower())); // Lọc phim theo tên thể loại
            }

            // Lọc theo quốc gia
            if (!string.IsNullOrEmpty(Country))
            {
                query = query.Where(m => m.Country.ToLower().Contains(Country.ToLower()));
            }

            // Lọc theo năm phát hành
            if (ReleaseYear.HasValue)
            {
                query = query.Where(m => m.ReleaseDate.HasValue && m.ReleaseDate.Value.Year == ReleaseYear);
            }

            // Tính tổng số trang
            int totalMovies = query.Count();
            int totalPages = (int)Math.Ceiling(totalMovies / (double)pageSize);

            // Lấy danh sách phim đã lọc và phân trang
            var movies = query
                .OrderBy(m => m.Title) // Sắp xếp theo tiêu đề
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            // Truyền thông tin phân trang và tiêu chí tìm kiếm vào ViewBag
            ViewBag.Page = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.Title = Title; // Truyền Title vào ViewBag để giữ giá trị tìm kiếm
            ViewBag.Country = Country;
            ViewBag.ReleaseYear = ReleaseYear;
            ViewBag.GenreName = GenreName; // Truyền GenreName vào ViewBag để giữ giá trị thể loại
            ViewBag.GenreId = GenreId;

            return View("FilterMovies_64130299", movies); // Trả về view với danh sách phim đã lọc
        }
    }
}
