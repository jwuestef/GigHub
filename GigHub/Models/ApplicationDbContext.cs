using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GigHub.Models
{
    // THIS WAS ORIGINALLY IN 'IdentityModels.cs' FILE - MOVED TO NEW FILE
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        public DbSet<Gig> Gigs { get; set; }  // Adding this means that we can add-migration, and EF will create a Gigs table
                                              // Since the Gig class contains a reference to the Genre class, EF will also be aware of (and create a table for) Genres
                                              // But if we want to be able to query the Genres table, we need to add a reference to it here too
        public DbSet<Genre> Genres { get; set; }


        public DbSet<Attendance> Attendances { get; set; }



        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }



        // override OnMode   (tab)

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Attendance>()
                .HasRequired(a => a.Gig)
                .WithMany()
                .WillCascadeOnDelete(false);



            base.OnModelCreating(modelBuilder);
        }
    }
}