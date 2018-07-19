using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CrystalBallpro.Models
{
    public class Admin
    {
        [Key]
        public int ID { get; set; }
        [Display(Name ="Admin Name")]
        public string Name { get; set; }
        public string Email { get; set; }

        [ForeignKey("ApplicationUser")]
        public string ApplicationUserID { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}