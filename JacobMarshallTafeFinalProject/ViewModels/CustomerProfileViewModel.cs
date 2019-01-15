using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//...
using JacobMarshallTafeFinalProject.Models;
using System.ComponentModel.DataAnnotations;

namespace JacobMarshallTafeFinalProject.ViewModels
{
    public class CustomerProfileViewModel
    {
		public int CustomerId { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }

		[DataType(DataType.Date)]
		public DateTime DOB { get; set; }
		public string PhoneNumber { get; set; }
	}
}
