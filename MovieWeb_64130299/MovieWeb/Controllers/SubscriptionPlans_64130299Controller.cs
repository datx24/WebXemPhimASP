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
    public class SubscriptionPlans_64130299Controller : Controller
    {
        private MovieDatabase_64130299Entities db = new MovieDatabase_64130299Entities();

        // GET: SubscriptionPlans_64130299
        public ActionResult Index()
        {
            return View(db.SubscriptionPlans_64130299.ToList());
        }

        // GET: SubscriptionPlans_64130299/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubscriptionPlans_64130299 subscriptionPlans_64130299 = db.SubscriptionPlans_64130299.Find(id);
            if (subscriptionPlans_64130299 == null)
            {
                return HttpNotFound();
            }
            return View(subscriptionPlans_64130299);
        }

        // GET: SubscriptionPlans_64130299/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SubscriptionPlans_64130299/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PlanId,PlanName,DurationMonths,Price,CreatedAt,UpdatedAt")] SubscriptionPlans_64130299 subscriptionPlans_64130299)
        {
            if (ModelState.IsValid)
            {
                db.SubscriptionPlans_64130299.Add(subscriptionPlans_64130299);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(subscriptionPlans_64130299);
        }

        // GET: SubscriptionPlans_64130299/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubscriptionPlans_64130299 subscriptionPlans_64130299 = db.SubscriptionPlans_64130299.Find(id);
            if (subscriptionPlans_64130299 == null)
            {
                return HttpNotFound();
            }
            return View(subscriptionPlans_64130299);
        }

        // POST: SubscriptionPlans_64130299/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PlanId,PlanName,DurationMonths,Price,CreatedAt,UpdatedAt")] SubscriptionPlans_64130299 subscriptionPlans_64130299)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subscriptionPlans_64130299).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(subscriptionPlans_64130299);
        }

        // GET: SubscriptionPlans_64130299/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubscriptionPlans_64130299 subscriptionPlans_64130299 = db.SubscriptionPlans_64130299.Find(id);
            if (subscriptionPlans_64130299 == null)
            {
                return HttpNotFound();
            }
            return View(subscriptionPlans_64130299);
        }

        // POST: SubscriptionPlans_64130299/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SubscriptionPlans_64130299 subscriptionPlans_64130299 = db.SubscriptionPlans_64130299.Find(id);
            db.SubscriptionPlans_64130299.Remove(subscriptionPlans_64130299);
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
