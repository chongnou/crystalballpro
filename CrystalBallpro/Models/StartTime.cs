using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CrystalBallpro.Models
{
    public class StartTime
    {
        [Key]
        public int ID { get; set; }

        [Display(Name = "Start Time")]
        public string Start { get; set; }

    }
}