using MovieWeb.Models;
using System;
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
            string userId = Session["UserId"]?.ToString();
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login_64130299", "User_64130299");
            }

            ViewBag.UserId = userId;

            // Mặc định chọn gói 1 tháng nếu chưa có gói nào
            var selectedPlan = Request["PlanId"] ?? "1";
            DateTime calculatedExpiryDate = DateTime.Now.AddMonths(selectedPlan == "2" ? 12 : 1);

            ViewBag.SelectedPlan = selectedPlan;
            ViewBag.ExpiryDate = calculatedExpiryDate;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MemberSubscription_64130299 model)
        {
            string userId = Session["UserId"]?.ToString();
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login_64130299", "User_64130299");
            }

            if (ModelState.IsValid)
            {
                DateTime expiryDate = model.PlanId == 2 ? DateTime.Now.AddMonths(12) : DateTime.Now.AddMonths(1);

                var subscription = new MemberSubscription_64130299
                {
                    SubscriptionId = Guid.NewGuid().ToString(),
                    UserId = userId,
                    PlanId = model.PlanId,
                    StartDate = DateTime.Now,
                    ExpiryDate = expiryDate,
                    Status = "Active",
                    PaymentMethod = model.PaymentMethod,
                };

                db.MemberSubscription_64130299.Add(subscription);
                db.SaveChanges();

                return model.PaymentMethod == "VNPay"
                    ? RedirectToAction("VNPayPayment", new { subscriptionId = subscription.SubscriptionId })
                    : RedirectToAction("MoMoPayment", new { subscriptionId = subscription.SubscriptionId });
            }

            return View(model);
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
