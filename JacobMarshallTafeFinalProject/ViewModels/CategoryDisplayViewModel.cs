using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//...
using JacobMarshallTafeFinalProject.Models;

namespace JacobMarshallTafeFinalProject.ViewModels
{
    public class CategoryDisplayViewModel
    {
		public int Total { get; set; }
		public IEnumerable<Category> Categories { get; set; }
	}
}
