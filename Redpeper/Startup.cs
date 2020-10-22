using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Redpeper.Data;
using Redpeper.Helper;
using Redpeper.Hubs;
using Redpeper.Model;
using Redpeper.Repositories;
using Redpeper.Repositories.Inventory;
using Redpeper.Repositories.InvoiceSupply;
using Redpeper.Repositories.Order;
using Redpeper.Repositories.Order.Combos;
using Redpeper.Repositories.Order.Dishes;
using Redpeper.Repositories.Orders;
using Redpeper.Repositories.Tables;
using Redpeper.Services.Expo;
using Redpeper.Services.Inventory;

namespace Redpeper
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DataContext>(x => x.UseNpgsql(Configuration.GetConnectionString("PostgresConnetion")));
            services.AddIdentity<User, IdentityRole>(cfg =>
            {
                cfg.User.RequireUniqueEmail = true;
                cfg.Password.RequireDigit = false;
                cfg.Password.RequiredUniqueChars = 0;
                cfg.Password.RequireLowercase = false;
                cfg.Password.RequireNonAlphanumeric = false;
                cfg.Password.RequireUppercase = false;
            }).AddEntityFrameworkStores<DataContext>();
            services.AddAuthentication()
                .AddCookie()
                .AddJwtBearer(cfg =>
                {
                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = Configuration["Tokens:Issuer"],
                        ValidAudience = Configuration["Tokens:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Tokens:Key"]))
                    };
                });
            services.AddTransient<SeedDb>();
            services.AddCors(options =>
            {
                options.AddPolicy("ReactClient", policy =>
                {
                    policy.AllowAnyHeader()
                        .AllowAnyMethod()
                        .WithOrigins("http://localhost:3000")
                        .AllowCredentials();
                });
            });
            services.AddCors(options =>
            {
                options.AddPolicy("MobileClient", policy =>
                {
                    policy.AllowAnyHeader()
                        .AllowAnyMethod()
                        .WithOrigins("http://localhost:19002")
                        .AllowCredentials();
                });
            });

            services.AddCors(options =>
            {
                options.AddDefaultPolicy( policy =>
                {
                    policy.AllowAnyHeader().AllowAnyOrigin()
                        .AllowAnyMethod()
                        .WithOrigins("https://red-pepper.netlify.app")
                        .AllowCredentials();
                });
            });

            services.AddMvc();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IProviderRepository, ProviderRepository>();
            services.AddScoped<ISupplyRepository, SupplyRepository>();
            services.AddScoped<ISupplyInvoiceRepository, SupplyInvoiceRepository>();
            services.AddScoped<ISupplyInvoiceDetailRepository, SupplyInvoiceDetailRepository>();
            services.AddScoped<ICurrentInventorySupplyRepository, CurrentInventorySupplyRepository>();
            services.AddScoped<ICurrentInventorySupplyRepository, CurrentInventorySupplyRepository>();
            services.AddScoped<IInventorySupplyTransactionRepository, InventorySupplyTransactionRepository>();
            services.AddScoped<IDishRepository, DishRepository>();
            services.AddScoped<IDishSuppliesRepository, DishSuppliesRepository>();
            services.AddScoped<IDishCategoryRepository, DishCategoryRepository>();
            services.AddScoped<ITableRepository, TableRepository>();
            services.AddScoped<IComboRepository, ComboRepository>();
            services.AddScoped<IComboDetailRepository, ComboDetailRepository>();
            services.AddScoped<IInventoryService, InventoryService>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderTypeRepository, OrderTypeRepository>();
            services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserHelper, UserHelper>();
            services.AddScoped<IExpoServices, ExpoServices>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddSignalR();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors();
            app.UseCors("ReactClient");
            app.UseCors("MobileClient");
            app.UseCors("NetlifyClient");

            app.UseAuthentication();
            app.UseSignalR(routes =>
            {
                routes.MapHub<OrderHub>("/redpeper/app");
            });
            app.UseMvc();

        }
    }
}
