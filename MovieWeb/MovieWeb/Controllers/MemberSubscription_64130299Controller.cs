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

        // GET: MemberSubscription_64130299
        public ActionResult Index()
        {
            var memberSubscription_64130299 = db.MemberSubscription_64130299.Include(m => m.User_64130299).Include(m => m.SubscriptionPlans_64130299);
            return View(memberSubscription_64130299.ToList());
        }

        // GET: MemberSubscription_64130299/Details/5
        public ActionResult Details(string id)
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

        // GET: MemberSubscription_64130299/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.User_64130299, "UserId", "Email");
            ViewBag.PlanId = new SelectList(db.SubscriptionPlans_64130299, "PlanId", "PlanName");
            return View();
        }

        // POST: MemberSubscription_64130299/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SubscriptionId,UserId,StartDate,ExpiryDate,AccessLevel,Status,RenewalDate,CreatedAt,UpdatedAt,PlanId,PaymentMethod,AmountPaid")] MemberSubscription_64130299 memberSubscription_64130299)
        {
            if (ModelState.IsValid)
            {
                db.MemberSubscription_64130299.Add(memberSubscription_64130299);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.User_64130299, "UserId", "Email", memberSubscription_64130299.UserId);
            ViewBag.PlanId = new SelectList(db.SubscriptionPlans_64130299, "PlanId", "PlanName", memberSubscription_64130299.PlanId);
            return View(memberSubscription_64130299);
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
            ViewBag.PlanId = new SelectList(db.SubscriptionPlans_64130299, "PlanId", "PlanName", memberSubscription_64130299.PlanId);
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
