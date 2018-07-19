using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace CrystalBallpro.Models
{
    public class Inventory
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Stock { get; set; }
        public string Units { get; set; }
        public string Expiration { get; set; }
        [Display(Name = "Last Ordered")]
        public string LastOrdered { get; set; }
    }
}