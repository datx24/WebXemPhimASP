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
using System.Web.Security;
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

        // Trang đăng nhập của người quản lý
        public ActionResult Login_64130299()
        {
            return View();
        }

        // Xử lý đăng nhập
        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            if (ModelState.IsValid)
            {
                var user = AuthenticateUser(email, password);
                if (user != null)
                {
                    // Lưu thông tin đăng nhập vào session
                    Session["Email"] = user.Email;

                    // Tạo cookie đăng nhập
                    FormsAuthentication.SetAuthCookie(user.Email, false);

                    // Thêm thông báo thành công vào TempData
                    TempData["SuccessMessage"] = "Đăng nhập thành công";
                    return RedirectToAction("Home_64130299", "Home_64130299"); // Redirect đến trang quản trị
                }
                else
                {
                    // Thêm thông báo lỗi vào TempData
                    TempData["ErrorMessage"] = "Tên đăng nhập hoặc mật khẩu không đúng.";
                }
            }
            return View();
        }

        // Phương thức xác thực người dùng
        private User_64130299 AuthenticateUser(string email, string password)
        {
            var user = db.User_64130299.FirstOrDefault(u => u.Email == email); ;
            if (user != null)
            {
                string hashedPassword = HashPassword(password);
                if (hashedPassword == user.Password)
                {
                    return user;
                }
            }
            return null;
        }

        // Hàm băm mật khẩu với SHA-256
        public static string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }

        // Đăng xuất
        public ActionResult Logout()
        {
            // Xóa toàn bộ session
            Session.Clear();

            // Sử dụng FormsAuthentication để xóa cookie đăng nhập
            FormsAuthentication.SignOut();

            // Thêm thông báo cho lần đăng nhập sau (nếu cần)
            TempData["InfoMessage"] = "Bạn đã đăng xuất thành công.";

            // Điều hướng về trang đăng nhập
            return RedirectToAction("Home_64130299", "Home_64130299");
        }
        // GET: User_64130299/Register
        public ActionResult Register_64130299()
        {
            return View(); // Hiển thị view đăng ký
        }

        // POST: User_64130299/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User_64130299 user_64130299)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra xem email đã tồn tại chưa
                var existingUser = db.User_64130299.FirstOrDefault(u => u.Email == user_64130299.Email);
                if (existingUser != null)
                {
                    // Nếu email đã tồn tại, trả về thông báo lỗi
                    TempData["ErrorMessage"] = "Email này đã được đăng ký.";
                    return View(user_64130299);
                }

                // Băm mật khẩu trước khi lưu vào cơ sở dữ liệu
                user_64130299.Password = HashPassword(user_64130299.Password);

                // Tạo UserId mới và gán cho người dùng
                user_64130299.UserId = GenerateUserId(); // Tạo UserId tự động
                user_64130299.CreatedAt = DateTime.Now;
                user_64130299.UpdatedAt = DateTime.Now;

                // Thêm người dùng mới vào cơ sở dữ liệu
                db.User_64130299.Add(user_64130299);
                db.SaveChanges();

                // Thêm thông báo thành công
                TempData["SuccessMessage"] = "Đăng ký thành công! Bạn có thể đăng nhập ngay.";

                return RedirectToAction("Login_64130299"); // Chuyển hướng tới trang đăng nhập
            }

            // Nếu có lỗi, trả lại view với thông báo lỗi
            return View(user_64130299);
        }

    }
}
