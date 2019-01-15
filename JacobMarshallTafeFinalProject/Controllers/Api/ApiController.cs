using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
//...
using JacobMarshallTafeFinalProject.Models;
using JacobMarshallTafeFinalProject.Services;

namespace JacobMarshallTafeFinalProject.Controllers.Api
{
	[Route("api/products")]
	public class ApiController : Controller
	{
		//fields
		IDataService<Product> _productService;
		IDataService<Category> _categoryService;

		//constructor
		public ApiController(IDataService<Product> productService,
			IDataService<Category> categoryService)
		{
			_productService = productService;
			_categoryService = categoryService;
		}

		[HttpGet]
		public IEnumerable<Product> Get()
		{
			return _productService.GetAll();
		}

		[HttpGet("{categoryName}")]
		public IEnumerable<Product> Get(string categoryName)
		{
			return _productService.GetQuery().Where(p => p.Cat.CategoryName == categoryName).ToList();
		}
	}
}