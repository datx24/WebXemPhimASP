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
    public class WatchHistory_64130299Controller : Controller
    {
        private MovieDatabase_64130299Entities db = new MovieDatabase_64130299Entities();

        // GET: WatchHistory_64130299
        public ActionResult Index()
        {
            var watchHistory_64130299 = db.WatchHistory_64130299.Include(w => w.Movie_64130299).Include(w => w.User_64130299);
            return View(watchHistory_64130299.ToList());
        }

        // GET: WatchHistory_64130299/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WatchHistory_64130299 watchHistory_64130299 = db.WatchHistory_64130299.Find(id);
            if (watchHistory_64130299 == null)
            {
                return HttpNotFound();
            }
            return View(watchHistory_64130299);
        }

        // GET: WatchHistory_64130299/Create
        public ActionResult Create()
        {
            ViewBag.MovieId = new SelectList(db.Movie_64130299, "MovieId", "Title");
            ViewBag.UserId = new SelectList(db.User_64130299, "UserId", "Email");
            return View();
        }

        // POST: WatchHistory_64130299/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "HistoryId,UserId,MovieId,LastWatchedTime,CreatedAt")] WatchHistory_64130299 watchHistory_64130299)
        {
            if (ModelState.IsValid)
            {
                db.WatchHistory_64130299.Add(watchHistory_64130299);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MovieId = new SelectList(db.Movie_64130299, "MovieId", "Title", watchHistory_64130299.MovieId);
            ViewBag.UserId = new SelectList(db.User_64130299, "UserId", "Email", watchHistory_64130299.UserId);
            return View(watchHistory_64130299);
        }

        // GET: WatchHistory_64130299/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WatchHistory_64130299 watchHistory_64130299 = db.WatchHistory_64130299.Find(id);
            if (watchHistory_64130299 == null)
            {
                return HttpNotFound();
            }
            ViewBag.MovieId = new SelectList(db.Movie_64130299, "MovieId", "Title", watchHistory_64130299.MovieId);
            ViewBag.UserId = new SelectList(db.User_64130299, "UserId", "Email", watchHistory_64130299.UserId);
            return View(watchHistory_64130299);
        }

        // POST: WatchHistory_64130299/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "HistoryId,UserId,MovieId,LastWatchedTime,CreatedAt")] WatchHistory_64130299 watchHistory_64130299)
        {
            if (ModelState.IsValid)
            {
                db.Entry(watchHistory_64130299).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MovieId = new SelectList(db.Movie_64130299, "MovieId", "Title", watchHistory_64130299.MovieId);
            ViewBag.UserId = new SelectList(db.User_64130299, "UserId", "Email", watchHistory_64130299.UserId);
            return View(watchHistory_64130299);
        }

        // GET: WatchHistory_64130299/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WatchHistory_64130299 watchHistory_64130299 = db.WatchHistory_64130299.Find(id);
            if (watchHistory_64130299 == null)
            {
                return HttpNotFound();
            }
            return View(watchHistory_64130299);
        }

        // POST: WatchHistory_64130299/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            WatchHistory_64130299 watchHistory_64130299 = db.WatchHistory_64130299.Find(id);
            db.WatchHistory_64130299.Remove(watchHistory_64130299);
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
