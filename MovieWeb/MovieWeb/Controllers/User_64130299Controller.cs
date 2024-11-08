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
    public class User_64130299Controller : Controller
    {
        private MovieDatabase_64130299Entities db = new MovieDatabase_64130299Entities();

        // GET: User_64130299
        public ActionResult Index(string emailFilter = "", string passwordFilter = "", string usernameFilter = "", DateTime? createdAtFrom = null, DateTime? createdAtTo = null, DateTime? updatedAtFrom = null, DateTime? updatedAtTo = null)
        {
            var users = db.User_64130299.AsQueryable(); // Lấy danh sách tất cả người dùng

            // Áp dụng bộ lọc nếu có
            if (!string.IsNullOrEmpty(emailFilter))
            {
                users = users.Where(u => u.Email.Contains(emailFilter));
            }
            if (!string.IsNullOrEmpty(passwordFilter))
            {
                users = users.Where(u => u.Password.Contains(passwordFilter));
            }
            if (!string.IsNullOrEmpty(usernameFilter))
            {
                users = users.Where(u => u.Username.Contains(usernameFilter));
            }
            if (createdAtFrom.HasValue)
            {
                users = users.Where(u => u.CreatedAt >= createdAtFrom);
            }
            if (createdAtTo.HasValue)
            {
                users = users.Where(u => u.CreatedAt <= createdAtTo);
            }
            if (updatedAtFrom.HasValue)
            {
                users = users.Where(u => u.UpdatedAt >= updatedAtFrom);
            }
            if (updatedAtTo.HasValue)
            {
                users = users.Where(u => u.UpdatedAt <= updatedAtTo);
            }

            // Trả về view với danh sách đã lọc
            ViewBag.EmailFilter = emailFilter;
            ViewBag.PasswordFilter = passwordFilter;
            ViewBag.UsernameFilter = usernameFilter;
            ViewBag.CreatedAtFrom = createdAtFrom;
            ViewBag.CreatedAtTo = createdAtTo;
            ViewBag.UpdatedAtFrom = updatedAtFrom;
            ViewBag.UpdatedAtTo = updatedAtTo;

            return View("Index",users.ToList());
        }



        // GET: User_64130299/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User_64130299 user_64130299 = db.User_64130299.Find(id);
            if (user_64130299 == null)
            {
                return HttpNotFound();
            }
            return View(user_64130299);
        }

        // GET: User_64130299/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User_64130299/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserId,Email,Password,Username,CreatedAt,UpdatedAt")] User_64130299 user_64130299)
        {
            if (ModelState.IsValid)
            {
                db.User_64130299.Add(user_64130299);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user_64130299);
        }

        // GET: User_64130299/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User_64130299 user_64130299 = db.User_64130299.Find(id);
            if (user_64130299 == null)
            {
                return HttpNotFound();
            }
            return View(user_64130299);
        }

        // POST: User_64130299/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserId,Email,Password,Username,CreatedAt,UpdatedAt")] User_64130299 user_64130299)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user_64130299).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user_64130299);
        }

        // GET: User_64130299/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User_64130299 user_64130299 = db.User_64130299.Find(id);
            if (user_64130299 == null)
            {
                return HttpNotFound();
            }
            return View(user_64130299);
        }

        // POST: User_64130299/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            User_64130299 user_64130299 = db.User_64130299.Find(id);
            db.User_64130299.Remove(user_64130299);
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
