using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CrystalBallpro.Models
{
    public class EndTime
    {
        [Key]
        public int ID { get; set; }

        public string End { get; set; }

    }
}