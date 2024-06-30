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
    }
}
