using DriverExpansesTracker.Repository.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DriveTracker.DbContexts
{
    public class AppDbContext:IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {

        }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Journey> Journeys { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<PassengerRoute> PassengerRoutes { get; set; }
        public DbSet<ExpiredToken> ExpiredTokens { get; set; }
    }
}