using JacobMarshallTafeFinalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JacobMarshallTafeFinalProject.ViewModels
{
	public class ProductUpdateViewModel
	{
		public int ProductId { get; set; }
		public string Name { get; set; }
		public string Details { get; set; }
		public decimal Price { get; set; }
        public string Image { get; set; }
        public string CategoryName { get; set; }
		public int CategoryId { get; set; }
		public bool Discontinue { get; set; }
		public IEnumerable<Category> Cat { get; set; }
	}
}
