using JacobMarshallTafeFinalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JacobMarshallTafeFinalProject.ViewModels
{
	public class ProductCreateViewModel
	{
		public string ProdName { get; set; }
		public string ProdDetails { get; set; }
		public decimal Price { get; set; }
        public string Image { get; set; }
        public int CategoryId { get; set; }
		public IEnumerable<Category> Cat { get; set; }
	}
}
