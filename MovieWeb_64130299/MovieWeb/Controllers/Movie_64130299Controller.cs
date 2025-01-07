using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using MovieWeb.Models;

namespace MovieWeb.Controllers
{
    [AuthorizeAttribute_64130299Controller]
    public class Movie_64130299Controller : Controller
    {
        private MovieDatabase_64130299Entities db = new MovieDatabase_64130299Entities();
        // Phương thức để tính tổng số lượng phim
        public int GetTotalMovies()
        {
            return db.Movie_64130299.Count();
        }

        // Phương thức để cài đặt số lượng phim được cập nhật hôm nay
        public int GetUpdatedMoviesToday()
        {
            // Lọc các phim có UpdatedAt không phải null và được cập nhật hôm nay
            var updatedMoviesToday = db.Movie_64130299
                .Where(m => m.UpdatedAt.HasValue && DbFunctions.TruncateTime(m.UpdatedAt.Value) == DbFunctions.TruncateTime(DateTime.Today))
                .ToList();
            return updatedMoviesToday.Count();
        }
        // GET: Movie_64130299
        public ActionResult Index(
    string titleFilter = "",
    bool? genreIdFilter = null,
    DateTime? createdAtFrom = null,
    DateTime? createdAtTo = null,
    string genreNameFilter = "",
    string directorNameFilter = "",
    DateTime? updatedAtFrom = null,
    DateTime? updatedAtTo = null,
    string countryFilter = "",
    string actorNameFilter = "",
    string descriptionFilter = "",
    DateTime? releaseDateFilter = null,
    string accessLevelFilter = ""
)
        {
            ViewBag.TotalMovies = GetTotalMovies();
            ViewBag.UpdatedMoviesToday = GetUpdatedMoviesToday();
            // Start with the full movie list
            var movies = db.Movie_64130299.AsQueryable();

            // Apply each filter if it has a value
            if (!string.IsNullOrEmpty(titleFilter))
                movies = movies.Where(m => m.Title.Contains(titleFilter));

            if (genreIdFilter.HasValue)
                movies = movies.Where(m => m.GenreId == genreIdFilter);

            if (createdAtFrom.HasValue)
                movies = movies.Where(m => m.CreatedAt >= createdAtFrom);

            if (createdAtTo.HasValue)
                movies = movies.Where(m => m.CreatedAt <= createdAtTo);

            if (!string.IsNullOrEmpty(genreNameFilter))
                movies = movies.Where(m => m.GenreName.Contains(genreNameFilter));

            if (!string.IsNullOrEmpty(directorNameFilter))
                movies = movies.Where(m => m.DirectorName.Contains(directorNameFilter));

            if (updatedAtFrom.HasValue)
                movies = movies.Where(m => m.UpdatedAt >= updatedAtFrom);

            if (updatedAtTo.HasValue)
                movies = movies.Where(m => m.UpdatedAt <= updatedAtTo);

            if (!string.IsNullOrEmpty(countryFilter))
                movies = movies.Where(m => m.Country.Contains(countryFilter));

            if (!string.IsNullOrEmpty(actorNameFilter))
                movies = movies.Where(m => m.ActorName.Contains(actorNameFilter));

            if (!string.IsNullOrEmpty(descriptionFilter))
                movies = movies.Where(m => m.Description.Contains(descriptionFilter));

            if (releaseDateFilter.HasValue)
                movies = movies.Where(m => m.ReleaseDate == releaseDateFilter);
            if (!string.IsNullOrEmpty(accessLevelFilter))
                movies = movies.Where(m => m.Description.Contains(accessLevelFilter));

            // Return the filtered result to the view
            return View("Index",movies.ToList());
        }


        // GET: Movie_64130299/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie_64130299 movie_64130299 = db.Movie_64130299.Find(id);
            if (movie_64130299 == null)
            {
                return HttpNotFound();
            }
            return View(movie_64130299);
        }

        // GET: Movie_64130299/Create
        public ActionResult Create()
        {
            var movie = new Movie_64130299
            {
                MovieId = Guid.NewGuid().ToString(),
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                AccessLevel = "Free"
            };
            return View(movie);
        }

