using CrystalBallpro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CrystalBallpro.Controllers
{
    public class SalesHistoricalController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Events
        public ActionResult Index()
        {
            return View(db.SalesHistoricals.ToList());
        }
    }
}