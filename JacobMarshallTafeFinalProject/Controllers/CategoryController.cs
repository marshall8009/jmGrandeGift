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

namespace JacobMarshallTafeFinalProject.Controllers
{
    public class CategoryController : Controller
    {
		//fields
		private IDataService<Product> _productService;
		private IDataService<Category> _categoryService;


		
		//constructor
		public CategoryController(IDataService<Product> productService,
									IDataService<Category> categoryService)
		{
			_productService = productService;
			_categoryService = categoryService;
		}

		//methods
		[HttpGet]
		[Authorize(Roles = "Admin")]
		public IActionResult Display()
		{
			//call service
			IEnumerable<Category> list = _categoryService.GetAll();
			//vm
			CategoryDisplayViewModel vm = new CategoryDisplayViewModel
			{
				Total = list.Count(),
				Categories = list
			};
			//pass to view
			return View(vm);
		}

		[HttpGet]
		public IActionResult Details(int id)
		{
			//call category service
			Category cat = _categoryService.GetSingle(p => p.CategoryId == id);
			//call product service
			IEnumerable<Product> product = _productService.Query(p => p.CategoryId == id);

			//create vm
			CategoryDetailsViewModel vm = new CategoryDetailsViewModel
			{
				CatId = cat.CategoryId,
				CatName = cat.CategoryName,
				CatDetails = cat.CategoryDetails,
				Products = product 
			};

			//pass to the view
			return View(vm);
		}

		[HttpGet]
		[Authorize(Roles = "Admin")]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[Authorize(Roles = "Admin")]
		public IActionResult Create(CategoryCreateViewModel vm)
		{
			//check if the data is valid
			if (ModelState.IsValid)
			{
				//map vm to model
				Category cat = new Category
				{
					CategoryName = vm.CatName,
					CategoryDetails = vm.CatDetails
				};

				//call service
				_categoryService.Create(cat);
				TempData["message"] = $"{ cat.CategoryName} has been saved.";

				//return to home
				return RedirectToAction("Display", "Category");
			}

			//if invalid
			return View(vm);
		}

		[HttpGet]
		[Authorize(Roles = "Admin")]
		public IActionResult Update(int id)
		{
			Category cat = _categoryService.GetSingle(c => c.CategoryId == id);
			CategoryUpdateViewModel vm = new CategoryUpdateViewModel
			{
				CategoryId = cat.CategoryId,
				Name = cat.CategoryName,
				Details = cat.CategoryDetails
			};

			return View(vm);
		}

		[HttpPost]
		[Authorize(Roles = "Admin")]
		public IActionResult Update(CategoryUpdateViewModel vm)
		{
			//check if model is valid
			if (ModelState.IsValid)
			{
				//map vm to model
				Category cat = new Category
				{
					CategoryId = vm.CategoryId,
					CategoryName = vm.Name,
					CategoryDetails = vm.Details
				};

				//call service
				_categoryService.Update(cat);
				TempData["message"] = $"{ cat.CategoryName} has been updated.";

				//return to page
				return RedirectToAction("Display", "Category");
			}

			//if invalid
			return View(vm);
		}

		//[HttpGet]
		//[Authorize(Roles = "Admin")]
		//public IActionResult Delete(int id)
		//{
		//	_categoryService.Delete(new Category { CategoryId = id });
		//	TempData["message"] = $"Categroy has been deleted.";
		//	return RedirectToAction("Display", "Category");
		//}

		
    }


}