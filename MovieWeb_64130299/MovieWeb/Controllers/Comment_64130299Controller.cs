using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
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
    public class Comment_64130299Controller : Controller
    {
        private MovieDatabase_64130299Entities db = new MovieDatabase_64130299Entities();

        // GET: Comment_64130299
        public ActionResult Index()
        {
            // Truy vấn nhóm bình luận theo MovieId, đếm số lượng bình luận cho mỗi phim
            var mostCommentedMovieGroup = db.Comment_64130299
                .GroupBy(c => c.MovieId)
                .OrderByDescending(g => g.Count())  // Sắp xếp theo số lượng bình luận giảm dần
                .FirstOrDefault(); // Lấy phim có lượt bình luận nhiều nhất

            // Nếu có phim có bình luận
            if (mostCommentedMovieGroup != null)
            {
                var movieId = mostCommentedMovieGroup.Key;  // Lấy MovieId của phim có lượt bình luận nhiều nhất
                var mostCommentedMovie = db.Movie_64130299
                    .FirstOrDefault(m => m.MovieId == movieId); // Lấy thông tin phim từ bảng Movie_64130299

                // Truyền phim có lượt bình luận nhiều nhất vào ViewBag
                ViewBag.MostCommentedMovie = mostCommentedMovie;
                ViewBag.CommentCount = mostCommentedMovieGroup.Count(); // Truyền số lượt bình luận
            }

            // Truyền tất cả bình luận vào View
            var comment_64130299 = db.Comment_64130299.Include(c => c.Movie_64130299).Include(c => c.User_64130299);
            return View(comment_64130299.ToList());
        }


        // GET: Comment_64130299/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment_64130299 comment_64130299 = db.Comment_64130299.Find(id);
            if (comment_64130299 == null)
            {
                return HttpNotFound();
            }
            return View(comment_64130299);
        }

        // GET: Comment_64130299/Create
        public ActionResult Create()
        {
            var comment = new Comment_64130299
            {
                CommentId = Guid.NewGuid().ToString(),
                CreatedAt = DateTime.Now,
            };

            ViewBag.MovieId = new SelectList(db.Movie_64130299, "MovieId", "Title");
            ViewBag.UserId = new SelectList(db.User_64130299, "UserId", "Email");
            return View(comment);
        }

        // POST: Comment_64130299/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CommentId,UserId,MovieId,Content,CreatedAt")] Comment_64130299 comment_64130299)
        {
            if (ModelState.IsValid)
            {
                comment_64130299.CommentId = Guid.NewGuid().ToString();
                comment_64130299.CreatedAt = DateTime.Now;
                db.Comment_64130299.Add(comment_64130299);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MovieId = new SelectList(db.Movie_64130299, "MovieId", "Title", comment_64130299.MovieId);
            ViewBag.UserId = new SelectList(db.User_64130299, "UserId", "Email", comment_64130299.UserId);
            return View(comment_64130299);
        }

        // GET: Comment_64130299/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment_64130299 comment_64130299 = db.Comment_64130299.Find(id);
            if (comment_64130299 == null)
            {
                return HttpNotFound();
            }
            ViewBag.MovieId = new SelectList(db.Movie_64130299, "MovieId", "Title", comment_64130299.MovieId);
            ViewBag.UserId = new SelectList(db.User_64130299, "UserId", "Email", comment_64130299.UserId);
            return View(comment_64130299);
        }

        // POST: Comment_64130299/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CommentId,UserId,MovieId,Content,CreatedAt")] Comment_64130299 comment_64130299)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comment_64130299).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MovieId = new SelectList(db.Movie_64130299, "MovieId", "Title", comment_64130299.MovieId);
            ViewBag.UserId = new SelectList(db.User_64130299, "UserId", "Email", comment_64130299.UserId);
            return View(comment_64130299);
        }

        // GET: Comment_64130299/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment_64130299 comment_64130299 = db.Comment_64130299.Find(id);
            if (comment_64130299 == null)
            {
                return HttpNotFound();
            }
            return View(comment_64130299);
        }

        // POST: Comment_64130299/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Comment_64130299 comment_64130299 = db.Comment_64130299.Find(id);
            db.Comment_64130299.Remove(comment_64130299);
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
