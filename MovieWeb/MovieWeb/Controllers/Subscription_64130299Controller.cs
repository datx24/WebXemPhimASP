using MovieWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MovieWeb.Controllers
{
    
    public class Subscription_64130299Controller : Controller
    {
        private MovieDatabase_64130299Entities db = new MovieDatabase_64130299Entities();
        // GET: Subscription_64130299
        public ActionResult Create_64130299()
        {
            return View();
        }

        // POST: Subscription/Create (Xử lý việc đăng ký gói thành viên)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string accessLevel, DateTime expiryDate)
        {
            // Lấy UserId từ Session
            var userId = Session["UserId"] as string;

            if (string.IsNullOrEmpty(userId))
            {
                // Xử lý khi userId không tồn tại, có thể chuyển hướng đến trang đăng nhập
                return RedirectToAction("Login_64130299", "User_64130299");
            }

            // Truyền UserId vào View thông qua ViewBag
            ViewBag.UserId = userId;

            // Tạo SubscriptionId mới
            var subscriptionId = Guid.NewGuid().ToString();

            var newSubscription = new MemberSubscription_64130299
            {
                SubscriptionId = subscriptionId,
                UserId = userId,
                StartDate = DateTime.Now,
                ExpiryDate = expiryDate,
                AccessLevel = accessLevel,
                Status = "Active",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            // Lưu vào cơ sở dữ liệu
            db.MemberSubscription_64130299.Add(newSubscription);
            db.SaveChanges();

            // Chuyển hướng người dùng sang trang xác nhận
            return RedirectToAction("Confirmation_64130299"); // Hoặc đến một action khác
        }


        // GET: Subscription/Confirmation (Trang xác nhận thành công)
        public ActionResult Confirmation_64130299()
        {
            return View();
        }

        // GET: Subscription/Failure (Trang thất bại nếu có lỗi thanh toán)
        public ActionResult Failure_64130299()
        {
            return View();
        }
    }
}