////using Microsoft.EntityFrameworkCore;
////using ClimateSmartAgriculture.Models;

////namespace ClimateSmartAgriculture.Data
////{
////    public class ApplicationDbContext : DbContext
////    {
////        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
////        {
////        }

////        public DbSet<User> Users { get; set; }

////        public DbSet<Farm> Farms { get; set; }

////        public DbSet<Crop> Crops { get; set; }

////        public DbSet<SoilMoisture> SoilMoisture { get; set; }  // Include DbSet for SoilMoisture
////    }
////}

////The error indicates that the SoilMoisture class does not have a primary key defined. In Entity Framework, every entity type must have a primary key defined.

////Here is how you can define the primary key for the SoilMoisture class:

////Update the SoilMoisture Class:

////Ensure the SoilMoisture class has a primary key defined. Typically, you would use an Id property or another unique identifier.

////csharp
////Copy code
////using System;

////namespace ClimateSmartAgriculture.Models
////{
////    public class SoilMoisture
////    {
////        public int MoistureId { get; set; }  // Primary key
////        public int FarmId { get; set; }
////        public DateTime Date { get; set; }
////        public double Level { get; set; }

////        public Farm Farm { get; set; }  // Navigation property
////    }
////}

//using Microsoft.EntityFrameworkCore;
//using ClimateSmartAgriculture.Models;

//namespace ClimateSmartAgriculture.Data
//{
//    public class ApplicationDbContext : DbContext
//    {
//        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
//        {
//        }

//        public DbSet<User> Users { get; set; }
//        public DbSet<Farm> Farms { get; set; }
//        public DbSet<Crop> Crops { get; set; }
//        public DbSet<SoilMoisture> SoilMoisture { get; set; }  // Include DbSet for SoilMoisture
//    }
//}
////Create a Migration:

////If you have added the primary key to the SoilMoisture class, create a new migration and update the database schema.

////sh
////Copy code
////dotnet ef migrations add AddPrimaryKeyToSoilMoisture
////dotnet ef database update
///

using Microsoft.EntityFrameworkCore;
using ClimateSmartAgriculture.Models;

namespace ClimateSmartAgriculture.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Farm> Farms { get; set; }
        public DbSet<Crop> Crops { get; set; }
        public DbSet<SoilMoisture> SoilMoisture { get; set; }  // Include DbSet for SoilMoisture

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<SoilMoisture>()
                .HasKey(sm => sm.MoistureId);

            modelBuilder.Entity<SoilMoisture>()
                .HasOne(sm => sm.Farm)
                .WithMany(f => f.SoilMoistureReadings)
                .HasForeignKey(sm => sm.FarmId);
        }
    }
}
