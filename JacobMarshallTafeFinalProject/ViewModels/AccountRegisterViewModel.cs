﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//...
using System.ComponentModel.DataAnnotations;


namespace JacobMarshallTafeFinalProject.ViewModels
{
    public class AccountRegisterViewModel
    {
		[Required, DataType(DataType.EmailAddress)]
		public string Email { get; set; }

		[Required, DataType(DataType.Password)]
		public string Password { get; set; }

		[Required, DataType(DataType.Password), Compare(nameof(Password))]
		public string ConfirmPassword { get; set; }
	}
}
