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
        public DbSet<CartItem> CartItems { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<RolModel>(tb =>
            {
                tb.ToTable("rol");
                tb.HasKey(col => col.Id);
                tb.Property(col => col.Id).ValueGeneratedOnAdd();
                tb.Property(col => col.Name).IsRequired().HasMaxLength(50);
            });
            modelBuilder.Entity<RolModel>().HasData(
                new RolModel { Id = Guid.Parse("00000000-0000-0000-0000-000000000001"), Name = "client" },
                new RolModel { Id = Guid.Parse("00000000-0000-0000-0000-000000000002"), Name = "admin" },
                new RolModel { Id = Guid.Parse("00000000-0000-0000-0000-000000000003"), Name = "local"}
            );

            modelBuilder.Entity<UserModel>(tb =>
            {
                tb.ToTable("user");
                tb.HasKey(col => col.Id);
                tb.Property(col => col.Id).ValueGeneratedOnAdd();
                tb.Property(col => col.IdRol).IsRequired();
                tb.Property(col => col.Name).IsRequired().HasMaxLength(100);
                tb.Property(col => col.Email).IsRequired().HasMaxLength(100);
                tb.Property(col => col.Password).IsRequired().HasMaxLength(100);
                tb.Property(col => col.Phone).IsRequired().HasMaxLength(50);
                tb.Property(col => col.BirthDate);

                tb.HasOne(col => col.Rol)
                .WithOne(rol => rol.User)
                .HasForeignKey<UserModel>(col => col.IdRol)
                .OnDelete(DeleteBehavior.Restrict);
            });
            modelBuilder.Entity<CategoryModel>(tb =>
            {
                tb.ToTable("category");
                tb.HasKey(col => col.Id);
                tb.Property(col => col.Id).ValueGeneratedOnAdd();
                tb.Property(col => col.Name).IsRequired().HasMaxLength(100);
                tb.Property(col => col.Type).IsRequired().HasMaxLength(50);
                tb.Property(col => col.Visibility).IsRequired();
            });
            modelBuilder.Entity<FoodModel>(tb =>
            {
                tb.ToTable("food");
                tb.HasKey(col => col.Id);
                tb.Property(col => col.Id).ValueGeneratedOnAdd();
                tb.Property(col => col.IdCategory).IsRequired();
                tb.Property(col => col.Name).IsRequired().HasMaxLength(200);
                tb.Property(col => col.Description).IsRequired().HasMaxLength(400);
                tb.Property(col => col.ImgUrl);
                tb.Property(col => col.Price).IsRequired().HasColumnType("decimal(18,2)");
                tb.Property(col => col.Discount);
                tb.Property(col => col.Visibility).IsRequired();

                tb.HasOne(col => col.Category)
                .WithMany(cat => cat.Foods)
                .HasForeignKey(col => col.IdCategory)
                .OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<CartItem>(tb =>
            {
                tb.ToTable("cartItem");
                tb.HasKey(col => col.Id);
                tb.Property(col => col.Id).ValueGeneratedOnAdd();
                tb.Property(col => col.IdFood);
                tb.Property(col => col.IdUser).IsRequired();
                tb.Property(col => col.Quantity).IsRequired();
                tb.Property(col => col.PriceTotal).IsRequired();

                tb.HasOne(col => col.Food)
                .WithMany(food => food.Orders)
                .HasForeignKey(col => col.IdFood)
                .OnDelete(DeleteBehavior.Cascade);

                tb.HasOne(col => col.User)
                .WithMany(user => user.Orders)
                .HasForeignKey(col => col.IdUser)
                .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
