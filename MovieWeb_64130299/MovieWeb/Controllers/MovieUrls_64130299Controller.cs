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
    public class MovieUrls_64130299Controller : Controller
    {
        private MovieDatabase_64130299Entities db = new MovieDatabase_64130299Entities();

        // GET: MovieUrls_64130299
        public ActionResult Index()
        {
            var movieUrls_64130299 = db.MovieUrls_64130299.Include(m => m.Movie_64130299);
            return View(movieUrls_64130299.ToList());
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
            ViewBag.MovieId = new SelectList(db.Movie_64130299, "MovieId", "Title");
            return View();
        }

        // POST: MovieUrls_64130299/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MovieUrlId,MovieId,Url")] MovieUrls_64130299 movieUrls_64130299)
        {
            if (ModelState.IsValid)
            {
                db.MovieUrls_64130299.Add(movieUrls_64130299);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

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
