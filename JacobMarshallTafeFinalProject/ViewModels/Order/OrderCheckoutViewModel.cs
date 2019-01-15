using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
//...
using JacobMarshallTafeFinalProject.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace JacobMarshallTafeFinalProject.ViewModels
{
	public class OrderCheckoutViewModel
	{
		[BindNever]
		public int OrderId { get; set; }

		[BindNever]
		public ICollection<CartLine> CartLine { get; set; }


		public string Name { get; set; }


		public string Line1 { get; set; }
		public string Line2 { get; set; }
		public string Line3 { get; set; }


		public string City { get; set; }


		public string State { get; set; }

	
		public string Postcode { get; set; }

	
		public string Country { get; set; }
        //FK
        public int? AddressId { get; set; }
        public SelectList Addresses { get; set; }
    }

	public class OrderAddressViewModel
	{
		[Required(ErrorMessage = "Please enter your address")]
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
