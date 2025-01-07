using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using MovieWeb.Models;

namespace MovieWeb.Controllers
{
    [AuthorizeAttribute_64130299Controller]
    public class MovieUrls_64130299Controller : Controller
    {
        private MovieDatabase_64130299Entities db = new MovieDatabase_64130299Entities();

        // GET: MovieUrls_64130299
        public ActionResult Index(string search)
        {
            // Lấy tất cả dữ liệu MovieUrls_64130299, bao gồm các thông tin phim
            var movieUrls_64130299 = db.MovieUrls_64130299
                .Include(m => m.Movie_64130299)
                .AsQueryable();

            // Nếu có từ khóa tìm kiếm, lọc kết quả theo Tiêu đề Phim và URL
            if (!string.IsNullOrEmpty(search))
            {
                var searchLower = search.ToLower(); // Chuyển đổi từ khóa tìm kiếm về chữ thường

                // Tải tất cả dữ liệu từ DB vào bộ nhớ và loại bỏ dấu
                movieUrls_64130299 = movieUrls_64130299.AsEnumerable() // Chuyển đổi sang IEnumerable để thực hiện LINQ to Objects
                    .Where(m =>
                        RemoveDiacritics(m.Movie_64130299.Title.ToLower()).Contains(RemoveDiacritics(searchLower)) ||  // Loại bỏ dấu trước khi so sánh
                        RemoveDiacritics(m.Url.ToLower()).Contains(RemoveDiacritics(searchLower))                      // Loại bỏ dấu trước khi so sánh
                    )
                    .AsQueryable();  // Chuyển lại thành IQueryable sau khi lọc
            }

            // Trả về kết quả tìm kiếm dưới dạng danh sách
            return View(movieUrls_64130299.ToList());
        }

        // Hàm loại bỏ dấu
        public string RemoveDiacritics(string text)
        {
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }

        // GET: MovieUrls_64130299/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MovieUrls_64130299 movieUrls_64130299 = db.MovieUrls_64130299.Find(id);
            if (movieUrls_64130299 == null)
            {
                return HttpNotFound();
            }
            return View(movieUrls_64130299);
        }

        // GET: MovieUrls_64130299/Create
        public ActionResult Create()
        {
            // Truyền danh sách các bộ phim vào dropdown list để chọn
            ViewBag.MovieId = new SelectList(db.Movie_64130299, "MovieId", "Title");

            // Lấy thông tin phim đầu tiên (hoặc bất kỳ phim nào) để tính MovieUrlId
            var firstMovie = db.Movie_64130299.FirstOrDefault();
            if (firstMovie != null)
            {
                // Lấy năm phát hành phim (ReleaseDate)
                var movieYear = firstMovie.ReleaseDate?.Year.ToString() ?? "Unknown";

                // Lấy tên phim không dấu
                var movieNameNoDiacritics = RemoveDiacritics(firstMovie.Title).ToLower();

                // Tạo MovieUrlId với số tập mặc định là "full"
                var episodeNumber = "full";  // Số tập mặc định là full
                ViewBag.MovieUrlId = $"{movieYear}-{movieNameNoDiacritics}-{episodeNumber}";
            }
            else
            {
                ViewBag.MovieUrlId = "Unknown";
            }

            return View();
        }

        // POST: MovieUrls_64130299/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MovieUrlId,MovieId,Url,EpisodeNumber")] MovieUrls_64130299 movieUrls_64130299)
        {
            if (ModelState.IsValid)
            {
                // Nếu số tập chưa được nhập, mặc định là "full"
                if (movieUrls_64130299.EpisodeNumber == 0)
                {
                    movieUrls_64130299.EpisodeNumber = 1;  // Hoặc "full"
                }

                // Lấy năm phim từ thông tin phim (ReleaseDate)
                var movieYear = db.Movie_64130299.Find(movieUrls_64130299.MovieId)?.ReleaseDate?.Year.ToString() ?? "Unknown";

                // Lấy tên phim không dấu
                var movieTitle = db.Movie_64130299.Find(movieUrls_64130299.MovieId)?.Title;
                var movieNameNoDiacritics = RemoveDiacritics(movieTitle ?? "").ToLower();

                // Loại bỏ dấu cách, dấu gạch ngang và dấu hai chấm
                var movieUrlId = movieNameNoDiacritics.Replace(" ", "").Replace("-", "").Replace(":", "");

                // Tạo MovieUrlId: Năm-phimKhôngDau-SốTập
                var episodeNumber = movieUrls_64130299.EpisodeNumber == 1 ? "full" : movieUrls_64130299.EpisodeNumber.ToString();
                movieUrls_64130299.MovieUrlId = $"{movieYear}-{movieUrlId}-{episodeNumber}";

                // Mã hóa MovieUrlId (nếu cần)
                var encodedMovieUrlId = HttpUtility.UrlEncode(movieUrls_64130299.MovieUrlId);

                // Truyền MovieUrlId vào ViewBag sau khi mã hóa
                ViewBag.MovieUrlId = encodedMovieUrlId;

                // Thêm MovieUrls_64130299 vào cơ sở dữ liệu
                db.MovieUrls_64130299.Add(movieUrls_64130299);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            // Nếu có lỗi, trả lại View và truyền lại MovieId
            ViewBag.MovieId = new SelectList(db.Movie_64130299, "MovieId", "Title", movieUrls_64130299.MovieId);
            return View(movieUrls_64130299);
        }

        // GET: MovieUrls_64130299/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MovieUrls_64130299 movieUrls_64130299 = db.MovieUrls_64130299.Find(id);
            if (movieUrls_64130299 == null)
            {
                return HttpNotFound();
            }

            // Truyền thông tin movieId và EpisodeNumber vào View
            ViewBag.MovieId = new SelectList(db.Movie_64130299, "MovieId", "Title", movieUrls_64130299.MovieId);
            ViewBag.EpisodeNumber = movieUrls_64130299.EpisodeNumber;  // Truyền số tập phim vào view

            return View(movieUrls_64130299);
        }

        // POST: MovieUrls_64130299/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MovieUrlId,MovieId,Url,EpisodeNumber")] MovieUrls_64130299 movieUrls_64130299)
        {
            if (ModelState.IsValid)
            {
                // Cập nhật trạng thái của đối tượng MovieUrls_64130299 và lưu vào cơ sở dữ liệu
                db.Entry(movieUrls_64130299).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            // Nếu có lỗi, truyền lại giá trị MovieId và EpisodeNumber vào View
            ViewBag.MovieId = new SelectList(db.Movie_64130299, "MovieId", "Title", movieUrls_64130299.MovieId);
            ViewBag.EpisodeNumber = movieUrls_64130299.EpisodeNumber;  // Truyền lại số tập phim vào view

            return View(movieUrls_64130299);
        }


        // GET: MovieUrls_64130299/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MovieUrls_64130299 movieUrls_64130299 = db.MovieUrls_64130299.Find(id);
            if (movieUrls_64130299 == null)
            {
                return HttpNotFound();
            }
            return View(movieUrls_64130299);
        }

        // POST: MovieUrls_64130299/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            MovieUrls_64130299 movieUrls_64130299 = db.MovieUrls_64130299.Find(id);
            db.MovieUrls_64130299.Remove(movieUrls_64130299);
            db.SaveChanges();
            return RedirectToAction("Index");
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
