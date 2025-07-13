using Microsoft.EntityFrameworkCore;
using MiMenu_Back.Data.Models;

namespace MiMenu_Back.Data
{
    public class AppDB : DbContext
    {
        public AppDB(DbContextOptions<AppDB> options) : base(options) { }

        public DbSet<UserModel> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserModel>(user =>
            {
                user.ToTable("user");
                user.HasKey(u => u.Id);
                user.Property(u => u.Id).ValueGeneratedOnAdd();
                user.Property(u => u.Name).IsRequired().HasMaxLength(100);
                user.Property(u => u.Email).IsRequired().HasMaxLength(100);
                user.Property(u => u.Password).IsRequired().HasMaxLength(100);
                user.Property(u => u.Address).IsRequired().HasMaxLength(200);
                user.Property(u => u.Role).IsRequired().HasMaxLength(50);
                user.Property(u => u.BirthDate);
            });
        }
    }
}
