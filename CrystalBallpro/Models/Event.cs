using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrystalBallpro.Models
{
    public class Event
    {
        [Key]
        public int Id { get; set; }
        public string EventName { get; set; }
        public string Location { get; set; }
        public DateTime EventDate { get; set; }
    }
}