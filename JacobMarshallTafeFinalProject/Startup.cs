using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
//...
using Microsoft.Extensions.DependencyInjection;
using JacobMarshallTafeFinalProject.Services;
using JacobMarshallTafeFinalProject.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace JacobMarshallTafeFinalProject
{
    public class Startup
    {
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
			services.AddScoped<IDataService<Customer>, DataService<Customer>>();
			services.AddScoped<IDataService<Product>, DataService<Product>>();
			services.AddScoped<IDataService<Category>, DataService<Category>>();
			services.AddScoped<IOrderDataService, OrderDataService>();
			services.AddScoped<IDataService<Address>, DataService<Address>>();
			

			services.AddIdentity<IdentityUser, IdentityRole>
			(
				config =>
				{
					config.User.RequireUniqueEmail = true;
					config.Password.RequireDigit = true;
					config.Password.RequiredLength = 8;
					config.Password.RequireLowercase = true;
					config.Password.RequireUppercase = true;
					config.Password.RequireNonAlphanumeric = true;
				}
			).AddEntityFrameworkStores<MyDbContext>();

			services.AddDbContext<MyDbContext>(options =>
				options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

			services.AddScoped<Cart>(sp => SessionCart.GetCart(sp));

			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
			services.AddMvc();
			services.AddMemoryCache();
            services.AddSession(options =>
            {
                //doesn't allow the session id to be accessible by client side script
                options.Cookie.HttpOnly = true;
            });
		}

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

			app.UseStaticFiles();
			app.UseSession();
			app.UseAuthentication();
			app.UseMvcWithDefaultRoute();

			SeedHelper.Seed(app.ApplicationServices).Wait();
        }
    }
}
