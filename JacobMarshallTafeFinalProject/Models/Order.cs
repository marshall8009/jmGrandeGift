using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace JacobMarshallTafeFinalProject.Models
{
	public class Order
	{
		[BindNever]
		public int OrderId { get; set; }

		[BindNever]
		public ICollection<CartLine> Lines { get; set; }

		[Required(ErrorMessage ="Please enter a name")]
		public string Name { get; set; }

		[Required(ErrorMessage ="Please enter your address")]
		public string Line1 { get; set; }
		public string Line2 { get; set; }
		public string Line3 { get; set; }

		[Required(ErrorMessage = "Please enter your city")]
		public string City { get; set; }

		[Required(ErrorMessage = "Please enter your state")]
		public string State { get; set; }

		[Required(ErrorMessage = "Please enter your postcode")]
		public string Postcode { get; set; }

		[Required(ErrorMessage = "Please enter your country")]
		public string Country { get; set; }
	}
}
