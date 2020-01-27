﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Redpeper.Data;
using Redpeper.Repositories;
using Redpeper.Repositories.Inventory;
using Redpeper.Repositories.InvoiceSupply;
using Redpeper.Repositories.Order.Dishes;
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddCors();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IProviderRepository, ProviderRepository>();
            services.AddScoped<ISupplyRepository, SupplyRepository>();
            services.AddScoped<ISupplyInvoiceRepository, SupplyInvoiceRepository>();
            services.AddScoped<ISupplyInvoiceDetailRepository, SupplyInvoiceDetailRepository>();
            services.AddScoped<ICurrentInventorySupplyRepository, CurrentInventorySupplyRepository>();
            services.AddScoped<ICurrentInventorySupplyRepository, CurrentInventorySupplyRepository>();
            services.AddScoped<IInventorySupplyTransactionRepository, InventorySupplyRepository >();
            services.AddScoped<IDishRepository, DishRepository>();
            services.AddScoped<IDishSuppliesRepository, DishSuppliesRepository>();
            services.AddScoped<IInventoryService, InventoryService>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseMvc();
        }
    }
}
