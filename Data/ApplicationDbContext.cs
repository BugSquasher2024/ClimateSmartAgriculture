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
    }
}