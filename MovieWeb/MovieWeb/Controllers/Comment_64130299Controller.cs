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
    public class Comment_64130299Controller : Controller
    {
        private MovieDatabase_64130299Entities db = new MovieDatabase_64130299Entities();

        // GET: Comment_64130299
        public ActionResult Index()
        {
            var comment_64130299 = db.Comment_64130299.Include(c => c.Movie_64130299).Include(c => c.User_64130299);
            return View(comment_64130299.ToList());
        }

        // GET: Comment_64130299/Details/5
        public ActionResult Details(int? id)
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
            ViewBag.MovieId = new SelectList(db.Movie_64130299, "MovieId", "Title");
            ViewBag.UserId = new SelectList(db.User_64130299, "UserId", "Email");
            return View();
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
                db.Comment_64130299.Add(comment_64130299);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MovieId = new SelectList(db.Movie_64130299, "MovieId", "Title", comment_64130299.MovieId);
            ViewBag.UserId = new SelectList(db.User_64130299, "UserId", "Email", comment_64130299.UserId);
            return View(comment_64130299);
        }

        // GET: Comment_64130299/Edit/5
        public ActionResult Edit(int? id)
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
        public ActionResult Delete(int? id)
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
        public ActionResult DeleteConfirmed(int id)
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