        // Hàm tạo ID cho Member
        private string GenerateSubscriptionId()
        {
            // Quy tắc tạo ID: MEM + số tự động tăng
            var lastMember = db.MemberSubscription_64130299
                .OrderByDescending(m => m.SubscriptionId)
                .FirstOrDefault();

            int nextId = 1;

            if (lastMember != null && !string.IsNullOrEmpty(lastMember.SubscriptionId))
            {
                // Giả sử ID có dạng MEM0001, MEM0002, ...
                string lastIdNumber = lastMember.SubscriptionId.Substring(3); // Lấy phần số
                if (int.TryParse(lastIdNumber, out int parsedId))
                {
                    nextId = parsedId + 1;
                }
            }

            return $"MEM{nextId:0000}"; // Format: MEM0001, MEM0002, ...
        }

        // POST: Movie_64130299/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MemberSubscription_64130299 memberSubscription_64130299)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Tạo ID cho Member nếu cần
                    memberSubscription_64130299.SubscriptionId = GenerateSubscriptionId();

                    // Gán giá trị mặc định nếu cần
                    if (string.IsNullOrEmpty(memberSubscription_64130299.Status))
                    {
                        memberSubscription_64130299.Status = "Active";
                    }

                    // Gán thời gian tạo và cập nhật
                    memberSubscription_64130299.CreatedAt = DateTime.Now;
                    memberSubscription_64130299.UpdatedAt = DateTime.Now;

                    // Thêm bản ghi vào bảng MemberSubscription
                    db.MemberSubscription_64130299.Add(memberSubscription_64130299);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                    .SelectMany(x => x.ValidationErrors)
                    .Select(x => $"Property: {x.PropertyName}, Error: {x.ErrorMessage}")
                    .ToList();

                ViewBag.ErrorMessages = errorMessages;
                ModelState.AddModelError("", "Validation failed: " + string.Join("; ", errorMessages));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An unexpected error occurred: " + ex.Message);
            }

            // Nếu có lỗi, truyền lại các giá trị ViewBag cho form
            ViewBag.UserId = new SelectList(db.User_64130299, "UserId", "Email", memberSubscription_64130299.UserId);
            ViewBag.PlanId = new SelectList(db.SubscriptionPlans_64130299, "PlanId", "PlanName", memberSubscription_64130299.PlanId);
            return View(memberSubscription_64130299);
        }

        // GET: Movie_64130299/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie_64130299 movie_64130299 = db.Movie_64130299.Find(id);
            if (movie_64130299 == null)
            {
                return HttpNotFound();
            }
            return View(movie_64130299);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MovieId,Title,Description,GenreId,GenreName,DirectorName,ActorName,Country,ReleaseDate,PosterUrl,TrailerUrl,AccessLevel")] Movie_64130299 movie_64130299)
        {
            if (ModelState.IsValid)
            {
                // Lấy thông tin phim hiện tại từ cơ sở dữ liệu
                var existingMovie = db.Movie_64130299.AsNoTracking().FirstOrDefault(m => m.MovieId == movie_64130299.MovieId);

                if (existingMovie != null)
                {
                    // Giữ nguyên giá trị CreatedAt
                    movie_64130299.CreatedAt = existingMovie.CreatedAt;

                    // Cập nhật UpdatedAt với thời gian hiện tại
                    movie_64130299.UpdatedAt = DateTime.Now;

                    // Đánh dấu entity là đã được sửa đổi
                    db.Entry(movie_64130299).State = EntityState.Modified;

                    // Lưu thay đổi vào cơ sở dữ liệu
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                else
                {
                    // Nếu không tìm thấy phim, trả về lỗi
                    return HttpNotFound();
                }
            }

            // Nếu có lỗi trong ModelState, trả về view với dữ liệu nhập vào
            return View(movie_64130299);
        }

        // GET: Movie_64130299/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie_64130299 movie_64130299 = db.Movie_64130299.Find(id);
            if (movie_64130299 == null)
            {
                return HttpNotFound();
            }
            return View(movie_64130299);
        }

        // POST: Movie_64130299/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Movie_64130299 movie_64130299 = db.Movie_64130299.Find(id);
            db.Movie_64130299.Remove(movie_64130299);
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
