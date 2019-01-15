using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
//...
using JacobMarshallTafeFinalProject.Models;
using JacobMarshallTafeFinalProject.Services;
using JacobMarshallTafeFinalProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace JacobMarshallTafeFinalProject.Controllers
{
    public class CustomerController : Controller
    {
		//fields
		private IDataService<Customer> _customerService;
		private UserManager<IdentityUser> _userManager;

		//constructor
		public CustomerController(IDataService<Customer> customerService,
									UserManager<IdentityUser> userManager)
		{
			_customerService = customerService;
			_userManager = userManager;
		}

		//methods
		[Authorize]
		[HttpGet]
		public IActionResult Profile()
		{
			string userId =_userManager.GetUserId(User);
			Customer customer = _customerService.GetSingle(c => c.UserID == userId);
			var user = User.Identity.Name;

			CustomerProfileViewModel vm = new CustomerProfileViewModel
			{
				FirstName = customer.FirstName,
				LastName = customer.LastName,
				DOB = customer.DOB,
				PhoneNumber = customer.PhoneNumber,
				Email = user
			};

			return View(vm);
		}

		[Authorize]
		[HttpGet]
		public IActionResult Create()
		{
			var user = User.Identity.Name;
			CustomerProfileViewModel cust = new CustomerProfileViewModel
			{
				Email = user
			};
			
			return View(cust);
		}

		[Authorize]
		[HttpPost]
		public async Task<IActionResult> Create(CustomerProfileViewModel vm)
		{
			var user = await _userManager.GetUserAsync(User);
			//check if data is valid
			if (ModelState.IsValid)
			{
				Customer cust = new Customer()
				{
					FirstName = vm.FirstName,
					LastName = vm.LastName,
					//user.Email = vm.Email,
					DOB = vm.DOB.Date,
					PhoneNumber = vm.PhoneNumber,
					UserID = user.Id
				};

				//call service
				_customerService.Create(cust);

				//go back to home
				return RedirectToAction("Index", "Home");
			}
			//if invalid
			return View();
		}

		[Authorize]
		[HttpGet]
		public IActionResult Update(int id)
		{
			var user = User.Identity.Name;

			string userId = _userManager.GetUserId(User);
			Customer cust = _customerService.GetSingle(c => c.UserID == userId);
			CustomerUpdateViewModel vm = new CustomerUpdateViewModel
			{
				CustomerId = cust.CustomerId,
				FirstName = cust.FirstName,
				LastName = cust.LastName,
				Email = user,
				DOB = cust.DOB,
				PhoneNumber = cust.PhoneNumber
			};

			return View(vm);

		}

		[Authorize]
		[HttpPost]
		public IActionResult Update(CustomerUpdateViewModel vm)
		{		
			string userId = _userManager.GetUserId(User);
			
			//check if data is valid
			if (ModelState.IsValid)
			{
				Customer customer = _customerService.GetSingle(c => c.UserID == userId);

				
				customer.FirstName = vm.FirstName;
				customer.LastName = vm.LastName;
				customer.DOB = vm.DOB;
				customer.PhoneNumber = vm.PhoneNumber;
				customer.UserID = userId;
				
				

				//call service
				_customerService.Update(customer);
			

				//return to page
				return RedirectToAction("Profile", "Customer");
			}

			//if invalid
			return View(vm);

			
		}
    }
}