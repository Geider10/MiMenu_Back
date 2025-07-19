using Microsoft.EntityFrameworkCore;
using MiMenu_Back.Data.Models;

namespace MiMenu_Back.Data
{
    public class AppDB : DbContext
    {
        public AppDB(DbContextOptions<AppDB> options) : base(options) { }

        public DbSet<UserModel> Users { get; set; }
        public DbSet<CategoryModel> Categories { get; set; }
        public DbSet<FoodModel> Foods { get; set; }

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
            modelBuilder.Entity<CategoryModel>(tb =>
            {
                tb.ToTable("category");
                tb.HasKey(col => col.Id);
                tb.Property(col => col.Id).ValueGeneratedOnAdd();
                tb.Property(col => col.Name).IsRequired().HasMaxLength(100);
                tb.Property(col => col.Type).IsRequired().HasMaxLength(50);
            });
            modelBuilder.Entity<FoodModel>(tb =>
            {
                tb.ToTable("food");
                tb.HasKey(col => col.Id);
                tb.Property(col => col.Id).ValueGeneratedOnAdd();
                tb.Property(col => col.IdCategory).IsRequired();
                tb.Property(col => col.Name).IsRequired().HasMaxLength(200);
                tb.Property(col => col.Description).IsRequired().HasMaxLength(400);
                tb.Property(col => col.ImgUrl).IsRequired().HasMaxLength(400);
                tb.Property(col => col.Price).IsRequired().HasColumnType("decimal(18,2)");
                tb.Property(col => col.Discount);

                tb.HasOne(col => col.Category)
                .WithMany(cat => cat.Foods)
                .HasForeignKey(col => col.IdCategory);
            });
        }
    }
}
