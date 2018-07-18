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
        public int NumberOfEvents { get; set; }
        public int NumberOfEmployees { get; set; }
        public double SalesIncreasePercent { get; set; }
    }
}