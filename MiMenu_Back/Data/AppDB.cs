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
        public DbSet<VoucherModel> Vouchers { get; set; }
        public DbSet<ItemVoucherModel> ItemsVoucher { get; set; }
        public DbSet<BannerModel> Banners { get; set; }
        public DbSet<PaymentModel> Payments { get; set; }
        public DbSet<OrderModel> Orders { get; set; }
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
                .WithMany(food => food.CartItems)
                .HasForeignKey(col => col.IdFood)
                .OnDelete(DeleteBehavior.Cascade);

                tb.HasOne(col => col.User)
                .WithMany(user => user.CartItems)
                .HasForeignKey(col => col.IdUser)
                .OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<VoucherModel>(tb =>
            {
                tb.ToTable("voucher");
                tb.HasKey(col => col.Id);
                tb.Property(col => col.Id).ValueGeneratedOnAdd();
                tb.Property(col => col.Name).IsRequired().HasMaxLength(100);
                tb.Property(col => col.Type).IsRequired().HasMaxLength(50);
                tb.Property(col => col.Discount).IsRequired();
                tb.Property(col => col.BuyMinimum).IsRequired();
                tb.Property(col => col.Visibility).IsRequired();
                tb.Property(col => col.DueDate).IsRequired();
                tb.Property(col => col.CreateDate).IsRequired();
            });
            modelBuilder.Entity<ItemVoucherModel>(tb =>
            {
                tb.ToTable("itemVoucher");
                tb.HasKey(col => col.Id);
                tb.Property(col => col.Id).ValueGeneratedOnAdd();
                tb.Property(col => col.IdUser).IsRequired();
                tb.Property(col => col.IdVoucher).IsRequired();

                tb.HasOne(col => col.User)
                .WithMany(user => user.ItemsVoucher)
                .HasForeignKey(col => col.IdUser)
                .OnDelete(DeleteBehavior.Cascade);

                tb.HasOne(col => col.Voucher)
                .WithMany(vou => vou.ItemsVoucher)
                .HasForeignKey(col => col.IdVoucher)
                .OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<BannerModel>(tb =>
            {
                tb.ToTable("banner");
                tb.HasKey(col => col.Id);
                tb.Property(col => col.Id).ValueGeneratedOnAdd();
                tb.Property(col => col.Description).IsRequired().HasMaxLength(250);
                tb.Property(col => col.Priority).IsRequired();
                tb.Property(col => col.ImgUrl);
                tb.Property(col => col.Visibility).IsRequired();
            });
            modelBuilder.Entity<PaymentModel>(tb =>
            {
                tb.ToTable("payment");
                tb.HasKey(col => col.Id);
                tb.Property(col => col.Id).ValueGeneratedOnAdd();
                tb.Property(col => col.Status).IsRequired();
                tb.Property(col => col.PaymentMethod);
                tb.Property(col => col.Currency).IsRequired().HasMaxLength(50);
                tb.Property(col => col.Total).IsRequired().HasColumnType("decimal(18,2)");
                tb.Property(col => col.IdPublic).IsRequired();
                tb.Property(col => col.CreateDate).IsRequired();
                tb.Property(col => col.ApprovedDate);
            });
            modelBuilder.Entity<OrderModel>(tb =>
            {
                tb.ToTable("order");
                tb.HasKey(col => col.Id);
                tb.Property(col => col.Id).ValueGeneratedOnAdd();
                tb.Property(col => col.IdUser).IsRequired();
                tb.Property(col => col.IdPayment).IsRequired();
                tb.Property(col => col.Type).IsRequired();
                tb.Property(col => col.Status).IsRequired();
                tb.Property(col => col.RetirementTime).IsRequired();
                tb.Property(col => col.RetirementInstruction).IsRequired();
                tb.Property(col => col.IdPublic).IsRequired();
                tb.Property(col => col.CreateDate).IsRequired();

                tb.HasOne(col => col.User)
                .WithMany(user => user.Orders)
                .HasForeignKey(col => col.IdUser)
                .OnDelete(DeleteBehavior.Cascade);
               
                tb.HasOne(col => col.Payment)
                .WithOne(pay => pay.Order)
                .HasForeignKey<OrderModel>(col => col.IdPayment)
                .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
