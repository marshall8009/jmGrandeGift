using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//...
using System.ComponentModel.DataAnnotations.Schema;

namespace JacobMarshallTafeFinalProject.Models
{
    public class Address
    {
		public int AddressId { get; set; }
		public string StreetNumber { get; set; }
		public string StreetName { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public string Postcode { get; set; }
		public string Country { get; set; }
		//FK
		public int CustomerId { get; set; }
		//Navigation property
		public Customer customer { get; set; }
	}
}
