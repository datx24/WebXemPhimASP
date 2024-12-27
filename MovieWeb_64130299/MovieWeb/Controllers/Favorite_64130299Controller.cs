using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MovieWeb.Models;

namespace MovieWeb.Controllers
{
    [AuthorizeAttribute_64130299Controller]
    public class Favorite_64130299Controller : Controller
    {
        private MovieDatabase_64130299Entities db = new MovieDatabase_64130299Entities();
        // Phương thức trả về tên phim được yêu thích nhất
        public string GetMostFavoritedMovieName()
        {
            // Nhóm theo MovieId, đếm số lượng yêu thích, tìm phim có số yêu thích cao nhất
            var mostFavoritedMovie = db.Favorite_64130299
                .GroupBy(f => f.MovieId)
                .OrderByDescending(g => g.Count()) // Sắp xếp theo số lượng yêu thích giảm dần
                .Select(g => new
                {
                    MovieId = g.Key,
                    FavoriteCount = g.Count()
                })
                .FirstOrDefault(); // Lấy phim đầu tiên (nhiều yêu thích nhất)

            // Nếu không có phim yêu thích nào, trả về thông báo
            if (mostFavoritedMovie == null)
            {
                return "No movie found in the favorite list.";
            }

            // Lấy thông tin tên phim từ bảng Movie
            var movieName = db.Movie_64130299
                .Where(m => m.MovieId == mostFavoritedMovie.MovieId)
                .Select(m => m.Title)
                .FirstOrDefault();

            return movieName ?? "Movie title not found.";
        }

        // GET: Favorite_64130299
        public ActionResult Index()
        {
            // Lấy phim được yêu thích nhất và số lượt yêu thích
            var mostFavoritedMovie = db.Favorite_64130299
                .GroupBy(f => f.Movie_64130299)
                .OrderByDescending(g => g.Count())
                .Select(g => new
                {
                    Title = g.Key.Title,
                    PosterUrl = g.Key.PosterUrl,
                    LikeCount = g.Count()
                })
                .FirstOrDefault();

            if (mostFavoritedMovie != null)
            {
                dynamic movie = new ExpandoObject();
                movie.Title = mostFavoritedMovie.Title;
                movie.PosterUrl = mostFavoritedMovie.PosterUrl;
                movie.LikeCount = mostFavoritedMovie.LikeCount;
                ViewBag.MostFavoritedMovie = movie;
            }

            // Lấy danh sách yêu thích
            var favorite_64130299 = db.Favorite_64130299
                .Include(f => f.Movie_64130299)
                .Include(f => f.User_64130299)
                .ToList();

            return View(favorite_64130299);
        }

        // GET: Favorite_64130299/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Favorite_64130299 favorite_64130299 = db.Favorite_64130299.Find(id);
            if (favorite_64130299 == null)
            {
                return HttpNotFound();
            }
            return View(favorite_64130299);
        }

        // GET: Favorite_64130299/Create
        public ActionResult Create()
        {
            var favorite = new Favorite_64130299
            {
                CreatedAt = DateTime.Now
            };
            ViewBag.MovieId = new SelectList(db.Movie_64130299, "MovieId", "Title");
            ViewBag.UserId = new SelectList(db.User_64130299, "UserId", "Email");
            return View(favorite);
        }

        // POST: Favorite_64130299/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FavoriteId,UserId,MovieId,CreatedAt")] Favorite_64130299 favorite_64130299)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Tạo FavoriteId tự động bằng GUID
                    favorite_64130299.FavoriteId = Guid.NewGuid().ToString();
                    favorite_64130299.CreatedAt = DateTime.Now;
                    db.Favorite_64130299.Add(favorite_64130299);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                // Nếu ModelState không hợp lệ, tiếp tục hiển thị form với các lỗi
                ViewBag.MovieId = new SelectList(db.Movie_64130299, "MovieId", "Title", favorite_64130299.MovieId);
                ViewBag.UserId = new SelectList(db.User_64130299, "UserId", "Email", favorite_64130299.UserId);
                return View(favorite_64130299);
            }
            catch (DbEntityValidationException ex)
            {
                // Ghi lại chi tiết lỗi xác thực
                foreach (var validationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        // In ra lỗi chi tiết hoặc lưu vào log
                        ModelState.AddModelError("", $"Property: {validationError.PropertyName}, Error: {validationError.ErrorMessage}");
                    }
                }

                // Nếu có lỗi xác thực, trả lại view với thông báo lỗi
                ViewBag.MovieId = new SelectList(db.Movie_64130299, "MovieId", "Title", favorite_64130299.MovieId);
                ViewBag.UserId = new SelectList(db.User_64130299, "UserId", "Email", favorite_64130299.UserId);
                return View(favorite_64130299);
            }
            catch (Exception ex)
            {
                // Bắt các lỗi khác (nếu có)
                ModelState.AddModelError("", $"An error occurred: {ex.Message}");

                // Nếu có lỗi hệ thống, trả lại view với thông báo lỗi
                ViewBag.MovieId = new SelectList(db.Movie_64130299, "MovieId", "Title", favorite_64130299.MovieId);
                ViewBag.UserId = new SelectList(db.User_64130299, "UserId", "Email", favorite_64130299.UserId);
                return View(favorite_64130299);
            }
        }


        // GET: Favorite_64130299/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Favorite_64130299 favorite_64130299 = db.Favorite_64130299.Find(id);
            if (favorite_64130299 == null)
            {
                return HttpNotFound();
            }
            ViewBag.MovieId = new SelectList(db.Movie_64130299, "MovieId", "Title", favorite_64130299.MovieId);
            ViewBag.UserId = new SelectList(db.User_64130299, "UserId", "Email", favorite_64130299.UserId);
            return View(favorite_64130299);
        }

        // POST: Favorite_64130299/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FavoriteId,UserId,MovieId,CreatedAt")] Favorite_64130299 favorite_64130299)
        {
            if (ModelState.IsValid)
            {
                db.Entry(favorite_64130299).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MovieId = new SelectList(db.Movie_64130299, "MovieId", "Title", favorite_64130299.MovieId);
            ViewBag.UserId = new SelectList(db.User_64130299, "UserId", "Email", favorite_64130299.UserId);
            return View(favorite_64130299);
        }

        // GET: Favorite_64130299/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Favorite_64130299 favorite_64130299 = db.Favorite_64130299.Find(id);
            if (favorite_64130299 == null)
            {
                return HttpNotFound();
            }
            return View(favorite_64130299);
        }

        // POST: Favorite_64130299/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Favorite_64130299 favorite_64130299 = db.Favorite_64130299.Find(id);
            db.Favorite_64130299.Remove(favorite_64130299);
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
