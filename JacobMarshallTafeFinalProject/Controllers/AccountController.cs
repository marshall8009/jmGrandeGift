using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
//...
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using JacobMarshallTafeFinalProject.ViewModels;
using JacobMarshallTafeFinalProject.Models;
using JacobMarshallTafeFinalProject.Services;
using Microsoft.AspNetCore.Authorization;

namespace JacobMarshallTafeFinalProject.Controllers
{
	public class AccountController : Controller
	{
		//fields
		private UserManager<IdentityUser> _userManagerService;
		private SignInManager<IdentityUser> _signInManagerService;
		private IDataService<Customer> _customerService;

		//constructor
		public AccountController(UserManager<IdentityUser> managerService,
								SignInManager<IdentityUser> signInService,
								IDataService<Customer> customerService)
		{
			_userManagerService = managerService;
			_signInManagerService = signInService;
			_customerService = customerService;
		}


		//methods
		[HttpGet]
		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Register(AccountRegisterViewModel vm)
		{
			if (ModelState.IsValid)
			{
				//add new user into the database
				IdentityUser user = new IdentityUser(vm.Email);
				user.Email = vm.Email;
				IdentityResult result = await _userManagerService.CreateAsync(user, vm.Password);
				//Customer customer = _customerService.Create();
				

				if (result.Succeeded)
				{
					Customer customer = new Customer
					{
						UserID = user.Id
						

					};
					_customerService.Create(customer);
					//signs them in after register
					var results = await _signInManagerService.PasswordSignInAsync(vm.Email,
					vm.Password, false, false);
					//return to home page
					return RedirectToAction("Profile", "Customer");
				}
				else
				{
					//show errors
					foreach (var error in result.Errors)
					{
						ModelState.AddModelError("", error.Description);
					}
				}
				
			}
			return View(vm);
		}

		[HttpGet]
		public IActionResult Login(string returnUrl = "")
		{
			AccountLoginViewModel vm = new AccountLoginViewModel
			{
				ReturnUrl = returnUrl
			};
			return View(vm);
		}

		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(AccountLoginViewModel vm)
		{
			if (ModelState.IsValid)
			{
				var results = await _signInManagerService.PasswordSignInAsync(vm.Email,
					vm.Password, vm.RememberMe, false);
				if (results.Succeeded)
				{
					var user = await _userManagerService.FindByEmailAsync(vm.Email);
					var isAdmin = await _userManagerService.IsInRoleAsync(user, "Admin");
					if (string.IsNullOrEmpty(vm.ReturnUrl))
					{
						if (isAdmin == true)
						{
							return RedirectToAction("Index", "Admin");
						}
						return RedirectToAction("Index", "Home");
					}
					else
					{
						return Redirect(vm.ReturnUrl);
					}					
				}
				else
				{
					ModelState.AddModelError("", "Username or password is not correct");
					return View(vm);
				}
			}
			return View(vm);
		}

		public async Task<IActionResult> Logout()
		{
			await _signInManagerService.SignOutAsync();
			return RedirectToAction("Index", "home");
		}
	
    }
}