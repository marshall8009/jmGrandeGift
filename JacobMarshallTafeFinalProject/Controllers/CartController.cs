using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
//...
using JacobMarshallTafeFinalProject.Models;
using JacobMarshallTafeFinalProject.ViewModels;
using JacobMarshallTafeFinalProject.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace JacobMarshallTafeFinalProject.Controllers
{
    public class CartController : Controller
    {
		//fields
		private IDataService<Product> _productService;
		private IDataService<Category> _categoryServcie;

		private Cart cart;

		//constructor
		public CartController(IDataService<Product> productService,
								IDataService<Category> categoryServcie,
								Cart cartService)
		{
			_productService = productService;
			_categoryServcie = categoryServcie;
			cart = cartService;
		}

		//methods
		public ViewResult Index(string returnUrl)
		{
			return View(new CartIndexViewModel {
				Cart = GetCart(),
				ReturnUrl = returnUrl
			});
		}

		public IActionResult AddToCart(int productId, string returnUrl)
		{
			Product product = _productService.GetSingle(p => p.ProductId == productId);

			if (product != null)
			{
				Cart cart = GetCart();
				cart.AddItem(product, 1);
				SaveCart(cart);
			}

			return RedirectToAction("Index", new { returnUrl });
		}

		public RedirectToActionResult RemoveFromCart(int productId, string returnUrl)
		{
			Product product = _productService.GetSingle(p => p.ProductId == productId);
			if (product != null)
			{
				Cart cart = GetCart();
				cart.RemoveLine(product);
				SaveCart(cart);
			}

			return RedirectToAction("Index", new { returnUrl });
		}

		private Cart GetCart()
		{
			Cart cart = HttpContext.Session.GetJson<Cart>("Cart") ?? new Cart();
			return cart;
		}

		private void SaveCart(Cart cart)
		{
			HttpContext.Session.SetJson("Cart", cart);
		}
	}
}