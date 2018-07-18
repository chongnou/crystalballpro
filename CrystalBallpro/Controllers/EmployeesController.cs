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
    public class EmployeesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Employees
        public ActionResult Index()
        {
            return View(db.Employees.ToList());
        }

        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {
            var currentUserId = User.Identity.GetUserId();

            if (currentUserId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Where(e => e.ApplicationUserID == currentUserId).FirstOrDefault();
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Email,ApplicationUserId")] Employee employee)
        {
            var currentUserId = User.Identity.GetUserId();
            employee.ApplicationUserID = currentUserId;

            if (ModelState.IsValid)
            {
                db.Employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            return View(employee);
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Email,ApplicationUserId")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details");
            }
            return View(employee);
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
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

        public ActionResult AvailabilityIndex()
        {
            var currentUserId = User.Identity.GetUserId();
            var employee = db.Employees.Where(e => e.ApplicationUserID == currentUserId).FirstOrDefault();
            var availabilities = db.Availabilities.Include(a => a.Admin).Include(a => a.Employee).Include(a => a.Week).Include(a => a.StartTime).Include(a => a.EndTime).Where(a => a.EmployeeID == employee.Id).ToList();
            return View(availabilities);
        }

        [HttpGet]
        public ActionResult AvailabilityCreate()
        {
            ViewBag.DayID = new SelectList(db.Weeks, "ID", "Day");
            ViewBag.StartTimeID = new SelectList(db.StartTimes, "ID", "Start");
            ViewBag.EndTimeID = new SelectList(db.EndTimes, "ID", "End");
            
            return View();
        }

        [HttpPost, ActionName("AvailabilityCreate")]
        [ValidateAntiForgeryToken]
        public ActionResult AvailabilityCreate([Bind(Include = "ID,AdminID,EmployeeID,DayID,StartTimeID,EndTimeID,WorkStatus")] Availability availability)
        {
            var currentUserId = User.Identity.GetUserId();
            var employee = db.Employees.Where(e => e.ApplicationUserID == currentUserId).FirstOrDefault();
            var day = db.Weeks.Where(d => d.ID == availability.DayID).FirstOrDefault();
            var startTime = db.StartTimes.Where(s => s.ID == availability.StartTimeID).FirstOrDefault();
            var endTime = db.EndTimes.Where(t => t.ID == availability.EndTimeID).FirstOrDefault();
            

            if (ModelState.IsValid || availability.ID == 0)
            {
                availability.AdminID = null;
                availability.EmployeeID = employee.Id;
                availability.DayID = day.ID;
                availability.StartTimeID = startTime.ID;
                availability.EndTimeID = endTime.ID;
                db.Availabilities.Add(availability);
                db.SaveChanges();
                return RedirectToAction("AvailabilityIndex", "Employees");
            }
            ViewBag.DayID = new SelectList(db.Weeks, "ID", "Day", availability.DayID);
            ViewBag.StartTimeID = new SelectList(db.StartTimes, "ID", "Start", availability.StartTimeID);
            ViewBag.EndTimeID = new SelectList(db.EndTimes, "ID", "End", availability.EndTimeID);

            return View(availability);
        }

        public ActionResult AvailabilityEdit(int? id)
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
        public ActionResult AvailabilityEdit([Bind(Include = "ID,AdminID,EmployeeID,DayID,StartTimeID,EndTimeID,WorkStatus")] Availability availability)
        {
            
            var employee = db.Employees.Where(e => e.Id == availability.EmployeeID).FirstOrDefault();
            var day = db.Weeks.Where(d => d.ID == availability.DayID).FirstOrDefault();
            var startTime = db.StartTimes.Where(s => s.ID == availability.StartTimeID).FirstOrDefault();
            var endTime = db.EndTimes.Where(t => t.ID == availability.EndTimeID).FirstOrDefault();


            if (ModelState.IsValid)
            {
                availability = db.Availabilities.Include(a => a.Admin).Include(a => a.Employee).Include(a => a.Week).Include(a => a.StartTime).Include(a => a.EndTime).Where(a => a.ID == availability.ID).FirstOrDefault();
                availability.EmployeeID = employee.Id;
                availability.DayID = day.ID;
                availability.StartTimeID = startTime.ID;
                availability.EndTimeID = endTime.ID;
                db.SaveChanges();
                return RedirectToAction("AvailabilityIndex", "Employees");
            }
            
            return View(availability);
        }

        public ActionResult AvailabilityDelete(int? id)
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

        [HttpPost, ActionName("AvailabilityDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult AvailabilityDeleteConfirmed(int id)
        {
            Availability availability = db.Availabilities.Include(a => a.Admin).Include(a => a.Employee).Include(a => a.Week).Include(a => a.StartTime).Include(a => a.EndTime).Where(a => a.ID == id).FirstOrDefault();
            db.Availabilities.Remove(availability);
            db.SaveChanges();
            return RedirectToAction("AvailabilityIndex", "Employees");
        }

        
    }
}
