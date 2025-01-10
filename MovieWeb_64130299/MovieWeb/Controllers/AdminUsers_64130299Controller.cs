using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
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
    public class AdminUsers_64130299Controller : Controller
    {
        private MovieDatabase_64130299Entities db = new MovieDatabase_64130299Entities();
        public string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // Mã hóa mật khẩu thành dạng hash
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Chuyển đổi mảng byte thành chuỗi hex
                StringBuilder builder = new StringBuilder();
                foreach (byte byteValue in bytes)
                {
                    builder.Append(byteValue.ToString("x2"));
                }
                return builder.ToString();
            }
        }

        // GET: AdminUsers_64130299
        public ActionResult Index()
        {
            return View(db.AdminUsers_64130299.ToList());
        }

        // GET: AdminUsers_64130299/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdminUsers_64130299 adminUsers_64130299 = db.AdminUsers_64130299.Find(id);
            if (adminUsers_64130299 == null)
            {
                return HttpNotFound();
            }
            return View(adminUsers_64130299);
        }

        // GET: AdminUsers_64130299/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminUsers_64130299/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Username,PasswordHash,Role")] AdminUsers_64130299 adminUsers_64130299)
        {
            if (ModelState.IsValid)
            {
                // Mã hóa mật khẩu trước khi lưu
                adminUsers_64130299.PasswordHash = HashPassword(adminUsers_64130299.PasswordHash);
                db.AdminUsers_64130299.Add(adminUsers_64130299);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(adminUsers_64130299);
        }

        // GET: AdminUsers_64130299/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdminUsers_64130299 adminUsers_64130299 = db.AdminUsers_64130299.Find(id);
            if (adminUsers_64130299 == null)
            {
                return HttpNotFound();
            }
            return View(adminUsers_64130299);
        }

        // POST: AdminUsers_64130299/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Username,PasswordHash")] AdminUsers_64130299 adminUsers_64130299)
        {
            if (ModelState.IsValid)
            {
                // Băm mật khẩu trước khi lưu
                adminUsers_64130299.PasswordHash = HashPassword(adminUsers_64130299.PasswordHash);
                db.Entry(adminUsers_64130299).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(adminUsers_64130299);
        }

        // GET: AdminUsers_64130299/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdminUsers_64130299 adminUsers_64130299 = db.AdminUsers_64130299.Find(id);
            if (adminUsers_64130299 == null)
            {
                return HttpNotFound();
            }
            return View(adminUsers_64130299);
        }

        // POST: AdminUsers_64130299/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AdminUsers_64130299 adminUsers_64130299 = db.AdminUsers_64130299.Find(id);
            db.AdminUsers_64130299.Remove(adminUsers_64130299);
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
