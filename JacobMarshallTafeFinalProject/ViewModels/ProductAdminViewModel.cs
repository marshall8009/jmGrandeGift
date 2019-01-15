using JacobMarshallTafeFinalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JacobMarshallTafeFinalProject.ViewModels
{
	public class ProductAdminViewModel
	{
		public IEnumerable<Product> Products { get; set; }
		public string CategoryName { get; set; }
		public int CategoryId { get; set; }
		public IEnumerable<Category> Categories { get; set; }
	}
}
