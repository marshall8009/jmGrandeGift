using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//...
using JacobMarshallTafeFinalProject.Models;

namespace JacobMarshallTafeFinalProject.ViewModels
{
    public class AddressCreateViewModel
    {
        public int AddressId { get; set; }
        public string StreetNumber { get; set; }
		public string StreetName { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public string Postcode { get; set; }
		public string Country { get; set; }
		//FK
		//Navigation property
		public IEnumerable<Customer> customer { get; set; }
	}
}
