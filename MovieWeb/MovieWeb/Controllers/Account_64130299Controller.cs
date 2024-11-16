using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MovieWeb.Models;

namespace MovieWeb.Controllers
{
    public class Account_64130299Controller : Controller
    {
        // Danh sách người dùng quản trị viên (có thể thay đổi sau này)
        private static List<User_64130299> _adminUsers = new List<User_64130299>
        {
            new User_64130299 { Username = "admin", Password = "password" }
        };

        // Trang đăng nhập
        public ActionResult Login_64130299()
        {
            return View();
        }

        // Xử lý đăng nhập
        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            var user = AuthenticateUser(username, password);
            if (user != null)
            {
                // Lưu thông tin đăng nhập vào session
                Session["Username"] = user.Username;

                // Thêm thông báo vào TempData
                TempData["SuccessMessage"] = "Đăng nhập thành công!";
                return RedirectToAction("Index", "User_64130299"); // Redirect đến trang quản trị
            }
            else
            {
                ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng.");
            }
            return View();
        }


        // Phương thức xác thực người dùng
        private User_64130299 AuthenticateUser(string username, string password)
        {
            // Tìm người dùng trong danh sách quản trị viên
            return _adminUsers.FirstOrDefault(u => u.Username == username && u.Password == password);
        }

        // Đăng xuất
        public ActionResult Logout()
        {
            // Xóa toàn bộ session
            Session.Clear();

            // Thêm thông báo cho lần đăng nhập sau (nếu cần)
            TempData["InfoMessage"] = "Bạn đã đăng xuất thành công.";

            // Điều hướng về trang đăng nhập
            return RedirectToAction("Login_64130299");
        }

    }


}
