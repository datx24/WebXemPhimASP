using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MovieWeb.Models;

namespace MovieWeb.Controllers
{
    [AuthorizeAttribute_64130299Controller]
    public class Rating_64130299Controller : Controller
    {
        private MovieDatabase_64130299Entities db = new MovieDatabase_64130299Entities();

        // GET: Rating_64130299
        public ActionResult Index()
        {
            // Tính trung bình đánh giá cho mỗi phim
            var movieRatings = db.Rating_64130299
                .GroupBy(r => r.MovieId)
                .Select(g => new
                {
                    MovieId = g.Key,
                    AverageRating = g.Average(r => r.Rating)
                })
                .ToList();

            // Lấy phim có điểm trung bình cao nhất
            var highestRatedMovie = movieRatings.OrderByDescending(m => m.AverageRating).FirstOrDefault();
            if (highestRatedMovie != null)
            {
                var movieHigh = db.Movie_64130299.FirstOrDefault(m => m.MovieId == highestRatedMovie.MovieId);
                ViewBag.HighestRatedMovie = movieHigh;
                ViewBag.HighestRating = highestRatedMovie.AverageRating;
            }

            // Lấy phim có điểm trung bình thấp nhất
            var lowestRatedMovie = movieRatings.OrderBy(m => m.AverageRating).FirstOrDefault();
            if (lowestRatedMovie != null)
            {
                var movieLow = db.Movie_64130299.FirstOrDefault(m => m.MovieId == lowestRatedMovie.MovieId);
                ViewBag.LowestRatedMovie = movieLow;
                ViewBag.LowestRating = lowestRatedMovie.AverageRating;
            }

            // Truyền tất cả các đánh giá vào View
            var rating_64130299 = db.Rating_64130299.Include(r => r.Movie_64130299).Include(r => r.User_64130299);
            return View(rating_64130299.ToList());
        }


        // GET: Rating_64130299/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rating_64130299 rating_64130299 = db.Rating_64130299.Find(id);
            if (rating_64130299 == null)
            {
                return HttpNotFound();
            }
            return View(rating_64130299);
        }

        // GET: Rating_64130299/Create
        public ActionResult Create()
        {
            var rating = new Rating_64130299
            {
                RatingId = Guid.NewGuid().ToString(),
                CreatedAt = DateTime.Now,
            };
            ViewBag.MovieId = new SelectList(db.Movie_64130299, "MovieId", "Title");
            ViewBag.UserId = new SelectList(db.User_64130299, "UserId", "Email");
            return View(rating);
        }

        // POST: Rating_64130299/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RatingId,UserId,MovieId,Rating,CreatedAt")] Rating_64130299 rating_64130299)
        {
            if (ModelState.IsValid)
            {
                rating_64130299.RatingId = Guid.NewGuid().ToString();
                db.Rating_64130299.Add(rating_64130299);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MovieId = new SelectList(db.Movie_64130299, "MovieId", "Title", rating_64130299.MovieId);
            ViewBag.UserId = new SelectList(db.User_64130299, "UserId", "Email", rating_64130299.UserId);
            return View(rating_64130299);
        }

        // GET: Rating_64130299/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rating_64130299 rating_64130299 = db.Rating_64130299.Find(id);
            if (rating_64130299 == null)
            {
                return HttpNotFound();
            }
            ViewBag.MovieId = new SelectList(db.Movie_64130299, "MovieId", "Title", rating_64130299.MovieId);
            ViewBag.UserId = new SelectList(db.User_64130299, "UserId", "Email", rating_64130299.UserId);
            return View(rating_64130299);
        }

        // POST: Rating_64130299/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RatingId,UserId,MovieId,Rating,CreatedAt")] Rating_64130299 rating_64130299)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rating_64130299).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MovieId = new SelectList(db.Movie_64130299, "MovieId", "Title", rating_64130299.MovieId);
            ViewBag.UserId = new SelectList(db.User_64130299, "UserId", "Email", rating_64130299.UserId);
            return View(rating_64130299);
        }

        // GET: Rating_64130299/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rating_64130299 rating_64130299 = db.Rating_64130299.Find(id);
            if (rating_64130299 == null)
            {
                return HttpNotFound();
            }
            return View(rating_64130299);
        }

        // POST: Rating_64130299/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Rating_64130299 rating_64130299 = db.Rating_64130299.Find(id);
            db.Rating_64130299.Remove(rating_64130299);
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
