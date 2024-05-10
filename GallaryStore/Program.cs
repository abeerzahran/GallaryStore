
using GallaryStore.dbContext;
using GallaryStore.Models;
using GallaryStore.Repositories;
using GallaryStore.Services;
using GallaryStore.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace GallaryStore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

            // Add services to the container.

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                builder =>
                {
                    builder.AllowAnyOrigin();
                    builder.AllowAnyMethod();
                    builder.AllowAnyHeader();
                });
            });

            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<GallaryContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("ConString"))
            );

            builder.Services.AddScoped<unitOfWork<Product>, unitOfWork<Product>>();
            builder.Services.AddScoped<unitOfWork<Category>, unitOfWork<Category>>();
            builder.Services.AddScoped<unitOfWork<Order>, unitOfWork<Order>>();
            builder.Services.AddScoped<unitOfWork<Favourite>, unitOfWork<Favourite>>();
            builder.Services.AddScoped<unitOfWork<OrderProducts>, unitOfWork<OrderProducts>>();


            builder.Services.AddScoped<IRepository<Product>,GenericRepository<Product>>();
            builder.Services.AddScoped<IRepository<Category>, GenericRepository<Category>>();
            builder.Services.AddScoped<IRepository<Order>, GenericRepository<Order>>();
            builder.Services.AddScoped<IRepository<Favourite>, GenericRepository<Favourite>>();
            builder.Services.AddScoped<IRepository<OrderProducts>, GenericRepository<OrderProducts>>();


            builder.Services.AddScoped<ProductService, ProductService>();
            builder.Services.AddScoped<CategoryService, CategoryService>();
            builder.Services.AddScoped<OrderService, OrderService>();
            builder.Services.AddScoped<FavouriteService, FavouriteService>();
            builder.Services.AddScoped<OrderProductsService, OrderProductsService>();

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<GallaryContext>();

            builder.Services.AddAuthentication(options =>
                options.DefaultAuthenticateScheme = "myschema")
                .AddJwtBearer("myschema",
                    option =>
                    {
                        string key = "welcome to my secret world abeer adel zahran";
                        var secretkey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));
                        option.TokenValidationParameters = new TokenValidationParameters()
                        {
                            IssuerSigningKey = secretkey,
                            ValidateIssuer = false,
                            ValidateAudience = false,
                        };
                    }
                    
                );

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            
            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseCors(MyAllowSpecificOrigins);


            app.MapControllers();

            app.Run();
        }
    }
}
