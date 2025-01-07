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
    public class MemberSubscription_64130299Controller : Controller
    {
        private MovieDatabase_64130299Entities db = new MovieDatabase_64130299Entities();

        // Hàm tạo mã SubscriptionId duy nhất
        private string GenerateUniqueSubscriptionId()
        {
            var lastSubscription = db.MemberSubscription_64130299
                .OrderByDescending(m => m.SubscriptionId)
                .FirstOrDefault();

            int nextId = 1;
            if (lastSubscription != null && !string.IsNullOrEmpty(lastSubscription.SubscriptionId))
            {
                // Giả sử SubscriptionId có dạng "SUBXXXX"
                string lastIdNumber = lastSubscription.SubscriptionId.Substring(3); // Lấy phần số
                if (int.TryParse(lastIdNumber, out int parsedId))
                {
                    nextId = parsedId + 1;
                }
            }

            return $"SUB{nextId:0000}";
        }

        // GET: MemberSubscription_64130299
        public ActionResult Index()
        {
            var memberSubscription_64130299 = db.MemberSubscription_64130299.Include(m => m.User_64130299).Include(m => m.SubscriptionPlans_64130299);

            // Tính số lượt đăng ký hôm nay
            var today = DateTime.Today;
            int countToday = memberSubscription_64130299.Count(s => s.CreatedAt.HasValue && DbFunctions.TruncateTime(s.CreatedAt) == today);

            // Tính tổng doanh thu
            var subscriptions = memberSubscription_64130299.ToList(); // Chuyển dữ liệu vào bộ nhớ
            decimal totalRevenue = subscriptions.Sum(s => s.AmountPaid ?? 0); // Sử dụng `??` để xử lý giá trị null

            // Gửi dữ liệu vào ViewBag
            ViewBag.CountToday = countToday;
            ViewBag.TotalRevenue = totalRevenue;

            return View(subscriptions);
        }

        // GET: MemberSubscription_64130299/Details
        public ActionResult Details()
        {
            var memberSubscription = TempData["MemberSubscription"] as MemberSubscription_64130299;
            if (memberSubscription == null)
            {
                return RedirectToAction("Create");
            }

            return View(memberSubscription);
        }


        // GET: MemberSubscription_64130299/Create
        public ActionResult Create()
        {
            // Kiểm tra xem người dùng đã đăng nhập chưa
            if (Session["UserId"] == null)
            {
                // Nếu chưa đăng nhập, chuyển hướng đến trang đăng nhập
                return RedirectToAction("Login_64130299", "User_64130299");
            }

            // Lấy UserId từ Session và ép kiểu thành string
            string userId = Session["UserId"].ToString();  // Chuyển giá trị trong session sang string

            // Gán UserId vào ViewBag để có thể sử dụng trong view
            ViewBag.UserId = userId;

            // Khởi tạo ngày bắt đầu và ngày kết thúc (mặc định là 1 tháng sau)
            var startDate = DateTime.Now;
            var expiryDate = startDate.AddMonths(1);  // Mặc định là 1 tháng

            // Truyền ngày bắt đầu và kết thúc qua ViewBag
            ViewBag.StartDate = startDate.ToString("yyyy-MM-dd");
            ViewBag.ExpiryDate = expiryDate.ToString("yyyy-MM-dd");

            // Tạo mã duy nhất cho SubscriptionId
            string newSubscriptionId = GenerateUniqueSubscriptionId();

            // Truyền mã SubscriptionId qua ViewBag
            ViewBag.NewSubscriptionId = newSubscriptionId;

            // Truyền giá trị mặc định cho các trường khác
            ViewBag.Status = "Active";
            ViewBag.RenewalDate = (DateTime?)null;
            ViewBag.AccessLevel = "Premium";  // Mặc định là "Premium"

            var plans = db.SubscriptionPlans_64130299.ToList();
            ViewBag.PlanId = new SelectList(plans, "PlanId", "PlanName");

            // Kiểm tra nếu người dùng đã có thẻ thành viên trước đó
            var existingSubscription = db.MemberSubscription_64130299.FirstOrDefault(m => m.UserId == userId);
            if (existingSubscription != null)
            {
                // Nếu người dùng đã có thẻ, gửi thông báo lỗi và quay lại trang trước
                ViewData["ErrorMessage"] = "Bạn đã đăng ký thẻ thành viên trước đó.";
                return View();
            }

            return View();
        }

        // POST: MemberSubscription_64130299/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SubscriptionId,UserId,PlanId,PaymentMethod")] MemberSubscription_64130299 memberSubscription_64130299)
        {
            if (ModelState.IsValid)
            {
                // Tạo SubscriptionId tự động
                memberSubscription_64130299.SubscriptionId = GenerateUniqueSubscriptionId();

                // Mặc định Access Level là "Premium"
                memberSubscription_64130299.AccessLevel = "Premium";

                // Đặt giá trị mặc định cho Status nếu chưa được gán
                if (string.IsNullOrEmpty(memberSubscription_64130299.Status))
                {
                    memberSubscription_64130299.Status = "Kích hoạt";
                }

                // Kiểm tra xem người dùng đã đăng ký thẻ thành viên chưa
                var existingSubscription = db.MemberSubscription_64130299
                                              .FirstOrDefault(m => m.UserId == memberSubscription_64130299.UserId);

                if (existingSubscription != null)
                {
                    // Nếu đã đăng ký, thêm thông báo lỗi vào ViewData và không tiếp tục xử lý
                    ViewData["ErrorMessage"] = "Bạn đã đăng ký thẻ thành viên trước đó.";
                    return View(memberSubscription_64130299);
                }

                // Lấy thông tin gói thành viên từ cơ sở dữ liệu
                var plan = db.SubscriptionPlans_64130299.Find(memberSubscription_64130299.PlanId);
                if (plan != null)
                {
                    // Tính toán Amount Paid dựa trên Payment Method
                    if (memberSubscription_64130299.PaymentMethod == "VNPay")
                    {
                        memberSubscription_64130299.AmountPaid = plan.Price;
                    }
                    else if (memberSubscription_64130299.PaymentMethod == "Momo")
                    {
                        memberSubscription_64130299.AmountPaid = plan.Price * (decimal)0.95;
                    }
                }

                // Gán ngày bắt đầu và ngày hết hạn
                memberSubscription_64130299.StartDate = DateTime.Now;
                memberSubscription_64130299.ExpiryDate = memberSubscription_64130299.StartDate.AddMonths(plan.DurationMonths);

                // Lưu tạm thông tin vào TempData để truyền sang Details
                TempData["MemberSubscription"] = memberSubscription_64130299;

                return RedirectToAction("Details");
            }

           
            return View(memberSubscription_64130299);
        }

        [HttpPost]
        public ActionResult ConfirmSubscription(MemberSubscription_64130299 memberSubscription_64130299)
        {
            if (ModelState.IsValid)
            {
                // Gán các giá trị cần thiết
                memberSubscription_64130299.CreatedAt = DateTime.Now;
                memberSubscription_64130299.UpdatedAt = DateTime.Now;

                // Thêm vào cơ sở dữ liệu
                db.MemberSubscription_64130299.Add(memberSubscription_64130299);
                db.SaveChanges();

                return RedirectToAction("Success"); // Trở về danh sách hoặc trang thành công
            }

            // Nếu có lỗi, trở về trang Details
            return View("Details", memberSubscription_64130299);
        }

        // GET: MemberSubscription_64130299/Success
        public ActionResult Success()
        {
            return View();
        }

        // GET: MemberSubscription_64130299/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemberSubscription_64130299 memberSubscription_64130299 = db.MemberSubscription_64130299.Find(id);
            if (memberSubscription_64130299 == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.User_64130299, "UserId", "Email", memberSubscription_64130299.UserId);
    
            return View(memberSubscription_64130299);
        }

        // POST: MemberSubscription_64130299/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SubscriptionId,UserId,StartDate,ExpiryDate,AccessLevel,Status,RenewalDate,CreatedAt,UpdatedAt,PlanId,PaymentMethod,AmountPaid")] MemberSubscription_64130299 memberSubscription_64130299)
        {
            if (ModelState.IsValid)
            {
                db.Entry(memberSubscription_64130299).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.User_64130299, "UserId", "Email", memberSubscription_64130299.UserId);
            ViewBag.PlanId = new SelectList(db.SubscriptionPlans_64130299, "PlanId", "PlanName", memberSubscription_64130299.PlanId);
            return View(memberSubscription_64130299);
        }

        // GET: MemberSubscription_64130299/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemberSubscription_64130299 memberSubscription_64130299 = db.MemberSubscription_64130299.Find(id);
            if (memberSubscription_64130299 == null)
            {
                return HttpNotFound();
            }
            return View(memberSubscription_64130299);
        }

        // POST: MemberSubscription_64130299/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            MemberSubscription_64130299 memberSubscription_64130299 = db.MemberSubscription_64130299.Find(id);
            db.MemberSubscription_64130299.Remove(memberSubscription_64130299);
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
