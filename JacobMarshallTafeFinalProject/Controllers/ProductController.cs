using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
//...
using JacobMarshallTafeFinalProject.Services;
using JacobMarshallTafeFinalProject.Models;
using JacobMarshallTafeFinalProject.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace JacobMarshallTafeFinalProject.Controllers
{
    public class ProductController : Controller
    {
		//fields
		IDataService<Product> _productService;
		IDataService<Category> _categoryService;

        //A Helper to identify Hosting side path info 
        private readonly IHostingEnvironment _hostingEnvironment;

        //constructor
        public ProductController(IDataService<Product> productService,
			IDataService<Category> categoryService,
            IHostingEnvironment hostingEnvironment)
		{
			_productService = productService;
			_categoryService = categoryService;
            _hostingEnvironment = hostingEnvironment;
        }

		//methods
		[HttpGet]
		public IActionResult Display(string categoryName, string price, int number)
		{
			//call service
			IQueryable<Product> list = _productService.GetQuery();
			IEnumerable<Category> catList = _categoryService.GetAll();

			if (!string.IsNullOrEmpty(categoryName))
			{
				list = list.Where(l => l.Cat.CategoryName == categoryName);
			}
			if (!string.IsNullOrEmpty(price))
			{
				if (price == "max" && number != 0)
				{
					list = list.Where(l => l.Price <= number);
				}else if (price == "min" && number != 0)
				{
					list = list.Where(l => l.Price >= number);
				}
			}
			//vm
			ProductDisplayViewModel vm = new ProductDisplayViewModel
			{
				Products = list.ToList(),
				Categories = catList
			};

			//pass to view
			return View(vm);
		}

		[HttpGet]
		[Authorize(Roles ="Admin")]
		public IActionResult Create()
		{
			//call service
			IEnumerable<Category> list = _categoryService.GetAll();

			//vm
			ProductCreateViewModel vm = new ProductCreateViewModel
			{
				Cat = list
			};
			return View(vm);
		}

		[HttpPost]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Create(ProductCreateViewModel vm, IFormFile image)
		{
			
			//check if the data is valid
			if (ModelState.IsValid)
			{
				//map vm to model
				Product product = new Product
				{					
					ProductName = vm.ProdName,
					ProductDetails = vm.ProdDetails,
					Price = vm.Price,
					CategoryId = vm.CategoryId,
				};

                if (image != null)
                {
                    //Create a path including the filename where we want to save the file 
                    var fileName = Path.Combine(_hostingEnvironment.WebRootPath, "images", Path.GetFileName(image.FileName));
                    //copy the file from temp memory to a parmanement memory
                    var fileStream = new FileStream(fileName, FileMode.Create);
                    await image.CopyToAsync(fileStream);
                    //Whenever you use any System.IO interface or classes you makesure you close the process;
                    fileStream.Close();
                    product.Image = Path.GetFileName(image.FileName);
                }
				
				//call service
				_productService.Create(product);
				TempData["message"] = $"{ product.ProductName} has been saved.";

				return RedirectToAction("AdminDisplay", "Product");
			}

			//if invalid
			return View(vm);
		}

		[HttpGet]
		public IActionResult Details(int id)
		{
			//call service
			//Get Query queries all products
			//.Include() says include Cat
			//Where narrows it down to the Product ID matching the parrameter ID
			//first or default grabs first ID to match
			Product prod = _productService.GetQuery().Include(p => p.Cat).Where(p => p.ProductId == id).FirstOrDefault();
		
			//create vm
			ProductDetailsViewModel vm = new ProductDetailsViewModel
			{
				ProductId = prod.ProductId,
				ProductName = prod.ProductName,
				ProductDetails = prod.ProductDetails,
				Price = prod.Price,
                Image = prod.Image,
				CategoryName = prod.Cat.CategoryName,
                Discontinue = prod.Discontinue
				
			};

			//pass to view
			return View(vm);
		}

		[HttpGet]
		[Authorize(Roles = "Admin")]
		public IActionResult AdminDisplay()
		{
			//call service
			IEnumerable<Product> list = _productService.GetAll();
			IEnumerable<Category> cat = _categoryService.GetAll();
			

			//vm
			ProductAdminViewModel vm = new ProductAdminViewModel
			{
				Products = list,
				Categories = cat
			};

			//pass to view
			return View(vm);
		}

		[HttpGet]
		[Authorize(Roles = "Admin")]
		public IActionResult Update(int id)
		{
			//call category service
			IEnumerable<Category> list = _categoryService.GetAll();
			Product prod = _productService.GetQuery().Include(p => p.Cat).Where(p => p.ProductId == id).FirstOrDefault();
			ProductUpdateViewModel vm = new ProductUpdateViewModel
			{
				ProductId = prod.ProductId,
				Name = prod.ProductName,
				Details = prod.ProductDetails,
				Price = prod.Price,
                Image = prod.Image,
				Discontinue = prod.Discontinue,
				CategoryName = prod.Cat.CategoryName,
				Cat = list
			};

			//pass to view
			return View(vm);
		}

		[HttpPost]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Update(ProductUpdateViewModel vm, IFormFile image)
		{

			//check if data is valid
			if (ModelState.IsValid)
			{
                Product prod = _productService.GetSingle(p => p.ProductId == vm.ProductId);

                //map vm to model
                prod.ProductName = vm.Name;
                prod.ProductDetails = vm.Details;
                prod.Price = vm.Price;
                prod.Discontinue = vm.Discontinue;
                prod.CategoryId = vm.CategoryId;
				

                if (image != null)
                {
                    //Create a path including the filename where we want to save the file 
                    var fileName = Path.Combine(_hostingEnvironment.WebRootPath, "images", Path.GetFileName(image.FileName));
                    //copy the file from temp memory to a parmanement memory
                    var fileStream = new FileStream(fileName, FileMode.Create);
                    await image.CopyToAsync(fileStream);
                    //Whenever you use any System.IO interface or classes you makesure you close the process;
                    fileStream.Close();
                    prod.Image = Path.GetFileName(image.FileName);
                    
                }

                //call service
                _productService.Update(prod);
				TempData["message"] = $"{ prod.ProductName} has been updated.";

				//return to page
				return RedirectToAction("AdminDisplay", "Product");
			}

			//if invalid
			return View(vm);
		}

		


	}
}