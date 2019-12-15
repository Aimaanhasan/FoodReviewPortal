using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEBDEVPROJECT.Models
{
    public class Restaurants
    {
        
        public int RestaurntID { get; set; }
        public string RestName { get; set; }
        public string ResType { get; set; }
        public string ResLocation { get; set; }

        public virtual ICollection<Ratings> ratings { get; set; }
    }
}


