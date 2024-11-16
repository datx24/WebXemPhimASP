using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;  // Thêm vào để sử dụng FormsAuthentication
using MovieWeb.Models;

namespace MovieWeb.Controllers
{
    public class Account_64130299Controller : Controller
    {
        private MovieDatabase_64130299Entities db = new MovieDatabase_64130299Entities();

        // Trang đăng nhập
        public ActionResult Login_64130299()
        {
            return View();
        }

        // Xử lý đăng nhập
        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            if (ModelState.IsValid)
            {
                var user = AuthenticateUser(username, password);
                if (user != null)
                {
                    // Lưu thông tin đăng nhập vào session
                    Session["Username"] = user.Username;

                    // Sử dụng   để tạo cookie đăng nhập
                    FormsAuthentication.SetAuthCookie(user.Username, false); // Đăng nhập mà không cần nhớ

                    // Thêm thông báo vào TempData
                    TempData["SuccessMessage"] = "Đăng nhập thành công!";
                    return RedirectToAction("Index", "User_64130299"); // Redirect đến trang quản trị
                }
                else
                {
                    // Thêm lỗi vào ModelState nếu tên đăng nhập hoặc mật khẩu không đúng
                    ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng.");
                }
            }
            return View();
        }


        // Phương thức xác thực người dùng
        private AdminUsers_64130299 AuthenticateUser(string username, string password)
        {
            // Tìm người dùng trong cơ sở dữ liệu
            var user = db.AdminUsers_64130299.FirstOrDefault(u => u.Username == username);

            if (user != null)
            {
                // Băm mật khẩu người dùng nhập vào
                string hashedPassword = HashPassword(password);  // Hàm băm SHA-256

                // So sánh giá trị băm của mật khẩu nhập vào với mật khẩu đã lưu trong cơ sở dữ liệu
                if (hashedPassword == user.PasswordHash)
                {
                    return user; // Mật khẩu đúng
                }
            }

            return null; // Mật khẩu sai hoặc người dùng không tồn tại
        }

        // Hàm băm mật khẩu với SHA-256
        public static string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // Băm mật khẩu
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Chuyển đổi byte array thành chuỗi hex
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
            return RedirectToAction("Login_64130299");
        }
    }
}
