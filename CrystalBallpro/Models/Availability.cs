using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CrystalBallpro.Models
{
    public class Availability
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("Admin")]
        [Display(Name = "Admin")]
        public int? AdminID { get; set; }
        public Admin Admin { get; set; }

        [ForeignKey("Employee")]
        [Display(Name = "Employee")]
        public int? EmployeeID { get; set; }
        public Employee Employee { get; set; }

        [ForeignKey("Week")]
        [Display(Name = "Day")]
        public int DayID { get; set; }
        public Week Week { get; set; }

        public IEnumerable<Week> Weeks { get; set; }

        [ForeignKey("StartTime")]
        [Display(Name = "Start Time")]
        public int StartTimeID { get; set; }
        public StartTime StartTime { get; set; }

        public IEnumerable<StartTime> StartTimes { get; set; }

        [ForeignKey("EndTime")]
        [Display(Name = "End Time")]
        public int EndTimeID { get; set; }
        public EndTime EndTime { get; set; }

        public IEnumerable<EndTime> EndTimes { get; set; }


    }
}