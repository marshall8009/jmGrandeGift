using JacobMarshallTafeFinalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JacobMarshallTafeFinalProject.ViewModels
{
	public class ProductDetailsViewModel
	{
		public int ProductId { get; set; }
		public string ProductName { get; set; }
		public string ProductDetails { get; set; }
		public decimal Price { get; set; }
        public string Image { get; set; }
        public string CategoryName { get; set; }
		public bool Discontinue { get; set; }
		//public IEnumerable<Product> Products { get; set; }
	}
}
