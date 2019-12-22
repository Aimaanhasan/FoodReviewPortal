using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using WEBDEVPROJECT.Models;

namespace WEBDEVPROJECT.DAL
{
    public class SchoolInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<Restcontext>
    {
        protected override void Seed(Restcontext context)
        {
            var restaurants = new List<Restaurants>
            {
            new Restaurants{RestName = "Le Petit Souffle", ResType = "French", ResLocation =  "Karachi"},
            new Restaurants{ RestName = " Izakaya Kikufuji ", ResType = " Japanese ", ResLocation =  "Karachi"},
            new Restaurants{ RestName = " Heat - Edsa Shangri-La ", ResType = " Seafood ", ResLocation =  "Karachi"},
            new Restaurants{ RestName = " Ooma ", ResType = " Japanese ", ResLocation =  "Karachi"},
            new Restaurants{ RestName = " Sambo Kojin ", ResType = " Japanese ", ResLocation =  "Karachi"},
            new Restaurants{ RestName = " Din Tai Fung ", ResType = " Chinese ", ResLocation =  "Karachi"},
            new Restaurants{ RestName = " Buffet 101 ", ResType = " Asian ", ResLocation =  "Karachi"},
            new Restaurants{ RestName = " Vikings ", ResType = " Seafood ", ResLocation =  "Karachi"},
            new Restaurants{ RestName = " Spiral - Sofitel Philippine Plaza Manila ", ResType = " European ", ResLocation =  "Karachi"},
            new Restaurants{ RestName = " Locavore ", ResType = " Filipino ", ResLocation =  "Karachi"},
            new Restaurants{ RestName = " Silantro Fil-Mex ", ResType = " Filipino ", ResLocation =  "Karachi"},
            new Restaurants{ RestName = " Mad Mark's Creamery & Good Eats ", ResType = " American ", ResLocation =  "Karachi"},
            new Restaurants{ RestName = " Silantro Fil-Mex ", ResType = " Filipino ", ResLocation =  "Karachi"},
            new Restaurants{ RestName = " Guevarra's ", ResType = " Filipino ", ResLocation =  "Karachi"},
            new Restaurants{ RestName = " Sodam Korean Restaurant ", ResType = " Korean ", ResLocation =  "Karachi"},
            new Restaurants{ RestName = " Cafe Arabelle ", ResType = " Cafe ", ResLocation =  "Karachi"},
            new Restaurants{ RestName = " Nonna's Pasta & Pizzeria ", ResType = " Italian ", ResLocation =  "Karachi"},
            new Restaurants{ RestName = " Balay Dako ", ResType = " Filipino ", ResLocation =  "Karachi"},
            new Restaurants{ RestName = " Hobing Korean Dessert Cafe ", ResType = " Cafe ", ResLocation =  "Karachi"},
            new Restaurants{ RestName = " Wildflour Cafe + Bakery ", ResType = " Cafe ", ResLocation =  "Karachi"}
            };

            restaurants.ForEach(s => context.Restaurant.Add(s));
            context.SaveChanges();
            
        }
    }
}