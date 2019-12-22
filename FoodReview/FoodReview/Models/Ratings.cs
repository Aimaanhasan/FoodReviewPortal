using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodReview.Models
{
    public class Ratings
    {
        public int RatingsID { get; set; }
        
        public int RestaurantID { get; set; }
        
        public string ApplicationUserID { get; set; }

        public int RatingNum { get; set; }

        public string Comment { get; set; }

        public virtual Restaurants Restaurant { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
       
    }
}