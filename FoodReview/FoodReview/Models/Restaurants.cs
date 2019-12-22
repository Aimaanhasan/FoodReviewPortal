using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace FoodReview.Models
{
    public class Restaurants
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public bool isApproved { get; set; }
        
      //  public byte[] Image { get; set; }
        public virtual ICollection<Ratings> Ratings { get; set; }
        public virtual ICollection<File> Files { get; set; }
    }
}