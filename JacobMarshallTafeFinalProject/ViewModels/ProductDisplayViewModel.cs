using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//...
using JacobMarshallTafeFinalProject.Models;


namespace JacobMarshallTafeFinalProject.ViewModels
{
    public class ProductDisplayViewModel
    {
		public IEnumerable<Product> Products { get; set; }
		public IEnumerable<Category> Categories { get; set; }
	}
}
