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
        public ActionResult Index()
        {
            return View();
        }

        // POST: Subscription/Create (Xử lý việc đăng ký gói thành viên)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string userId, string accessLevel, DateTime expiryDate)
        {
            // Tạo SubscriptionId mới
            var subscriptionId = Guid.NewGuid().ToString(); // hoặc dùng một hệ thống mã khác

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

            // Chuyển hướng người dùng sang trang thanh toán thành công hoặc giao diện khác
            return RedirectToAction("Confirmation"); // Hoặc đến một action khác
        }

        // GET: Subscription/Confirmation (Trang xác nhận thành công)
        public ActionResult Confirmation()
        {
            return View();
        }

        // GET: Subscription/Failure (Trang thất bại nếu có lỗi thanh toán)
        public ActionResult Failure()
        {
            return View();
        }
    }
}