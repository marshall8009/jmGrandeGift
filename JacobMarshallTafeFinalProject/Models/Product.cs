using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//...
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace JacobMarshallTafeFinalProject.Models
{
	public class Product
	{		
		public int ProductId { get; set; }//Pk

		[Required(ErrorMessage = "Product name is required")]
		public string ProductName { get; set; }

		[Required(ErrorMessage = "Product details are required")]
		public string ProductDetails { get; set; }

		[Required]
		[Range(0.01, double.MaxValue, ErrorMessage = "Price requires a positive value")]
		public decimal Price { get; set; }
        public string Image { get; set; }

        public bool Discontinue { get; set; }

		[Required(ErrorMessage = "Category is required")]
		//FK category
		public int CategoryId { get; set; }
		//Navigation property
		public Category Cat { get; set; }

		

	}
}
