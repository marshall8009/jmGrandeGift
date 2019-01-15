using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//...
using JacobMarshallTafeFinalProject.Models;

namespace JacobMarshallTafeFinalProject.ViewModels
{
	public class CategoryDetailsViewModel
	{
		public int CatId { get; set; }
		public string CatName { get; set; }
		public string CatDetails { get; set; }
		public IEnumerable<Category> Categories { get; set; }
		public IEnumerable<Product> Products { get; set; }
	}
}
