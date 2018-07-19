using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CrystalBallpro.Models
{
    public class SalesHistorical
    {
        [Key]
        public int Id { get; set; }
        [Display(Name ="Number of Events")]
        public int NumberOfEvents { get; set; }
        [Display(Name = "Number of Employees")]
        public int NumberOfEmployees { get; set; }
        [Display(Name = "Sales Increase (%)")]
        public double SalesIncreasePercent { get; set; }
    }
}