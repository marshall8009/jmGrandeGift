using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
//...
using JacobMarshallTafeFinalProject.Models;
using JacobMarshallTafeFinalProject.ViewModels;
using JacobMarshallTafeFinalProject.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;

namespace JacobMarshallTafeFinalProject.Controllers
{
    public class OrderController : Controller
    {
		//fields
		private IOrderDataService _orderDataService;
        private IDataService<Address> _addressDataService;
        private UserManager<IdentityUser> _userManager;
        private IDataService<Customer> _customerDataService;

		//constructor
		public OrderController(IOrderDataService orderDataService,
            IDataService<Address> addressDataService,
            UserManager<IdentityUser> userManagerService,
            IDataService<Customer> customerDataService)
		{
			_orderDataService = orderDataService;
            _userManager = userManagerService;
            _customerDataService = customerDataService;
            _addressDataService = addressDataService;
		}

		//methods
        public IActionResult Checkout()
		{
			OrderCheckoutViewModel order = new OrderCheckoutViewModel();
            string userId = _userManager.GetUserId(User);
            //int customerId = _customerDataService.GetQuery().Where(c => c.UserID == userId).Select(c => c.CustomerId).FirstOrDefault();
            var addresses = _addressDataService.GetQuery().Where(a => a.customer.UserID == userId).Select(a => new
            {
                AddressId = a.AddressId,
                FullAddress = a.StreetNumber + " " + a.StreetName + " " + a.City + " " + a.State + " " + a.Country + " " + a.Postcode
            }).ToList();
            order.Addresses = new SelectList(addresses, "AddressId", "FullAddress");
			return View(order);
		}

		[HttpPost]
		public IActionResult Checkout(OrderCheckoutViewModel vm)
		{
            var cart = GetCart();
			if (cart.Lines.Count() == 0)
			{
				ModelState.AddModelError("", "Your cart is empty");
			}

			if (!vm.AddressId.HasValue)
			{
				OrderAddressViewModel address = new OrderAddressViewModel
				{
					Line1 = vm.Line1,
					Line2 = vm.Line2,
					Line3 = vm.Line3,
					City = vm.City,
					State = vm.State,
					Country = vm.Country,
					Postcode = vm.Postcode
				};

				TryValidateModel(address);
			}

			if (ModelState.IsValid)
			{

                Order order = new Order
                {
                    OrderId = vm.OrderId,
                    Name = vm.Name,
                    Line1 = vm.Line1,
                    Line2 = vm.Line2,
                    Line3 = vm.Line3,
                    City = vm.City,
                    State = vm.State,
                    Country = vm.Country,
                    Postcode = vm.Postcode,
                    Lines = cart.Lines.ToList()
                };
                if (vm.AddressId.HasValue)
                {
                    var address = _addressDataService.GetSingle(a => a.AddressId == vm.AddressId);
                    string userId = _userManager.GetUserId(User);
                    var customer = _customerDataService.GetQuery().Where(c => c.UserID == userId).Select(c => c.FirstName).FirstOrDefault();
                    order.Line1 = address.StreetNumber;
                    order.Line2 = address.StreetName;
                    order.City = address.City;
                    order.State = address.State;
                    order.Country = address.Country;
                    order.Postcode = address.Postcode;
                    order.Name = customer;
                }
                vm.CartLine = cart.Lines.ToArray();
				_orderDataService.SaveOrder(order);
				return RedirectToAction(nameof(Completed));
			}
			else
			{
				string userId = _userManager.GetUserId(User);
				//int customerId = _customerDataService.GetQuery().Where(c => c.UserID == userId).Select(c => c.CustomerId).FirstOrDefault();
				var addresses = _addressDataService.GetQuery().Where(a => a.customer.UserID == userId).Select(a => new
				{
					AddressId = a.AddressId,
					FullAddress = a.StreetNumber + " " + a.StreetName + " " + a.City + " " + a.State + " " + a.Country + " " + a.Postcode
				}).ToList();
				vm.Addresses = new SelectList(addresses, "AddressId", "FullAddress");
				return View(vm);
			}
		}

        private Cart GetCart()
        {
            Cart cart = HttpContext.Session.GetJson<Cart>("Cart") ?? new Cart();
            return cart;
        }

        public IActionResult Completed()
		{
            var cart = GetCart();
			cart.Clear();
			HttpContext.Session.Remove("Cart");
			return View();
		}
    }
}