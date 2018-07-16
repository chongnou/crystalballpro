using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CrystalBallpro.Models
{
    public class Week
    {
        [Key]
        public int ID { get; set; }

        [Display(Name = "Day")]
        public string Day { get; set; }
    }
}