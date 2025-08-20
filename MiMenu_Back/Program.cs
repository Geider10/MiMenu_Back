using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MiMenu_Back.Data;
using MiMenu_Back.Mappers;
using MiMenu_Back.Mappers.Interfaces;
using MiMenu_Back.Repositories;
using MiMenu_Back.Repositories.Interfaces;
using MiMenu_Back.Services;
using MiMenu_Back.Utils;
using System.Text;

//register services and setup app
var builder = WebApplication.CreateBuilder(args);

DotNetEnv.Env.Load();
var connectionString = System.Environment.GetEnvironmentVariable("MySQLConnection");
var secretKey = System.Environment.GetEnvironmentVariable("SecretKey");
builder.Services.AddDbContext<AppDB>(options => options.UseMySql(connectionString,ServerVersion.AutoDetect(connectionString)));
builder.Services.AddSingleton<Util>();
builder.Services.AddScoped<IAuthMapper, AuthMapper>();
builder.Services.AddScoped<IUserMapper, UserMapper>();
builder.Services.AddScoped<ICategoryMapper, CategoryMapper>();
builder.Services.AddScoped<IFoodMapper, FoodMapper>();
builder.Services.AddScoped<ICartItemMapper, CartItemMapper>();
builder.Services.AddScoped<IVoucherMapper, VoucherMapper>();
builder.Services.AddScoped<IBannerMapper, BannerMapper>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IFoodRepository, FoodRepository>();
builder.Services.AddScoped<ICartItemRepository, CartItemRepository>();
builder.Services.AddScoped<IVoucherRepository, VoucherRepository>();
builder.Services.AddScoped<IItemVoucherRepository, ItemVoucherRepository>();
builder.Services.AddScoped<IBannerRepository, BannerRepository>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<FoodService>();
builder.Services.AddScoped<CartItemService>();
builder.Services.AddScoped<VoucherService>();
builder.Services.AddScoped<ItemVoucherService>();
builder.Services.AddScoped<BannerService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddAuthentication(config =>
{
    config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(config =>
{
    config.RequireHttpsMetadata = false;
    config.SaveToken = true;
    config.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
    };
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("NewPolicy", app =>
    {
        app.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});
//setup middleware and routes
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("NewPolicy");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
