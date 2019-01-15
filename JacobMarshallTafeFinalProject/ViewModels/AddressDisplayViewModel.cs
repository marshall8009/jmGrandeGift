using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//...
using JacobMarshallTafeFinalProject.Models;

namespace JacobMarshallTafeFinalProject.ViewModels
{
    public class AddressDisplayViewModel
    {
		public IEnumerable<Address> Addresses { get; set; }
		public IEnumerable<Customer> Customer { get; set; }
	}
}
