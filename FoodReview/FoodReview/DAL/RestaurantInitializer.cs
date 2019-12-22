
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FoodReview.Models;

namespace FoodReview.DAL
{
    public class RestaurantInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            var students = new List<Restaurants>
            {
            new Restaurants{Name="Carson",Type="Alexander", isApproved = true},
            new Restaurants{Name = "dasda", Type = "dsadsasa", isApproved = false}
            
            };

            students.ForEach(s => context.Restaurants.Add(s));
            context.SaveChanges();
            
        }
    }
}