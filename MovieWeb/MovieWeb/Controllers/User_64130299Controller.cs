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

        string GenerateUserId()
        {
            // Fetch the current year
            string year = DateTime.Now.Year.ToString();

            // Fetch the total count of users and increment by 1 for uniqueness
            int userCount = db.User_64130299.Count() + 1;

            // Format the UserId, for example, "USR2024-001"
            string userId = $"USR{year}-{userCount:D3}"; // `D3` pads with zeros to make it 3 digits

            return userId;
        }
        [AuthorizeAttribute_64130299Controller]
        public ActionResult Index(string emailFilter = "", string passwordFilter = "", string usernameFilter = "", DateTime? createdAtFrom = null, DateTime? createdAtTo = null, DateTime? updatedAtFrom = null, DateTime? updatedAtTo = null)
        {
            // Cập nhật giá trị cho ViewBag để hiển thị trong form
            ViewBag.EmailFilter = emailFilter;
            ViewBag.PasswordFilter = passwordFilter;
            ViewBag.UsernameFilter = usernameFilter;
            ViewBag.CreatedAtFrom = createdAtFrom;
            ViewBag.CreatedAtTo = createdAtTo;
            ViewBag.UpdatedAtFrom = updatedAtFrom;
            ViewBag.UpdatedAtTo = updatedAtTo;

            // Lọc dữ liệu
            var users = db.User_64130299.AsQueryable();

            // Kiểm tra các bộ lọc và áp dụng
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

            // Trả về danh sách người dùng đã lọc
            return View(users.ToList());
        }



        [AuthorizeAttribute_64130299Controller]
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
        [AuthorizeAttribute_64130299Controller]
        // GET: User_64130299/Create
        public ActionResult Create()
        {
            var user = new User_64130299
            {
                UserId = GenerateUserId(), // Tạo ID tự động và gán cho UserId
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
            return View(user);
        }

        // POST: User_64130299/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        // POST: User_64130299/Create
        [AuthorizeAttribute_64130299Controller]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserId,Email,Password,Username,CreatedAt,UpdatedAt")] User_64130299 user_64130299)
        {
            if (ModelState.IsValid)
            {
                user_64130299.UserId = GenerateUserId(); // Gọi GenerateUserId khi lưu dữ liệu

                db.User_64130299.Add(user_64130299);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user_64130299);
        }


        // GET: User_64130299/Edit/5


        // GET: User_64130299/Edit/5
        [AuthorizeAttribute_64130299Controller]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Lấy đối tượng User từ cơ sở dữ liệu theo UserId (id)
            User_64130299 user_64130299 = db.User_64130299.Find(id);
            user_64130299.UpdatedAt = DateTime.Now;

            if (user_64130299 == null)
            {
                return HttpNotFound();
            }

            // Trả về View với model là đối tượng user đã tìm thấy từ cơ sở dữ liệu
            return View(user_64130299);
        }

        // POST: User_64130299/Edit/5
        [AuthorizeAttribute_64130299Controller]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User_64130299 model)
        {
            if (ModelState.IsValid)
            {
                // Lấy đối tượng User từ cơ sở dữ liệu theo UserId
                User_64130299 user_64130299 = db.User_64130299.Find(model.UserId);

                if (user_64130299 != null)
                {
                    // Cập nhật các trường cần thiết
                    user_64130299.Email = model.Email;
                    user_64130299.Password = model.Password;
                    user_64130299.Username = model.Username;
                    user_64130299.UpdatedAt = DateTime.Now; // Cập nhật thời gian cập nhật

                    // Lưu lại thay đổi vào cơ sở dữ liệu
                    db.SaveChanges();

                    return RedirectToAction("Index"); // Hoặc một nơi bạn muốn chuyển hướng
                }
                else
                {
                    return HttpNotFound();
                }
            }

            // Nếu có lỗi validation, trả lại view với lỗi
            return View(model);
        }


        // GET: User_64130299/Delete/5
        [AuthorizeAttribute_64130299Controller]
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
        [AuthorizeAttribute_64130299Controller]

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
        [AuthorizeAttribute_64130299Controller]
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
