using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//...
using System.ComponentModel.DataAnnotations;

namespace JacobMarshallTafeFinalProject.Models
{
	public class Category
	{
		public int CategoryId { get; set; }

		[Required(ErrorMessage = "Category name is required")]
		public string CategoryName { get; set; }

		[Required(ErrorMessage = "Category details are required")]
		public string CategoryDetails { get; set; }
		//one category - to many products
		public ICollection<Product> Products { get; set; }
	}
}
