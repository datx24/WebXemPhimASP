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
    public class Favorite_64130299Controller : Controller
    {
        private MovieDatabase_64130299Entities db = new MovieDatabase_64130299Entities();

        // GET: Favorite_64130299
        public ActionResult Index()
        {
            var favorite_64130299 = db.Favorite_64130299.Include(f => f.Movie_64130299).Include(f => f.User_64130299);
            return View(favorite_64130299.ToList());
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
            ViewBag.MovieId = new SelectList(db.Movie_64130299, "MovieId", "Title");
            ViewBag.UserId = new SelectList(db.User_64130299, "UserId", "Email");
            return View();
        }

        // POST: Favorite_64130299/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FavoriteId,UserId,MovieId,CreatedAt")] Favorite_64130299 favorite_64130299)
        {
            if (ModelState.IsValid)
            {
                db.Favorite_64130299.Add(favorite_64130299);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MovieId = new SelectList(db.Movie_64130299, "MovieId", "Title", favorite_64130299.MovieId);
            ViewBag.UserId = new SelectList(db.User_64130299, "UserId", "Email", favorite_64130299.UserId);
            return View(favorite_64130299);
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
