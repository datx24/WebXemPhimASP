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
    public class Movie_64130299Controller : Controller
    {
        private MovieDatabase_64130299Entities db = new MovieDatabase_64130299Entities();

        // GET: Movie_64130299
        public ActionResult Index()
        {
            return View(db.Movie_64130299.ToList());
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
            return View();
        }

        // POST: Movie_64130299/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MovieId,Title,Description,GenreId,GenreName,DirectorName,ActorName,Country,ReleaseDate,PosterUrl,TrailerUrl,CreatedAt,UpdatedAt")] Movie_64130299 movie_64130299)
        {
            if (ModelState.IsValid)
            {
                db.Movie_64130299.Add(movie_64130299);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(movie_64130299);
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

        // POST: Movie_64130299/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MovieId,Title,Description,GenreId,GenreName,DirectorName,ActorName,Country,ReleaseDate,PosterUrl,TrailerUrl,CreatedAt,UpdatedAt")] Movie_64130299 movie_64130299)
        {
            if (ModelState.IsValid)
            {
                db.Entry(movie_64130299).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
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
