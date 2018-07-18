using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CrystalBallpro.Models;
using Microsoft.AspNet.Identity;

namespace CrystalBallpro.Controllers
{
    public class AdminsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admins
        public ActionResult Index()
        {
            return View(db.Admins.ToList());
        }

        // GET: Admins/Details/5
        public ActionResult Details(int? id)
        {
            var currentUserId = User.Identity.GetUserId();
            if (currentUserId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin admin = db.Admins.Where(a => a.ApplicationUserID == currentUserId).FirstOrDefault();
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }

        // GET: Admins/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admins/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Email,ApplicationUserId")] Admin admin)
        {
            var currentUserId = User.Identity.GetUserId();
            admin.ApplicationUserID = currentUserId;
            if (ModelState.IsValid)
            {
                db.Admins.Add(admin);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            return View(admin);
        }

        // GET: Admins/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin admin = db.Admins.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }

        // POST: Admins/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Email,ApplicationUserID")] Admin admin)
        {
            if (ModelState.IsValid)
            {
                db.Entry(admin).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details");
            }
            return View(admin);
        }

        // GET: Admins/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin admin = db.Admins.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }

        // POST: Admins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Admin admin = db.Admins.Find(id);
            db.Admins.Remove(admin);
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

        public ActionResult AvailabilityAIndex()
        {
            var currentUserId = User.Identity.GetUserId();
            var admin = db.Admins.Where(a => a.ApplicationUserID == currentUserId).FirstOrDefault();
            var availabilities = db.Availabilities.Include(a => a.Admin).Include(a => a.Employee).Include(a => a.Week).Include(a => a.StartTime).Include(a => a.EndTime).Where(a => a.AdminID == admin.ID).ToList();
            return View(availabilities);
        }

        [HttpGet]
        public ActionResult AvailabilityACreate()
        {
            ViewBag.DayID = new SelectList(db.Weeks, "ID", "Day");
            ViewBag.StartTimeID = new SelectList(db.StartTimes, "ID", "Start");
            ViewBag.EndTimeID = new SelectList(db.EndTimes, "ID", "End");

            return View();
        }

        [HttpPost, ActionName("AvailabilityACreate")]
        [ValidateAntiForgeryToken]
        public ActionResult AvailabilityACreate([Bind(Include = "ID,AdminID,EmployeeID,DayID,StartTimeID,EndTimeID,WorkStatus")] Availability availability)
        {
            var currentUserId = User.Identity.GetUserId();
            var admin = db.Admins.Where(a => a.ApplicationUserID == currentUserId).FirstOrDefault();
            var day = db.Weeks.Where(d => d.ID == availability.DayID).FirstOrDefault();
            var startTime = db.StartTimes.Where(s => s.ID == availability.StartTimeID).FirstOrDefault();
            var endTime = db.EndTimes.Where(t => t.ID == availability.EndTimeID).FirstOrDefault();


            if (ModelState.IsValid || availability.ID == 0)
            {
                availability.AdminID = admin.ID;
                availability.EmployeeID = null;
                availability.DayID = day.ID;
                availability.StartTimeID = startTime.ID;
                availability.EndTimeID = endTime.ID;
                db.Availabilities.Add(availability);
                db.SaveChanges();
                return RedirectToAction("AvailabilityAIndex", "Admins");
            }
            ViewBag.DayID = new SelectList(db.Weeks, "ID", "Day", availability.DayID);
            ViewBag.StartTimeID = new SelectList(db.StartTimes, "ID", "Start", availability.StartTimeID);
            ViewBag.EndTimeID = new SelectList(db.EndTimes, "ID", "End", availability.EndTimeID);

            return View(availability);
        }

        public ActionResult AvailabilityAEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Availability availability = db.Availabilities.Include(a => a.Admin).Include(a => a.Employee).Include(a => a.Week).Include(a => a.StartTime).Include(a => a.EndTime).Where(a => a.ID == id).FirstOrDefault();

            if (availability == null)
            {
                return HttpNotFound();
            }

            ViewBag.DayID = new SelectList(db.Weeks, "ID", "Day", availability.DayID);
            ViewBag.StartTimeID = new SelectList(db.StartTimes, "ID", "Start", availability.StartTimeID);
            ViewBag.EndTimeID = new SelectList(db.EndTimes, "ID", "End", availability.EndTimeID);

            return View(availability);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AvailabilityAEdit([Bind(Include = "ID,AdminID,EmployeeID,DayID,StartTimeID,EndTimeID,WorkStatus")] Availability availability)
        {
            var admin = db.Admins.Where(a => a.ID == availability.AdminID).FirstOrDefault();
            var day = db.Weeks.Where(d => d.ID == availability.DayID).FirstOrDefault();
            var startTime = db.StartTimes.Where(s => s.ID == availability.StartTimeID).FirstOrDefault();
            var endTime = db.EndTimes.Where(t => t.ID == availability.EndTimeID).FirstOrDefault();


            if (ModelState.IsValid)
            {
                availability = db.Availabilities.Include(a => a.Admin).Include(a => a.Employee).Include(a => a.Week).Include(a => a.StartTime).Include(a => a.EndTime).Where(a => a.ID == availability.ID).FirstOrDefault();
                availability.AdminID = admin.ID;
                availability.DayID = day.ID;
                availability.StartTimeID = startTime.ID;
                availability.EndTimeID = endTime.ID;
                db.SaveChanges();
                return RedirectToAction("AvailabilityAIndex", "Admins");
            }

            return View(availability);
        }

        public ActionResult AvailabilityADelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Availability availability = db.Availabilities.Include(a => a.Admin).Include(a => a.Employee).Include(a => a.Week).Include(a => a.StartTime).Include(a => a.EndTime).Where(a => a.ID == id).FirstOrDefault();
            if (availability == null)
            {
                return HttpNotFound();
            }
            ViewBag.DayID = new SelectList(db.Weeks, "ID", "Day", availability.DayID);
            ViewBag.StartTimeID = new SelectList(db.StartTimes, "ID", "Start", availability.StartTimeID);
            ViewBag.EndTimeID = new SelectList(db.EndTimes, "ID", "End", availability.EndTimeID);

            return View(availability);
        }

        [HttpPost, ActionName("AvailabilityADelete")]
        [ValidateAntiForgeryToken]
        public ActionResult AvailabilityADeleteConfirmed(int id)
        {
            Availability availability = db.Availabilities.Include(a => a.Admin).Include(a => a.Employee).Include(a => a.Week).Include(a => a.StartTime).Include(a => a.EndTime).Where(a => a.ID == id).FirstOrDefault();
            db.Availabilities.Remove(availability);
            db.SaveChanges();
            return RedirectToAction("AvailabilityAIndex", "Admins");
        }

        public ActionResult ScheduleIndex()
        {
            var availabilities = db.Availabilities.Include(a => a.Admin).Include(a => a.Employee).Include(a => a.Week).Include(a => a.StartTime).Include(a => a.EndTime).OrderBy(a => a.DayID).ToList();
            return View(availabilities);
        }
    }
}
