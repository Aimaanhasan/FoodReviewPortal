﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEBDEVPROJECT.Models
{
    public class Ratings
    {
        public int RatingID { get; set; }
        public int RestaurantID { get; set; }
        public int UserID { get; set; }
        public string comments { get; set; }
        public int rate { get; set; }

        public virtual Restaurants restaurants { get; set; }
        public virtual ApplicationUser users { get; set; }
    }
}