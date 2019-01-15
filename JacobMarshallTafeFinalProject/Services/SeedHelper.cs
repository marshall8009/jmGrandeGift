using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//...
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using JacobMarshallTafeFinalProject.Models;
using JacobMarshallTafeFinalProject.Services;


namespace JacobMarshallTafeFinalProject.Services
{
	public static class SeedHelper
	{
		public static async Task Seed(IServiceProvider provider)
		{
			//set up the scope of our services that used 
			//our DI container
			var scopeFactory = provider.GetRequiredService<IServiceScopeFactory>();
			using (var scope = scopeFactory.CreateScope())
			{
				//built in services from identity packacges
				UserManager<IdentityUser> userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
				RoleManager<IdentityRole> roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

				//Add Admin role
				if (await roleManager.FindByNameAsync("Admin") == null)
				{
					await roleManager.CreateAsync(new IdentityRole("Admin"));
				}

				//add default admin
				if (await userManager.FindByNameAsync("admin1@yahoo.com") == null)
				{
					IdentityUser admin = new IdentityUser("admin1@yahoo.com");//creates new user name Admin1
					admin.Email = "admin1@yahoo.com";
					await userManager.CreateAsync(admin, "Apple#333");//add user to Users table with password Apple#333
					await userManager.AddToRoleAsync(admin, "Admin");//add admin1 to role Admin
				}

				//add sample product and category
				var context = scope.ServiceProvider.GetRequiredService<MyDbContext>();
				if (!context.TblCategory.Any() && !context.TblProduct.Any())
				{
					context.TblProduct.Add(new Product()
					{
						ProductName = "Newborn Baby Hamper",
						ProductDetails = "Hampers for newborns",
						Price = 75,
						Image = "christmasHamper.jpg",
						Cat = new Category()
						{
							CategoryName = "Baby",
							CategoryDetails = "Products for babies"
						}
					});
				}

				await context.SaveChangesAsync();
			}
		}
	}
}
