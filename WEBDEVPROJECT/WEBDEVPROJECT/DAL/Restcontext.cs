using WEBDEVPROJECT.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Microsoft.AspNet.Identity.EntityFramework;

namespace WEBDEVPROJECT.DAL
{
    public class Restcontext : DbContext
    {
        public Restcontext() : base("Restcontext")
        {
        }

        public DbSet<Restaurants> Restaurant{ get; set; }
        public DbSet<Ratings> Rating { get; set; }
        //public DbSet<ApplicationUser> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            //base.OnModelCreating(modelBuilder);
            //modelBuilder.Configurations.Add(new RestaurantsConfiguration());
            //modelBuilder.Configurations.Add(new OperationsToRolesConfiguration());
        }
    }
}