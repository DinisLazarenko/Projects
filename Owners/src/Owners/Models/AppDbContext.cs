using Microsoft.EntityFrameworkCore;

namespace Owners.Models{
    public class AppDbContext : DbContext {
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Pet> Pets { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){
            optionsBuilder.UseSqlite("Filename=./Owners.db");
        }
    }
}