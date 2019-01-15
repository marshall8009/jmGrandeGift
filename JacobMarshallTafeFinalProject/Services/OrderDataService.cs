using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using JacobMarshallTafeFinalProject.Models;

namespace JacobMarshallTafeFinalProject.Services
{
	public class OrderDataService : IOrderDataService
	{
		//fields
		private MyDbContext _context;


		//constructor
		public OrderDataService(MyDbContext context)
		{
			_context = context;

		}

		//methods
		public IQueryable<Order> Orders => _context.TblOrder
							.Include(o => o.Lines)
							.ThenInclude(l => l.Product); 
			//each product object in the Lines collection should also be loaded
			//order object is read from the database
			//tells EF that the collection of Lines should also be read and loaded
		

		public void SaveOrder(Order order)
		{
			_context.AttachRange(order.Lines.Select(l => l.Product));
			//tells EF not to deserialize the Product object associated with the Order object
			if (order.OrderId == 0)
			{
				_context.TblOrder.Add(order);
			}
			_context.SaveChanges();
		}
	}
}
