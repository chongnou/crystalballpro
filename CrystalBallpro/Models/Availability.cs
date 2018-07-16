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
        public int AdminID { get; set; }
        public Admin Admin { get; set; }

        [ForeignKey("Employee")]
        [Display(Name = "Employee")]
        public int EmployeeID { get; set; }
        public Employee Employee { get; set; }

        [ForeignKey("Week")]
        [Display(Name = "Day")]
        public int DayID { get; set; }
        public Week Week { get; set; }
    }
}