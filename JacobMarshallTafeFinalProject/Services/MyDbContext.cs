using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//...
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using JacobMarshallTafeFinalProject.Models;


namespace JacobMarshallTafeFinalProject.Services
{
    public class MyDbContext : IdentityDbContext
    {
		public DbSet<Customer> TblCustomer { get; set; }
		public DbSet<Product> TblProduct { get; set; }
		public DbSet<Category> TblCategory { get; set; }
		public DbSet<Order> TblOrder { get; set; }
		public DbSet<Address> TblAddress { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder option)
		{

			//connection string
			//option.UseSqlServer(@"Server = (localDB)\MSSQLLocalDB; Database = JMHampersDatabase; Trusted_Connection = true;");
		}

		public MyDbContext(DbContextOptions options) : base(options)
		{

		}
	}
}
