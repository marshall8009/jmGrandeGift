using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
//...
using JacobMarshallTafeFinalProject.Models;
using JacobMarshallTafeFinalProject.ViewModels;
using JacobMarshallTafeFinalProject.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace JacobMarshallTafeFinalProject.Controllers
{
    public class AddressController : Controller
    {
		//fields
		private IDataService<Address> _addressDataService;
		private IDataService<Customer> _customerDataService;
		private UserManager<IdentityUser> _userManager;

		//constructor
		public AddressController(IDataService<Address> addressDataService,
							IDataService<Customer> customerDataService,
							UserManager<IdentityUser> userManager)
		{
			_addressDataService = addressDataService;
			_customerDataService = customerDataService;
			_userManager = userManager;
		}

		//methods
		[Authorize]
		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}
		[Authorize]
		[HttpPost]
		public IActionResult Create(AddressCreateViewModel vm)
		{
			string userId = _userManager.GetUserId(User);
			int customerId = _customerDataService.GetQuery().Include(c => c.Addresses).Where(c => c.UserID == userId).Select(c => c.CustomerId).FirstOrDefault();
			//check if data is valid
			if (ModelState.IsValid)
			{
				//map to vm
				Address address = new Address
				{
					StreetNumber = vm.StreetNumber,
					StreetName = vm.StreetName,
					City = vm.City,
					State = vm.State,
					Postcode = vm.Postcode,
					Country = vm.Country,
					CustomerId = customerId
				};

				//call service
				_addressDataService.Create(address);
				TempData["message"] = $"New address successfully saved.";

				//return to home
				return RedirectToAction("Display", "Address");
			}

			//if invalid
			return View(vm);
		}

		[HttpGet]
		public IActionResult Display()
		{
			//call service
			string userId = _userManager.GetUserId(User);
			Customer cust = _customerDataService.GetSingle(c => c.UserID == userId);
			IQueryable<Address> list = _addressDataService.GetQuery().Where(a => a.CustomerId == cust.CustomerId);

			//vm
			AddressDisplayViewModel vm = new AddressDisplayViewModel
			{
				Addresses = list
			};

			//pass to view
			return View(vm);
		}

		[HttpGet]
		public IActionResult Update(int id)
		{
			//call service

			Address address = _addressDataService.GetSingle(a => a.AddressId == id);

			//pass to vm
			AddressUpdateViewModel vm = new AddressUpdateViewModel
			{
				StreetNumber = address.StreetNumber,
				StreetName = address.StreetName,
				City = address.City,
				State = address.State,
				Postcode = address.Postcode,
				Country = address.Country,
			};

			return View(vm);
		}

		[HttpPost]
		public IActionResult Update(int id, AddressUpdateViewModel vm)
		{
			//check if data is valid
			if (ModelState.IsValid)
			{
                
				Address address = _addressDataService.GetSingle(a => a.AddressId == id);


				address.StreetNumber = vm.StreetNumber;
				address.StreetName = vm.StreetName;
				address.City = vm.City;
				address.State = vm.State;
				address.Postcode = vm.Postcode;
				address.Country = vm.Country;				
				_addressDataService.Update(address);
				TempData["message"] = "Address has been updated.";

				return RedirectToAction("Display", "Address");
			}

			return View(vm);
		}

		[HttpGet]
		public IActionResult Delete(int id)
		{
			_addressDataService.Delete(new Address { AddressId = id });
			TempData["message"] = "Address has been deleted.";
			return RedirectToAction("Display", "Address");
		}
	}
}