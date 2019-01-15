using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
//...

namespace JacobMarshallTafeFinalProject.Models
{
    public class Customer
    {
		public int CustomerId { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public DateTime DOB { get; set; }
		public string PhoneNumber { get; set; }
		//FK
		[ForeignKey("User")]
		public string UserID { get; set; }
		//Navigation property
		public IdentityUser User { get; set; }
		public ICollection<Address> Addresses { get; set; }
	}
	
}
